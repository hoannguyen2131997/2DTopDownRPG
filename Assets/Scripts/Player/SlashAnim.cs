using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    private ParticleSystem _system;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _system = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DestroySelf()
    {
      this.gameObject.SetActive(false);
    }

    public void SetAniFlash(string directionWeapon)
    {
        if(_spriteRenderer != null)
        {
            switch (directionWeapon)
            {
                case DirectionWeaponContanst.DownWeapon:
                    _spriteRenderer.flipY = true;
                    break;
                case DirectionWeaponContanst.UpWeapon:
                    _spriteRenderer.flipY = true;
                    break;
                case DirectionWeaponContanst.LeftWeapon:
                    _spriteRenderer.flipY = true;
                    break;
                case DirectionWeaponContanst.RightWeapon:
                    _spriteRenderer.flipY = false;
                    break;
            }
        }
    }
}
