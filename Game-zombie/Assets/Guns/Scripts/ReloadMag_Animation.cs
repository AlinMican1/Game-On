using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadMag_Animation : MonoBehaviour
{
    public Animator Reload_Anim;
    public GunSystem Gun_System_Script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gun_System_Script.Reload_Anim == true)
        {
            Reload_Anim.SetBool("Reload", true);
        }
        else
        {
            Reload_Anim.SetBool("Reload", false);
        }
    }
}
