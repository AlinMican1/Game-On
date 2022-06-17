using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Transform otherLeg;
    Legs otherLegScript;

    [Header("Positions")]
    Vector3 oldPos;
    Vector3 newPos;
    Vector3 oldPosYOffset;
    Vector3 newPosYOffset;

    [Header("Raycast")]
    [SerializeField] float TargetYoffset;
    RaycastHit hit;

    [Header("Dist")]
    [SerializeField] float tooFarDist;

    [Header("Bools")]
    public bool isMoving;
    public bool hasMoved;
    public bool tooFar;

    [Header("Lerp")]
    [SerializeField] float stepHeightx2;
    [SerializeField] float lerpTime;
    float startLerpTime;

    private void Start()
    {
        otherLegScript = otherLeg.GetComponent<Legs>();

        if (Physics.Raycast(transform.position, -transform.root.up, out hit, 100))
        {
            Target.position = hit.point + (hit.normal * TargetYoffset);
            oldPos = hit.point;
            newPos = oldPos;

            oldPosYOffset = oldPos + (hit.normal * TargetYoffset);
            newPosYOffset = oldPosYOffset;
        }
    }

    private void Update()
    {
        oldPosYOffset = oldPos + (hit.normal * TargetYoffset);
        newPosYOffset = newPos + (hit.normal * TargetYoffset);

        Physics.Raycast(transform.position, -transform.root.up, out hit, 100);

        if (Vector3.Distance(hit.point, oldPos) > tooFarDist && !isMoving)
        {
            newPos = hit.point;
            tooFar = true;
            startLerpTime = Time.time;
        }

        if (oldPos != newPos)
        {
            print("lerping");

            isMoving = true;
            hasMoved = false;

            Vector3 halfPoint = (oldPosYOffset + ((newPosYOffset - oldPosYOffset)/2) + (transform.root.up * stepHeightx2));

            float percentageComplete = (Time.time - startLerpTime) / lerpTime;

            Vector3 lerpA = Vector3.Lerp(oldPosYOffset, halfPoint, percentageComplete);
            Vector3 lerpB = Vector3.Lerp(halfPoint, newPosYOffset, percentageComplete);

            Vector3 lerpC = Vector3.Lerp(lerpA, lerpB, percentageComplete);

            Target.position = lerpC;

            if (Target.position == newPosYOffset)
            {
                print("done lerping");
                isMoving = false;
                hasMoved = true;

                oldPos = newPos;
            }
        }
    }
}
