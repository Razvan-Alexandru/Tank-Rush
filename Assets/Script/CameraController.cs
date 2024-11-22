using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform tank;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float rotationSpeed = 100.0f;

    private float horizontalAngle;
    private float verticalAngle;
    private float minVerticalAngle = -10.0f;
    private float maxVerticalAngle = 20.0f;

    public float shakeDuration;
    public float shakeMagnitude;
    public float dampingSpeed = 1.0f;
    private bool isShaking = false;

    private Vector3 initialPosition;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        horizontalAngle = Mathf.Atan2(-tank.forward.x, -tank.forward.z) * Mathf.Rad2Deg;
        verticalAngle = 0.55f;
        initialPosition = transform.localPosition;

        Vector3 offset = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);
        transform.position = tank.position + offset + Vector3.up * height;

        transform.LookAt(tank.position + Vector3.up * height);
    }

    void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        horizontalAngle += mouseX;

        verticalAngle -= mouseY;
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);

        Vector3 offset = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);
        transform.position = tank.position + offset + Vector3.up * height;
        
        
        if(isShaking) {
            transform.position += Random.insideUnitSphere * shakeMagnitude;
        }

        transform.LookAt(tank.position + Vector3.up * height);
    }

    public void TriggerShake () {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake() {
        isShaking = true;
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }
}
