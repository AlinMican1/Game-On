using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{

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

