using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameInputSystemSingleton;

public class GrapeLand : MonoBehaviour
{
    private SpriteFace spriteFace;
    private PlayerController playerController;
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
        playerController = collision.gameObject.GetComponentInParent<PlayerController>();
        if(playerController != null)
        {
            playerController.eventsPlayerManager.GetDamegePlayer(damegeToPlayer);
        }
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
