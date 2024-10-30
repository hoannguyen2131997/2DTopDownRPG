using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StandingState;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem.XR;

public class WalkState : IMovementState
{
    private Rigidbody2D _rbPlayer;
    private Animator _aniPlayer;
    private Vector2 _movePlayer;
    private float _speedPlayer;
    public void EnterState(StateController controller)
    {
        Debug.Log("Entering Walk State");
    }

    private void GetDataPlayer(StateController controller)
    {
        if (_rbPlayer == null) _rbPlayer = controller.GetRbPlayer();
        if (_aniPlayer == null) _aniPlayer = controller.GetAniPlayer();
        if (_speedPlayer == 0) _speedPlayer = controller.GetPlayerData().SpeedPlayer;

        _movePlayer = controller.GetPlayerInputHandler().GetInputPlayer();
    }

    public void FixedUpdateState(StateController controller)
    {
        GetDataPlayer(controller);
        _rbPlayer.velocity = _movePlayer * _speedPlayer;          //Translate(_movePlayer * _speedPlayer * Time.deltaTime);
       
        _aniPlayer.SetFloat(KeyPlayer._horizontal, _movePlayer.x);
        _aniPlayer.SetFloat(KeyPlayer._vertical, _movePlayer.y);

        if (_movePlayer != Vector2.zero)
        {
            _aniPlayer.SetFloat(KeyPlayer._lastHorizontal, _movePlayer.x);
            _aniPlayer.SetFloat(KeyPlayer._lastVertical, _movePlayer.y);
        } else
        {
            controller.TransitionToMovementState(new IdleState());
        }
    }

    public void ExitState(StateController controller)
    {
        Debug.Log("Exiting Walk State");
    }
}