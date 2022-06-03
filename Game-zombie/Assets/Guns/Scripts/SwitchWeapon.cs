using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();
    public KeyCode WeaponChange = KeyCode.Q;
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
            Transform currentWeapon = transform.GetChild(0).GetComponent<Transform>();
            GameObject.Destroy(currentWeapon.gameObject);
            i++;
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
        }
    }
}
