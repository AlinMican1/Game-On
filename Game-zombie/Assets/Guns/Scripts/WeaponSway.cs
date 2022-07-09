using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    [SerializeField] private float MoveSwayMultiplier;
    [SerializeField] private float MoveSmooth;
    // Start is called before the first frame update
    GunSystem Aim_Down;
    float mouseX;
    float mouseY;
    float moveZ;
    float moveX;
    public GameObject weaponHolder;
    private void Start()
    {
        Aim_Down = GameObject.FindObjectOfType<GunSystem>();
    }
    // Update is called once per frame
    private void Update()
    {
       for (int i = 0; i < 3; i++)
        {
            if (weaponHolder.transform.GetChild(i).gameObject.activeSelf == true)
            {
                Aim_Down = weaponHolder.transform.GetChild(i).GetComponent<GunSystem>();
            }
        }
        
        if(Aim_Down.AimingDownSight() == true)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier * 0.5f;
            mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier * 0.5f;
        }
        else
        {
            mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
        }
       


        //Calculate Angular rotation

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);


        //Moving Sway
        if(Aim_Down.AimingDownSight() == true)
        {
            moveZ = Input.GetAxisRaw("Vertical") * MoveSwayMultiplier * 0.5f;
            moveX = Input.GetAxisRaw("Horizontal") * MoveSwayMultiplier  * 0.5f;
        }
        else
        {
            moveZ = Input.GetAxisRaw("Vertical") * MoveSwayMultiplier;
            moveX = Input.GetAxisRaw("Horizontal") * MoveSwayMultiplier;
        }
     
        Quaternion MoverotationZ = Quaternion.AngleAxis(-moveX, Vector3.up);
        Quaternion MoverotationX = Quaternion.AngleAxis(moveZ, Vector3.right);

        Quaternion MovetargetRotation = MoverotationZ * MoverotationX;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, MovetargetRotation, MoveSmooth * Time.deltaTime);
    }
}
