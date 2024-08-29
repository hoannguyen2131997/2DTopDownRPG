using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class SwordPlayer : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private WeaponInfo weaponInfo;

    private PlayerInputActions inputActions;
    private Animator m_Animator;
    private Transform weaponCollider;

    private bool attackButtonDown, isAttacking = false;

    private GameObject slashAnim;
    private Vector2 inputPlayer;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = Character.Instance.GetWeaponCollider();
       
        inputPlayer = GameInputSystemSingleton.Instance.GetMovementVectorNormalized();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private Vector3 rotationFlash;
    public void Attack()
    {
        m_Animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.Euler(rotationFlash));
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DontAttack()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        WeaponFollowPlayer();
    }

    private void WeaponFollowPlayer()
    {
        Vector2 inputPlayer = GameInputSystemSingleton.Instance.GetMovementVectorNormalized();

        if (inputPlayer.x > 0)
        {
            // Right
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 45);
            weaponCollider.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationFlash = new Vector3(0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if (inputPlayer.x < 0)
        {
            // Left order 1
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, -225);
            weaponCollider.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            rotationFlash = new Vector3(0, 0, -180);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (inputPlayer.y > 0)
        {
            // Up - order 1
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,135);
            weaponCollider.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            rotationFlash = new Vector3(0, 0, 90);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        } 
        else if (inputPlayer.y < 0)
        {
            // Down
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,-135);
            weaponCollider.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
            rotationFlash = new Vector3(0, 0, -90);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (Character.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Character.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
