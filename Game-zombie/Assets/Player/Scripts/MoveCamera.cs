using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Start()
    {
        cameraPosition = GameObject.Find("CameraPos").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
