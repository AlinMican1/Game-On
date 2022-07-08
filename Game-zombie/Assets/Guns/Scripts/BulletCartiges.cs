using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCartiges : MonoBehaviour
{
    public GameObject Cartige;
    float StartTime;
    [SerializeField] GunSystem Gun_System_Script;
    public Transform StartPosition;
    public Transform EndPosition;
    float LerpTime = 1f;
    public float LerpSpeed = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        Gun_System_Script = gameObject.GetComponent<GunSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        StartTime = Gun_System_Script.BeginTime;
    }

    public void CartiageLerp()
    {
       float PercentageComplete = (Time.time - StartTime) / LerpTime * LerpSpeed;

       Vector3.Lerp(StartPosition.position, EndPosition.position, PercentageComplete);
    }
}
