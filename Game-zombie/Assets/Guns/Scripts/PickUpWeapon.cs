using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [Header("Scripts")]
    Get_Weapon_Name Weapon_Name_Script;
    MovePlayer Move_Player_Script;
    SwitchWeapon Switch_Weapon_Script;
    
    [Header("Variables")]
    public Text MyText;
    public LayerMask Pickable_Weapon;
    string weaponNameSwitched;
    
    // Start is called before the first frame update
    void Start()
    {

        Weapon_Name_Script = GameObject.FindObjectOfType<Get_Weapon_Name>();
        Move_Player_Script = GameObject.FindObjectOfType<MovePlayer>();
        Switch_Weapon_Script = GameObject.FindObjectOfType<SwitchWeapon>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make a ray to get the tag of the weapon

        
        
        PickUpItemText();
        PickUpItem();
    }

    void PickUpItemText()
    {
        if (Physics.OverlapSphere(Move_Player_Script.GetPlayerCoordinates(),2,Pickable_Weapon).Length > 0)
        {
            MyText.enabled = true;
           
        }
        else
        {
            MyText.enabled = false;
        }
    }

    void PickUpItem()
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.OverlapSphere(Move_Player_Script.GetPlayerCoordinates(), 2, Pickable_Weapon).Length > 0)
        {
            //if(r)
            if (Physics.Raycast(ray, out hit))
            {
                weaponNameSwitched = hit.collider.name;
                if (hit.transform.gameObject.tag == "Pickable_Weapon")
                {
                    Weapon_Name_Script = hit.transform.GetComponent<Get_Weapon_Name>();

                    for (int i = 0; i<3; i++)
                    {
                        if (transform.GetChild(i).name == Weapon_Name_Script.GetWeaponName()) 
                        {
                            return;
                        }
                        
                    }
                    if (Input.GetKeyDown(KeyCode.E)) 
                    {
                        foreach(Transform weapons in this.transform)
                        {
                            if (weapons.name == Weapon_Name_Script.GetWeaponName())
                            {
                                weapons.gameObject.SetActive(true);
                                int x = weapons.GetSiblingIndex();
                                int y = Switch_Weapon_Script.currentActiveWeapon();
                                GameObject DisableWeapon = transform.GetChild(y).gameObject;
                                transform.GetChild(y).SetSiblingIndex(x);
                                weapons.SetSiblingIndex(y);
                                DisableWeapon.SetActive(false);
                                
                                
                            }
                        }


                        MyText.enabled = false;
                    }
                }
                else
                {
                    hit.transform.gameObject.tag = hit.transform.gameObject.tag;
                }
            }
        }
    }
    

}
