using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    public Recoil Recoil_Script;
    public Recoil Gun_Recoil_Script;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public ParticleSystem muzzleFlash; 
    public GameObject bulletHoleGraphics;
    public TextMeshProUGUI text;
    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

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
        muzzleFlash.Play();
        readyToShoot = false;
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        Debug.DrawRay(fpsCam.transform.position, direction);
        //RayCast
        if(Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range /*whatIsEnemy*/ ))
        {
            Debug.Log(rayHit.collider.name);
            //enemy will need tag Enemy, and needs takeDamage function
            //if (rayHit.collider.CompareTag("Enemy"))
            //{
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
            //}
        }

        //ShakeCamera
        //Graphics
        
        Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.Euler(0, -180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Recoil_Script.RecoilFire();
        Gun_Recoil_Script.RecoilFire();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rayHit.point, 0.075f);
    }
}
