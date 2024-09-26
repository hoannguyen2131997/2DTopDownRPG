using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private BulletExclusion bulletExclusionSto;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //public EnemyAttackData attackData;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK_HASH);

        if(transform.position.x - Character.Instance.transform.position.x < 0)
        {
            spriteRenderer.flipX = false;
        } else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void SpawProjectileAnimEvent()
    {
        Vector3 playerPos = Character.Instance.transform.position;
        GameObject bullet = ObjectPool.Instance.GetFromPool();
        bullet.GetComponent<SpriteRenderer>().sprite = bulletExclusionSto.SpriteBullet;
        bullet.transform.position = this.transform.position;

        GrapeProjectile grapeProjectile = bullet.AddComponent<GrapeProjectile>();
        grapeProjectile.Initialize(bulletExclusionSto);
    }
}

public enum BulletTrajectoryType
{
    Straight,  // Đường thẳng
    Curved,    // Đường cong
    Invisible  // Đạn vô hình, có thể cho thêm các loại khác như homing (tự động tìm mục tiêu)
}

[System.Serializable]
public struct CurvedData
{
    public float duration;
    public AnimationCurve animCurve;
    public float heightY;
}
