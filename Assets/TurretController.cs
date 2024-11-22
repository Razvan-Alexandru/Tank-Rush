using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform cameraTransform;
    public float rotationSpeed = 3f;

    void Update() {
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
