using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform Player;
    public Transform StartPosition;
    public Transform EndPosition;

    public float Range = 5f;
    public float FullHeightRange = 2f;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 Difference = Player.position - EndPosition.position;
        Difference.y = 0;

        float Distance = Difference.magnitude;

        float Amount = Mathf.InverseLerp(
            Range, FullHeightRange, Distance
        );

        transform.position = Vector3.Lerp(
            StartPosition.position,
            EndPosition.position,
            Amount
        );
    }
}