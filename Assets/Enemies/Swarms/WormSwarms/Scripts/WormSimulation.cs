using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class WormSimulation : MonoBehaviour
{
    private static List<WormSimulation> AllWorms = new List<WormSimulation>();

    [Header("Worm")]
    public int PointCount = 6;

    public float PointSpacing = 0.1f;

    [Header("Gravity")]
    public Vector3 Gravity = new Vector3(0,0,0);
    
    public float GravityScale = 0.1f;

    public float Damping = 0.02f;

    public int Contstraints = 8;

    [Header("Swarm Tracking")]
    public Transform SwarmCenter;
    public float MoveForce = 8f;
    public float MaxSpeed = 2f;

    [Header("Collision")]
    public LayerMask World;

    [Header("Worm Separation")]
    public float SeparationRadius = 0.5f;
    public float SeparationStrength = 2f;

    [Header("Debug Features")]
    public float PointRadius = 0.05f;

    public Color PointColor = Color.red;
    public Color LineColor = Color.yellow;

    public Vector3[] Pos;
    private Vector3[] PrePos;

    private bool Initilized;

    public void Awake()
    {
        InitializeWorm();
    }

    public void InitializeWorm()
    {
        Pos = new Vector3[PointCount];
        PrePos = new Vector3[PointCount];

        for(int i = 0; i < PointCount; i++)
        {
            Vector3 Position = transform.position - transform.forward * (PointSpacing * i);
            Pos[i] = Position;
            PrePos[i] = Position;
        }
        Initilized = true;
    }

    public void FixedUpdate()
    {
        StimulateWorm();
    }

    public void StimulateWorm()
    {
        float Timer = Time.fixedDeltaTime;
        float TimerSquared = Timer * Timer;

        for(int i = 0; i < PointCount; i++)
        {
            Vector3 CurrentPosition = Pos[i];

            Vector3 Velosity = (Pos[i] - PrePos[i]) * (1f - Damping);

            if(i == 0 && SwarmCenter != null)
            {
                Vector3 Dir = SwarmCenter.position - Pos[0];

                if(Dir.sqrMagnitude > 0.01f)
                {
                    Dir.Normalize();

                    Velosity += Dir * MoveForce * Timer;
                }
            }

            if (i == 0)
            {
                Vector3 Separation = CalculateSeparation();

                Velosity += Separation * SeparationStrength * Timer;
            }

            Vector3 HoriVelosity = Vector3.ProjectOnPlane(Velosity, Vector3.up);
            
            if(HoriVelosity.magnitude > MaxSpeed)
            {
                HoriVelosity = HoriVelosity.normalized * MaxSpeed;

                Velosity = new Vector3(HoriVelosity.x, Velosity.y, HoriVelosity.z);
            }



            PrePos[i] = CurrentPosition;

            Pos[i] += Velosity + Gravity * GravityScale * TimerSquared;
        }

        //Pos[0] = transform.position;

        for (int i = 1; i < PointCount; i++)
        {
            Vector3 Diff = Pos[i] - Pos[i-1];

            float Dist = Diff.magnitude;

            if (Dist < 0.0001f)
            {
                continue;
            }

            float Error = Dist - PointSpacing;

            Vector3 Rearenger = Diff.normalized * Error;

            if(i == 1)
            {
                Pos[i] -= Rearenger;
            }
            else
            {
                Pos[i-1] += Rearenger * 0.5f;
                Pos[i] -= Rearenger * 0.5f;
            }
        }
        ResolveFloorCollision();
        transform.position = Pos[0];
    }

    private Vector3 CalculateSeparation()
    {
        Vector3 Separation = Vector3.zero;

        foreach (WormSimulation Other in AllWorms)
        {
            if (Other == null || Other == this)
                continue;

            if (Other.Pos == null || Other.PointCount < 1)
                continue;

            Vector3 Difference = Pos[0] - Other.Pos[0];

            float Distance = Difference.magnitude;

            if (Distance > 0.001f && Distance < SeparationRadius)
            {
                float Strength = 1f - Distance / SeparationRadius;

                Separation += Difference.normalized * Strength;
            }
        }
        return Vector3.ClampMagnitude(Separation, 1f);
    }

    //ChatGPT IM TIRED
    private void ResolveFloorCollision()
    {
        for (int i = 0; i < PointCount; i++)
        {
            if (Pos[i].y < PointRadius)
            {
                Pos[i].y = PointRadius;

                // Prevent downward velocity.
                PrePos[i].y = PointRadius;
            }
        }
    }

    private void OnEnable()
    {
        if (!AllWorms.Contains(this))
        {
            AllWorms.Add(this);
        }
    }

    private void OnDisable()
    {
        AllWorms.Remove(this);
    }

    private void OnDrawGizmos()
    {
        return;
        if (!Application.isPlaying || !Initilized)
            return;

        for (int i = 0; i < PointCount; i++)
        {
            Gizmos.color = i == 0 ? Color.cyan : PointColor;

            Gizmos.DrawSphere(
                Pos[i],
                PointRadius
            );

            if (i < PointCount - 1)
            {
                Gizmos.color = LineColor;

                Gizmos.DrawLine(
                    Pos[i],
                    Pos[i + 1]
                );
            }
        }
    }
}
