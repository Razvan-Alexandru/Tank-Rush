using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingDotController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform turret;
   
   private RectTransform dotRectTransform;

   void Start() {
    dotRectTransform = GetComponent<RectTransform>();
   }

    void Update() {
        Vector3 aimDirection = -turret.forward;
        Vector3 aimPoint = turret.position + aimDirection * 100f;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(aimPoint);

        dotRectTransform.position = screenPoint;
    }
}
