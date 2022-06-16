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

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;


        //Calculate Angular rotation

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        float moveZ = Input.GetAxisRaw("Vertical") * MoveSwayMultiplier;
        float moveX = Input.GetAxisRaw("Horizontal") * MoveSwayMultiplier;

        Quaternion MoverotationZ = Quaternion.AngleAxis(-moveX, Vector3.up);
        Quaternion MoverotationX = Quaternion.AngleAxis(moveZ, Vector3.right);

        Quaternion MovetargetRotation = MoverotationZ * MoverotationX;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, MovetargetRotation, MoveSmooth * Time.deltaTime);
    }
}
