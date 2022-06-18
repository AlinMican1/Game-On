using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    Transform BarrelTip;
    Vector3 RayHit;
    float StartTime;
    bool ready;
    float timeElapsed;
    float LerpDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("destroyBullet", 10f);
    }
    public void Update()
    {
        if (ready)
            
        {
            if(timeElapsed < LerpDuration)
            {
                transform.position = Vector3.Lerp(BarrelTip.position, RayHit, timeElapsed / LerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.position = RayHit;
            }
           
           // if(this.transform.position == RayHit)
            //{
                
              //  Destroy(this.gameObject);
            //}
            
            
        }
        
        
    }
    // Update is called once per frame


    public void getRayHit(Vector3 rayhit, Transform barrelTip, float startTime)
    {
        if(rayhit == Vector3.zero)
        {
            RayHit = barrelTip.forward * 100f;
        }
        else
        {
            RayHit = rayhit;
        }
        StartTime = startTime;
        BarrelTip = barrelTip;
        ready = true;
    }
}
