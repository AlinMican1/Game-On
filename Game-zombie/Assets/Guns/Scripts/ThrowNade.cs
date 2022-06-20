using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowNade : MonoBehaviour
{
    public float throwForce = 1100f;
    public GameObject grenadePrefab;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Launch();
        }
    }
    private void Launch()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
    }
}
