using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem myPS = gameObject.GetComponent<ParticleSystem>();

        Destroy(gameObject, myPS.main.duration + myPS.main.startLifetime.constantMax);
    }
}
