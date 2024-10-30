using UnityEngine;

[CreateAssetMenu(menuName = "New weapon player")]
public class WeaponPlayerData : ScriptableObject
{
    public string weaponName;
    public int baseDamage;
    public float attackSpeed;
    public WeaponType weaponType; // Enum: Sword, Gun, Bow, Staff
    public SkillData[] skills; // Danh sách các kỹ năng của vũ khí
}

public enum WeaponType
{
    Sword,
    Gun,
    Bow,
    Staff
}

public enum SkillData
{
    normalAttack,
    skill1,
    skill2
}
