using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IKScript : MonoBehaviour
{
    #region Source of Code
    //https://www.youtube.com/watch?v=qqOAzn05fvk
    #endregion

    // Chain Length of Bones
    public int ChainLength;

    //Target the chain should bend to
    public Transform Target;
    public Transform Pole;

    [Header("Solver Parameters")]
    public int Iterations = 10;
    public float Delta = 0.001f;

    protected float[] BonesLength;
    protected float CompleteLength;
    protected Transform[] Bones;
    protected Vector3[] Positions;
    protected Vector3[] StartDirectionSucc;
    protected Quaternion[] StartRotationBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StartRotationRoot;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        // initial array
        Bones = new Transform[ChainLength + 1];
        Positions = new Vector3[ChainLength + 1];
        BonesLength = new float[ChainLength];
        StartDirectionSucc = new Vector3[ChainLength + 1];
        StartRotationBone = new Quaternion[ChainLength + 1];

        /*
        CURRENTLY NOT USING THE CODE BELOW AND MANUAL ASSIGNING TO PREFAB

        if (Target == null)
        {
            Target = new GameObject(gameObject.transform.parent.parent.parent.parent.parent.name + "Target").transform;
            Target.position = transform.position;
        }
        */

        StartRotationTarget = Target.rotation;
        CompleteLength = 0;

        //data for init
        var current = transform;
        for (var i = Bones.Length - 1; i >= 0; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;

            if(i == Bones.Length - 1) // find leaf bones (last bone)
            {
                // leaf
                StartDirectionSucc[i] = Target.position - current.position;
            }
            else
            {
                //mid bone
                StartDirectionSucc[i] = Bones[i + 1].position - current.position;
                BonesLength[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLength += BonesLength[i];
            }

            current = current.parent;
        }
    }

    private void LateUpdate()
    {
        ResolveIK();
    }

    private void ResolveIK()
    {
        if (Target == null)
        {
            return;
        }

        if (BonesLength.Length != ChainLength)
        {
            Init();
        }


        /* Visualisation below
           
           (Bone 0) <---(BoneLen 0)----> (Bone 1) <---(BoneLen 1)---> (Bone 2) ...
           x-----------------------------x-----------------------------x------ ...

           End of Visualisation
        */


        // get positions
        for (int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }

        var RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var RootRotDiff = RootRot * Quaternion.Inverse(StartRotationRoot);

        // calculations
        // check if target is further away than complete length in which case bones are in a straight line
        if ((Target.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
        {
            // make bones point to target in straight line
            var direction = (Target.position - Positions[0]).normalized;
            //set points for other bones using bonelength (distance between bones which is all the same in this case)
            for (int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = Positions[i - 1] + direction * BonesLength[i - 1];
            }
        }
        else
        {
            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                // back (tip/target to root)
                for (int i = Positions.Length - 1; i > 0; i--)
                {
                    if (i == Positions.Length - 1)
                    {
                        Positions[i] = Target.position; // set last bone to target position
                    }
                    else
                    {
                        Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BonesLength[i];
                    }
                }

                //forward (root to tip since root has to stay in place)
                for (int i = 1; i < Positions.Length; i++)
                {
                    Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i - 1]).normalized * BonesLength[i - 1];
                }


                // is tip bone close to the target?
                if((Positions[Positions.Length - 1] - Target.position).sqrMagnitude < Delta * Delta)
                {
                    break;
                }

            }
        }

        if (Pole != null)
        {
            for (int i = 1; i < Positions.Length - 1; i++)
            {
                var plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                var projectedPole = plane.ClosestPointOnPlane(Pole.position);
                var projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i - 1]) + Positions[i - 1];
            }
        }

        // set positions
        for(int i = 0; i < Positions.Length; i++)
        {
            if(i == Positions.Length - 1)
            {
                Bones[i].rotation = Target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            }
            else
            {
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionSucc[i], Positions[i + 1] - Positions[i]) * StartRotationBone[i];
            }

            Bones[i].position = Positions[i];
        }
    }

    private void OnDrawGizmos()
    {
        var current = this.transform;
        for(int i = 0; i < ChainLength && current != null && current.parent != null; i++)
        {
            #region Editor Stuff
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
            #endregion
        }
    }
}
