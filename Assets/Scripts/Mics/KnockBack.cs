using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageTakeSource, Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (damageTakeSource.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        //rb.AddForce(difference, ForceMode2D.Impulse);
        rb.position += difference;
        StartCoroutine(KnockRoutine());
    }


    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
