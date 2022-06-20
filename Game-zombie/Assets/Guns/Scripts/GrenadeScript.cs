using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 3f;
    float countdown;
    bool hasEploded = false;

    public float explosionForce = 100f;
    public float radius = 20f;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        Invoke("Explode", delay);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasEploded)
        {
            Explode();
            hasEploded = true;
        }
    }

    private void Explode()
    {

        //show effect
        GameObject ExplosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get Nearby Objects;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
            //Add damage - check berkleys
        }

        //Remove Greande
        Destroy(gameObject);
        Destroy(ExplosionEffect, 2f);
    }

    
}
