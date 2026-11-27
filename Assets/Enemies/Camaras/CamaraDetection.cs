using UnityEngine;

public class CamaraDetection : MonoBehaviour
{
    public Transform RayOrigin;

    public float DetectionRange = 10f;
    public float DetectionAngle = 90f;

    private Transform Player;
    private SpiderSence Status;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Status = FindFirstObjectByType<SpiderSence>();
        print(Status);
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            Status.SetDanger();
            print("You Are in Danger");
        }
    }

    bool CanSeePlayer()
    {
        Vector3 Direction = Player.position - RayOrigin.position;

        if (Direction.magnitude > DetectionRange)
        {
            return false;
        }

        float Angle = Vector3.Angle(RayOrigin.forward, Direction);

        if (Angle > DetectionAngle / 2)
        {
            return false;
        }

        RaycastHit Hit;

        if (Physics.Raycast(RayOrigin.position, Direction.normalized,
            out Hit, DetectionRange))
        {
            Debug.DrawLine(RayOrigin.position, Hit.point, Color.blue);
            //AI was used to help with debugging in this script
            Debug.Log(Hit.collider.gameObject.name);
            Debug.Log(Hit.collider.tag);
            
            if (Hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}