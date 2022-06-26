using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
     /*[Header("Weapons")]
     public List<GameObject> Weapons = new List<GameObject>();
     public KeyCode WeaponChange = KeyCode.Q;
     GameObject mygun;

     [Header("Scripts")]
     
     GunSystem Gun_System_Script;

     int i;
     void Start()
     {
         
         
         GameObject mygun = Instantiate(Weapons[0], this.transform);
         Gun_System_Script = mygun.GetComponent<GunSystem>();
         mygun.transform.SetParent(this.transform);
         i = 0;


     }

     // Update is called once per frame
     void Update()
     {
        
        if (Input.GetKeyDown(WeaponChange))
         {
             //Destroy weapon on change, checking for current weapon.
             Transform currentWeapon = transform.GetChild(0).GetComponent<Transform>();
             GameObject.Destroy(currentWeapon.gameObject);
             i++;
             //Checks if weapon is above the list count, if yes we set the i counter back to zero, otherwise we switch weapon.
             if(i + 1 > Weapons.Count)
             {
                 i = 0;
                 GameObject mygun = Instantiate(Weapons[i], this.transform);
                 Gun_System_Script = mygun.GetComponent<GunSystem>();
                 mygun.transform.SetParent(this.transform);

                 
             }
             else
             {
                 GameObject mygun = Instantiate(Weapons[i], this.transform);
                 Gun_System_Script = mygun.GetComponent<GunSystem>();
                 mygun.transform.SetParent(this.transform);
                
            }
             GetWeaponName();

         }
     }

     public string GetWeaponName()
     {
         return Weapons[i].name;
     }

  /* public int SelectedWeapon = 0;
    
    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousslectedWeapon = SelectedWeapon;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(SelectedWeapon >= transform.childCount - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if(previousslectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public string GetWeaponName()
    {
        return "hi";
    }*/



    
    int SelectedWeapon = 0;
    string SelectedWeaponName;

    private void Start()
    {
        for (int i = 1; i < this.transform.childCount; i++)
        {

            this.transform.GetChild(i).gameObject.SetActive(false);

        }
        SelectedWeaponName = this.transform.GetChild(0).name;
        currentActiveWeapon();

    }

    private void Update()
    {
        int previousWeapon = SelectedWeapon;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (SelectedWeapon >= 2)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
   
        }
        if(previousWeapon != SelectedWeapon)
        {
            DelayChangetime(0.5f);
            ChangeWeapons();

            currentActiveWeapon();
            GetWeaponName();
        }
     

    }
    public string GetWeaponName()
    {
        return SelectedWeaponName;
    }

    
    void ChangeWeapons()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                SelectedWeaponName = weapon.name;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
       
    }

    IEnumerator DelayChangetime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public int currentActiveWeapon()
    {
        return SelectedWeapon;
    }
}

