using UnityEngine;

public class Recoil : MonoBehaviour
{

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    string gunName;
    [SerializeField] Transform weaponHolder;

    SwitchWeapon Switch_Weapon_Script;
    PickUpWeapon Pick_Up_Weapon_Script;
    GunSystem Gun_System_Script;
    // Start is called before the first frame update
    void Start()
    {
        Switch_Weapon_Script = GameObject.FindObjectOfType<SwitchWeapon>();
        Pick_Up_Weapon_Script = GameObject.FindObjectOfType<PickUpWeapon>();
        Gun_System_Script = this.GetComponent<GunSystem>();

        weaponHolder = GameObject.Find("WeaponHolder").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        gunName = Switch_Weapon_Script.GetWeaponName();


        for (int i = 0; i < 3; i++)
        {
            if (weaponHolder.GetChild(i).gameObject.activeSelf == true)
            {
                gunName = weaponHolder.GetChild(i).gameObject.name;

            }
        }
        ChangeRecoilBasedOnGun(gunName);
        
    }

    //Adds recoil when fired, in the x,y,z direction.
    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

    private void ChangeRecoilBasedOnGun(string gunName)
    {
        
        if (gunName == "Shotgun")
        {
            
            if(Gun_System_Script.AimingDownSight() == true)
            {
                recoilX = -1;
                recoilY = 1;
                recoilZ = 0.35f;

                snappiness = 6;
                returnSpeed = 8;
            }
            else if(Gun_System_Script.AimingDownSight() == false)
            {
                recoilX = -2;
                recoilY = 3;
                recoilZ = 0.35f;

                snappiness = 6;
                returnSpeed = 8;
            }

        }
        if (gunName == "AR0")
        {
            
            
            if (Gun_System_Script.AimingDownSight() == true)
            {
                recoilX = -1;
                recoilY = 1;
                recoilZ = 0.35f;

                snappiness = 6;
                returnSpeed = 8;
            }
            else if (Gun_System_Script.AimingDownSight() == false)
            {
                recoilX = -3;
                recoilY = 1;
                recoilZ = 0.35f;

                snappiness = 3;
                returnSpeed = 8;
            }

        }
        if (gunName == "ak")
        {
            

            if (Gun_System_Script.AimingDownSight() == true)
            {
                recoilX = -1;
                recoilY = 1;
                recoilZ = 0.35f;

                snappiness = 6;
                returnSpeed = 8;
            }
            else if (Gun_System_Script.AimingDownSight() == false)
            {
                recoilX = -3;
                recoilY = 2;
                recoilZ = 0.35f;

                snappiness = 3;
                returnSpeed = 8;
            }

        }
    }
        
       
    
}
