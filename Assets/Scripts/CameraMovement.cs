using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 100.0f;
    public Transform playerTransform;

    private float xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float horizontalRotation = mouseX * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(
            xRotation - mouseY * sensitivity * Time.deltaTime, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

        playerTransform.Rotate(Vector3.up * horizontalRotation);
    }
}
