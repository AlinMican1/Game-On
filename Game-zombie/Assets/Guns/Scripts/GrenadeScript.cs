using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GrenadeScript : MonoBehaviour
{
    [Header("Grenade")]
    public GameObject explosionEffect;
    public float delay = 3f;
    float countdown;
    bool hasEploded = false;
    public float explosionForce = 100f;
    public float radius = 20f;


    [Header("Scripts")]
    public MovePlayer Move_Player_Script;

    
    [Header("CameraShake")]
    public float ShakeRadius = 60f;
    public float total;
    public float ExplosionMagnitude = 1f;
    public float CameraRoughness = 4f;
    public float FadeInTime = 2f;
    public float FadeOutTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        Invoke("Explode", delay);
        Move_Player_Script = GameObject.FindObjectOfType<MovePlayer>();
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
        //Calculate Player distance from grenade explosion;
        float dist = Vector3.Distance(Move_Player_Script.GetPlayerCoordinates(), transform.position);
        total = ShakeRadius - dist;
        Debug.Log(total);
        if (total < 0)
        {
            CameraShaker.Instance.ShakeOnce(0f, 0f, 0f, 0f);
        }
        else
        {
            CameraShaker.Instance.ShakeOnce(total*ExplosionMagnitude, CameraRoughness, FadeInTime, FadeOutTime);
            /*if(total >= 80)
            {
                CameraShaker.Instance.ShakeOnce(15f, CameraRoughness, FadeInTime, FadeOutTime);
            }
            if(total >= 60)
            {
                CameraShaker.Instance.ShakeOnce(10f, CameraRoughness, FadeInTime, FadeOutTime);
            }
            if (total >= 40)
            {
                CameraShaker.Instance.ShakeOnce(8f, CameraRoughness, FadeInTime, FadeOutTime);
            }
            if (total >= 20)
            {
                CameraShaker.Instance.ShakeOnce(6f, CameraRoughness, FadeInTime, FadeOutTime);
            }

            else
            {
                if(total>= 15 && total <= 20)
                {
                    
                    CameraShaker.Instance.ShakeOnce(5.5f, CameraRoughness, FadeInTime, FadeOutTime);
                }
                else
                {
                    CameraShaker.Instance.ShakeOnce(total * ExplosionMagnitude, CameraRoughness, FadeInTime, FadeOutTime);
                }
                
            }*/

        }
        


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
