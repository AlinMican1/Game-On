using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    
    float lerpTime = 0.2f;
    Vector3 RayHit;
    Transform BarrelTip;
    float StartTime;
    bool ready;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ready )
        {
            float percentageComplete = (Time.time - StartTime) / lerpTime;
            transform.position = Vector3.Lerp(BarrelTip.position, RayHit, percentageComplete);
            Debug.Log(percentageComplete);
            if (this.transform.position == RayHit) 
            {
                
                Destroy(this.gameObject);
            }
        }
        
    }

    public void bulletForce(Vector3 rayHit, Transform barrelTip, float startTime)
    {
        if (rayHit == Vector3.zero) 
        {
            RayHit = barrelTip.forward * 100f;

        }
        else
        {
            RayHit = rayHit;
        }
        
        BarrelTip = barrelTip;
        StartTime = startTime;
        ready = true;
    }

}
