using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public bool isAreaDamage = false;
    private float areaRadius = 3f;
    private float lifespan = 5f;

    private Rigidbody rb;

    public GameObject hitParticles;
    

    void Start() {

        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifespan);
    }

    void Update() {
        if (rb.velocity != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    void OnTriggerEnter(Collider collider) {

        if(isAreaDamage) {
            AreaDamage();
        } 
        else {
            DirectDamage(collider);
        }

        Vector3 hitPoint = transform.position;
        ParticleManager.PlayParticles(hitParticles, hitPoint);

        Destroy(gameObject);
    }
    void AreaDamage() {
        
        HashSet<GameObject> hitObjects = new HashSet<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaRadius);
        foreach(Collider collider in colliders) {
            GameObject other = collider.transform.root.gameObject;

            if(!hitObjects.Contains(other)) {
                hitObjects.Add(other);
                

                if(other.CompareTag("Player")) {
                    other.GetComponent<Player>().TakeDamage(damage);
                    Camera.main.GetComponent<CameraController>().TriggerShake();
                }
                else if(other.CompareTag("Enemy")) {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }

    void DirectDamage(Collider collider) {
        
        HashSet<GameObject> hitObjects = new HashSet<GameObject>();
        GameObject other = collider.transform.root.gameObject;
        if(!hitObjects.Contains(other)) {
            
            hitObjects.Add(other);
            if(other.CompareTag("Player")) {
                other.GetComponent<Player>().TakeDamage(damage);
                Camera.main.GetComponent<CameraController>().TriggerShake();
            }
            else if(other.CompareTag("Enemy")) {

                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        
    }
}
