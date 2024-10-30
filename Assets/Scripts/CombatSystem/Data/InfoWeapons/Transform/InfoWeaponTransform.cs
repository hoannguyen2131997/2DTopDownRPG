using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponConfig", menuName = "ScriptableObjects/PlayerWeaponPositionConfig")]
public class InfoWeaponTransform : ScriptableObject
{
    // Weapon collider rotations
    public Vector3 UpWeaponCollider;
    public Vector3 DownWeaponCollider;
    public Vector3 LeftWeaponCollider;
    public Vector3 RightWeaponCollider;

    // Weapon flash rotations
    public Vector3 UpWeaponFlash;
    public Vector3 DownWeaponFlash;
    public Vector3 LeftWeaponFlash;
    public Vector3 RightWeaponFlash;
}
