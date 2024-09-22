using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDamage : MonoBehaviour
{
    private SpriteFace spriteFace;
    private EventsPlayerManager eventPlayerManager;
    [SerializeField] private int damegeToPlayer = 1;

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
        eventPlayerManager = collision.gameObject.GetComponentInParent<EventsPlayerManager>();
        if (eventPlayerManager != null)
        {
            eventPlayerManager.GetDamegePlayer(damegeToPlayer);
        }
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
