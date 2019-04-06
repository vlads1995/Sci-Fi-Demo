using UnityEngine;

public class LookY : MonoBehaviour
{
    private const float Sensitivity = 1f;

    private void Update()
    {
        var _mouseY = Input.GetAxis("Mouse Y");
        var newRotation = transform.localEulerAngles;       
        newRotation.x -= _mouseY * Sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
