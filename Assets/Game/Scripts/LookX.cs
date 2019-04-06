using UnityEngine;

public class LookX : MonoBehaviour
{
    private const float Sensitivity = 1f;

    private void Update()
    {
        var _mouseX = Input.GetAxis("Mouse X");
        var newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * Sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
