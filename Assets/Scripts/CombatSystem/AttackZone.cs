using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private SwordController _activeWeaponPlayer;
    void OnTriggerStay2D(Collider2D collision)
    {
        _activeWeaponPlayer.Attack(collision);
    }
}
