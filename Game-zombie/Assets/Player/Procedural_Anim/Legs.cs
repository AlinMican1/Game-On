using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public Transform Target;

    [Header("Positions")]
    Vector3 oldPos;
    Vector3 newPos;

    [Header("Raycast")]
    [SerializeField] float RayOffset;
    [SerializeField] float TargetRayOffset;
    [SerializeField] LayerMask ignorePlayer;
    RaycastHit hit;

    void Start()
    {
        InfoRay();
        Target.position = hit.point + (transform.root.up * TargetRayOffset);
    }

    void Update()
    {
        InfoRay();
    }

    void InfoRay()
    {
        Debug.DrawRay(transform.position + (transform.root.forward * RayOffset), -transform.root.up * 100, Color.red);
        Physics.Raycast(transform.position + (transform.root.forward * RayOffset), -transform.root.up, out hit, 100, ~ignorePlayer);

        Target.position = hit.point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, 0.01f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(hit.point + (transform.root.up * TargetRayOffset), 0.01f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Target.position, 0.05f);
    }
}
