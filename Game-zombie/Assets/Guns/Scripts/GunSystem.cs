using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    [Header("Weapons")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    
    
    [Header("Scripts")]
    public Recoil Recoil_Script;
    public Recoil Gun_Recoil_Script;
    public BulletTrail BulletTrail_Script;
    

    [Header("Reference")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    


    [Header("Graphics")]
    public ParticleSystem muzzleFlash; 
    public GameObject bulletHoleGraphics;
    public TextMeshProUGUI text;
    public GameObject bulletPrefab;
    public Transform barrelTip;
    public Light PointLight;
    

    [Space(10)]
    [SerializeField] LayerMask IgnoreMask = 1 << 10  | 1 << 11;    

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        

        //Get the recoil script, text and fpsCam from hiearchy
        Recoil_Script = transform.root.GetChild(1).GetChild(0).GetComponent<Recoil>();
        text = GameObject.Find("Canvas/weaponAmmoTXT").GetComponent<TextMeshProUGUI>();
        fpsCam = transform.root.GetChild(1).GetChild(0).GetChild(0).GetComponent<Camera>();
        Gun_Recoil_Script = transform.GetComponent<Recoil>();
        
    }

    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    //Inputs for shooting and reloading weapon
    private void MyInput(){
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) {
            Reload();
        }
        
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void Shoot()
    {
        
        readyToShoot = false;
        
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        
        
        //RayCast
        if(Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, ~IgnoreMask))
        {
            Debug.Log(rayHit.collider.name);
            //enemy will need tag Enemy, and needs takeDamage function
            //if (rayHit.collider.CompareTag("Enemy"))
            //{
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
            //}
        }

        //ShakeCamera
        Recoil_Script.RecoilFire();
        Gun_Recoil_Script.RecoilFire();
        //Graphics
        if(Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, ~IgnoreMask))
        {
            GameObject bulletHole = Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.identity);
            bulletHole.transform.rotation = Quaternion.FromToRotation(bulletHole.transform.forward, rayHit.normal);
        }
        

        //GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, barrelTip.rotation);
        //BulletTrail_Script = bullet.GetComponent<BulletTrail>();
        //float startTime = Time.time;
        //BulletTrail_Script.bulletForce(rayHit.point, barrelTip, startTime);

        //Play muzzleflash animation
        muzzleFlash.Play();
        
        


        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    
}
