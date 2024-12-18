using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Transform target;

    public Vector3 offset;

    void Update() {
        transform.parent.rotation = Camera.main.transform.rotation;
        //transform.position = target.position + offset;
    }

    public void UpdateHealth(float currentHealth, float maxHealth) {
        slider.value = currentHealth / maxHealth;
    }
}
