using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnimation : MonoBehaviour
{

    public Animator Shoot_Anim;
    [SerializeField] GunSystem Gun_System_Script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Gun_System_Script.Pistol_shoot_Anim == true)
        {
            Shoot_Anim.SetTrigger("Shoot");
        }
        else
        {
            Shoot_Anim.ResetTrigger("Shoot");
        }
    }
}
