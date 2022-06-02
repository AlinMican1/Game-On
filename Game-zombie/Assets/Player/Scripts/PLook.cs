using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLook : MonoBehaviour
{
    [Header("Sense")]
    public float Xsens;
    public float Ysens;

    [Space(10)]
    public Transform orientation;

    float xRot;
    float yRot;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        orientation = transform.root.GetChild(1).GetComponent<Transform>();
    }

    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Xsens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Ysens;

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        // rotate cam and player
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
