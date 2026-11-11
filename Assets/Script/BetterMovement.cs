using Unity.Mathematics;
using UnityEngine;

public class BetterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;

    [Header("Drag")]
    public float GroundDrag;

    public Transform Orientation;
    public Transform CamaraOrientation;
    private float HorizonTalInput;
    private float VerticalInput;

    Vector3 MoveDirection;
    Rigidbody Rb;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizonTalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        //Debug
        Vector3 forward = transform.TransformDirection(Vector3.forward * 10);
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizonTalInput;
        Rb.AddForce(MoveDirection.normalized * MoveSpeed * 1f, ForceMode.Force);
        Rb.linearDamping = GroundDrag;

    }

}
