using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelManager : MonoBehaviour
{
    [SerializeField] float weaponWheelTransitionlerpTime = 1;
    [SerializeField] KeyCode weaponwheelShow = KeyCode.Tab;

    [SerializeField] GameObject weaponWheel;

    public bool wheelInteractable;
    MouseLook playerMouseLookScript;

    List<Transform> WWPieces = new List<Transform>();

    Vector3 miniForm = new Vector3(0.5f, 0.5f);
    Vector3 normalForm = new Vector3(8, 8);

    float enterStartTime;
    float exitStartTime;

    float enterPercentage;
    float exitPercentage;

    bool enterLerp;
    bool exitLerp;

    bool doneEnterLerp;
    bool doneExitLerp;
     
    [SerializeField] float angle;
    [SerializeField] Transform test;

    void Awake()
    {
        playerMouseLookScript = GameObject.Find("FPS Player").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<MouseLook>();

        for(int i = 0; i < weaponWheel.transform.childCount; i++)
        {
            WWPieces.Add(weaponWheel.transform.GetChild(i).GetComponent<Transform>());
        }
    }

    void Update()
    {
        if (playerMouseLookScript == null)
        {
            playerMouseLookScript = GameObject.Find("FPS Player").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<MouseLook>();
        }

        ShowWeaponWheel();

        if (enterLerp)
        {
            enterPercentage = (Time.time - enterStartTime) / weaponWheelTransitionlerpTime;

            weaponWheel.transform.localScale = Vector3.Lerp(miniForm, normalForm, enterPercentage);
        }

        if (enterPercentage > 1)
        {
            enterLerp = false;
            enterPercentage = 0;
            doneEnterLerp = true;
        }


        if (exitLerp)
        {
            exitPercentage = (Time.time - exitStartTime) / weaponWheelTransitionlerpTime;

            weaponWheel.transform.localScale = Vector3.Lerp(normalForm, miniForm, exitPercentage);
        }

        if (exitPercentage > 1)
        {
            exitLerp = false;
            exitPercentage = 0;
            weaponWheel.SetActive(false);
            doneExitLerp = true;
        }

        // While holding tab
        if (Input.GetKey(weaponwheelShow) && (doneExitLerp || doneEnterLerp))
        {
            wheelInteractable = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            playerMouseLookScript.enabled = false;

            Vector3 mousePos = Input.mousePosition - transform.position;
            Debug.DrawLine(transform.position, mousePos);

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        }
        else
        {
            wheelInteractable = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerMouseLookScript.enabled = true;
        }
    }

    public GameObject WhichPiece()
    {
        if (angle > 67.5 && angle < 112.5)
        {
            return WWPieces[0].gameObject;
        }
        else if (angle > 112.5 && angle < 157.5)
        {
            return WWPieces[1].gameObject;
        }
        else if (angle > 157.5 && angle < 180)
        {
            return WWPieces[2].gameObject;
        }
        else if (angle > -180 && angle < -157.5)
        {
            return WWPieces[2].gameObject;
        }
        else if (angle > -157.5 && angle < -112.5)
        {
            return WWPieces[3].gameObject;
        }
        else if (angle > -112.5 && angle < -67.5)
        {
            return WWPieces[4].gameObject;
        }
        else if (angle > -67.5 && angle < -22.5)
        {
            return WWPieces[5].gameObject;
        }
        else if (angle > -22.5 && angle < 22.5)
        {
            return WWPieces[6].gameObject;
        }
        else if (angle > 22.5 && angle < 67.5)
        {
            return WWPieces[7].gameObject;
        }

        return null;
    }

    void ShowWeaponWheel()
    {
        if (Input.GetKeyDown(weaponwheelShow))
        {
            doneEnterLerp = false;
            weaponWheel.SetActive(true);
            enterStartTime = Time.time;
            enterLerp = true;
        }

        if (Input.GetKeyUp(weaponwheelShow))
        {
            doneExitLerp = false;
            exitStartTime = Time.time;
            exitLerp = true;
        }
    }
}
