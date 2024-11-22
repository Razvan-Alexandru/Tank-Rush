using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    public float maxSpeed;
    public float acceleration;
    public float friction;
    public float rotationSpeed;
    public float currentSpeed;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, currentSpeed, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S)) {
            currentSpeed -= acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, currentSpeed);
        }
        else {
            if (currentSpeed > 0)
            {
                currentSpeed -= friction * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += friction * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, 0f);
            }
        }

        transform.Translate(Vector3.forward * -currentSpeed * Time.deltaTime);

        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        currentSpeed = 0f;
    }
}
