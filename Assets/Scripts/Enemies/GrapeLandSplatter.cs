using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLand : MonoBehaviour
{
    private SpriteFace spriteFace;

    private void Awake()
    {
        spriteFace = GetComponent<SpriteFace>();
    }

    private void Start()
    {
        StartCoroutine(spriteFace.SlowFadeRoutine());

        Invoke("DisableCollider", 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamege(1,transform);
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
