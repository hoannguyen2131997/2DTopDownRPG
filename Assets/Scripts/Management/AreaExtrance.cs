using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExtrance : MonoBehaviour
{
    [SerializeField] private string transtionName;

    private void Start()
    {
        if(transtionName == SceneManagement.Instance.SceneTransitionName)
        {
            Character.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
