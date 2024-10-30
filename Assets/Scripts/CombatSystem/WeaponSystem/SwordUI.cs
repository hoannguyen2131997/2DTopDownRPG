using UnityEngine;

public class SwordUI : MonoBehaviour
{
    [SerializeField] private SwordController _weaponManager;
    private Animator _aniSword;

    private void Awake()
    {
        _aniSword = this.GetComponent<Animator>();
    }

    public void RotateWeapon(string dirPlayer)
    {
       switch (dirPlayer)
       {
            case DirectionWeaponContanst.UpWeapon:
                 _aniSword.SetInteger("MoveDirection", 0);
                 break;
            case DirectionWeaponContanst.DownWeapon:
                 _aniSword.SetInteger("MoveDirection", 1);
                 break;
            case DirectionWeaponContanst.RightWeapon:
                 _aniSword.SetInteger("MoveDirection", 2);
                 break;
            case DirectionWeaponContanst.LeftWeapon:
                 _aniSword.SetInteger("MoveDirection", 3);
                 break;
       }
    }

    public void TriggerAttack(Vector2 _inputPlayer)
    {
        if (_inputPlayer != Vector2.zero)
        {
            _aniSword.SetTrigger("Attack");
        }
    }

    // Animation event
    public void BeginAttack()
    {
        _weaponManager.BeginAttack();
    }

    // Animation event
    public void EndAttack()
    {
        _weaponManager.EndAttack();
    }
}
