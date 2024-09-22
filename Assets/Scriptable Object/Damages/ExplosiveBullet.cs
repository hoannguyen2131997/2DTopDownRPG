using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/ExclusionBulletData")]
public class BulletExclusion : ScriptableObject
{
    public float Damage;
    public float Speed;
    public float ExplosionRadius;
    public BulletTrajectoryType TrajectoryType;
    public Sprite SpriteSplatter;
    public CurvedData CurvedData;
    public Sprite SpriteBullet;
    public float EventDurationExplosion;
}
