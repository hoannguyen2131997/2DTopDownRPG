using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public CanvasGroup joystickCanvasGroup; // CanvasGroup để kiểm soát độ trong suốt
    public float fadeSpeed = 1f; // Tốc độ làm mờ
    public float radiusLimit = 100f; // Bán kính di chuyển của handle

    private Vector2 _joystickStartPos;

    private void Start()
    {
        _joystickStartPos = joystickBackground.position;
        joystickCanvasGroup.alpha = 0.5f; // Thiết lập mức mờ ban đầu
    }

    public void OnJoystickMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            // Hiện joystick rõ hơn khi sử dụng
            StopAllCoroutines();
            joystickCanvasGroup.alpha = 1f;

            Vector2 inputPosition = context.ReadValue<Vector2>();
            Vector2 offset = inputPosition - _joystickStartPos;

            if (offset.magnitude > radiusLimit)
            {
                offset = offset.normalized * radiusLimit;
            }

            joystickHandle.position = _joystickStartPos + offset;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            joystickHandle.position = _joystickStartPos;
            StartCoroutine(FadeOutJoystick()); // Bắt đầu hiệu ứng làm mờ khi dừng joystick
        }
    }

    private IEnumerator FadeOutJoystick()
    {
        while (joystickCanvasGroup.alpha > 0.5f)
        {
            joystickCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    // Hiển thị bán kính joystick trên Scene View
    private void OnDrawGizmosSelected()
    {
        if (joystickBackground != null)
        {
            Gizmos.color = Color.green; // Màu của vòng tròn hiển thị
            Vector3 position = joystickBackground.position;

            // Vẽ một vòng tròn với bán kính joystickRadius
            Gizmos.DrawWireSphere(position, radiusLimit);
        }
    }
}
