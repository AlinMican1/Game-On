using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    
    float LerpTime = 1f;
    public Vector3 ADSoffSet;
    float StartTime;
    GunSystem Aim_Down_Sight_Script;
    public float LerpSpeed = 1f;
    Transform ADSPosition;
    public Vector3 StartingPositionOffSet;
    Transform WeaponPosition;

    // Start is called before the first frame update
    void Start()
    {

        Aim_Down_Sight_Script = this.GetComponent<GunSystem>();
        WeaponPosition = transform.parent.GetComponent<Transform>();
        ADSPosition = transform.parent.parent.GetChild(2).GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
        StartTime = Aim_Down_Sight_Script.BeginTime;

        if (Aim_Down_Sight_Script.AimingDownSight() == true && transform.position != transform.parent.parent.GetChild(2).position + ADSoffSet)
        {

            float PercentageComplete = (Time.time - StartTime) / LerpTime * LerpSpeed;
            
            transform.position = Vector3.Lerp(WeaponPosition.position + transform.right * StartingPositionOffSet.x + transform.up * StartingPositionOffSet.y + transform.forward * StartingPositionOffSet.z, transform.parent.parent.GetChild(2).position + transform.right * ADSoffSet.x + transform.up * ADSoffSet.y + transform.forward * ADSoffSet.z, PercentageComplete);
           
        }
        else if(Aim_Down_Sight_Script.AimingDownSight() == false && transform.position != WeaponPosition.position)  
        {
            
            float PercentageComplete = (Time.time - StartTime) / LerpTime* LerpSpeed;

            transform.position = Vector3.Lerp(transform.parent.parent.GetChild(2).position + transform.right * ADSoffSet.x + transform.up * ADSoffSet.y + transform.forward * ADSoffSet.z, WeaponPosition.position +  transform.right * StartingPositionOffSet.x + transform.up * StartingPositionOffSet.y + transform.forward * StartingPositionOffSet.z, PercentageComplete);

            
        }
    }
}
