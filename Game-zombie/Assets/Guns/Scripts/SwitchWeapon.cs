using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [Header("Weapons")]
    public List<GameObject> Weapons = new List<GameObject>();
    public KeyCode WeaponChange = KeyCode.Q;
    public GameObject mygun;

    
    int i;
    void Start()
    {
        GameObject mygun = Instantiate(Weapons[0], this.transform);
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
                mygun.transform.SetParent(this.transform);
                
            }
            else
            {
                GameObject mygun = Instantiate(Weapons[i], this.transform);
                mygun.transform.SetParent(this.transform);
                
                
            }
            GetWeaponName();

        }
    }

    public string GetWeaponName()
    {
        
        return Weapons[i].name;
    }
}
