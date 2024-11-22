using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    private Transform player;
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
