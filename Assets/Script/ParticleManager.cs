using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static void PlayParticles(GameObject particle, Vector3 position, float scale=1f) {
        if(particle != null && position != null) {
            GameObject particleInstance = Instantiate(particle, position, Quaternion.identity);
            particleInstance.transform.localScale *= scale;
            ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
            if(particleInstance != null) {
                particleSystem.Play();
            }
            Destroy(particleInstance, particleSystem.main.duration);
        }
    }
}
