using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "ScriptableObjects/WeaponBasicData")]
public class InfoBasicWeapon : ScriptableObject
{
    private string weaponName;       // Tên của vũ khí
    private float damage;            // Sát thương của vũ khí
    private float attackRange;       // Tầm tấn công của vũ khí
    private float attackSpeed;       // Tốc độ tấn công (số lần tấn công mỗi giây)

    // Public getters, nhưng không thể set từ bên ngoài
    public string WeaponName => weaponName;
    public float Damage => damage;
    public float AttackRange => attackRange;
    public float AttackSpeed => attackSpeed;
}
