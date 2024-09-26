using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    private Transform shadow;
    private Color initialColor;
    private Renderer renderer;
    public float DamageToPlayer {  get; private set; }

    public void Initialize(BulletExclusion data)
    {
        DamageToPlayer = data.Damage;
        shadow = transform.GetChild(0);
        if (shadow != null)
        {
            shadow.gameObject.SetActive(true); 
        }

        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            initialColor = renderer.material.color;
        }

        Vector3 playerPos = Character.Instance.transform.position;
        Vector3 grapeShadowStartPostion = shadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(this.transform.position, playerPos, data));
        StartCoroutine(MoveGrapeShadowRoutine(shadow.gameObject, grapeShadowStartPostion, playerPos, data));
    }

    private IEnumerator Fade(float _dur)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dur)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / _dur);
            Color newColor = initialColor;
            newColor.a = alpha;

            renderer.material.color = newColor;

            yield return null;  // Chờ một frame
        }

        ResetDataToPool();
    }

    private void ResetDataToPool()
    {
        shadow = null;
        renderer = null;
        StopAllCoroutines();
        ObjectPool.Instance.ReturnToPool(this.gameObject);
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition, BulletExclusion data)
    {
        float timePassed = 0f;

        while (timePassed < data.CurvedData.duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / data.CurvedData.duration;
            float heightT = data.CurvedData.animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, data.CurvedData.heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        CreateExplosive(data);
    }

    private void CreateExplosive(BulletExclusion _data)
    {

        this.gameObject.GetComponent<SpriteRenderer>().sprite = _data.SpriteSplatter;

#if UNITY_EDITOR
        explosionRadius = _data.ExplosionRadius;
#endif

        // Thêm CircleCollider2D khi phát nổ
        CircleCollider2D explosionCollider = gameObject.AddComponent<CircleCollider2D>();
        explosionCollider.isTrigger = true;  // Đặt Collider là Trigger để phát hiện va chạm mà không ngăn cản đối tượng khác
        explosionCollider.radius = _data.ExplosionRadius;  // Đặt bán kính của vụ nổ

        StartCoroutine(Fade(_data.EventDurationExplosion));
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition, BulletExclusion data)
    {
        float timePassed = 0f;

        while (timePassed < data.CurvedData.duration)
        {
            timePassed += Time.deltaTime;
            float linerT = timePassed / data.CurvedData.duration;
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linerT);
            yield return null;
        }

        shadow.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    private Color explosionGizmoColor = Color.red;
    private float explosionRadius;
    
    void OnDrawGizmos()
    {
        if (ObjectPool.Instance.ShowGizmos)  // Chỉ vẽ Gizmos nếu biến showGizmos được bật và khi có data 
        {
            Gizmos.color = explosionGizmoColor;

            // Vẽ một đường tròn đại diện cho vụ nổ tại vị trí của đối tượng (gameObject)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
#endif
}