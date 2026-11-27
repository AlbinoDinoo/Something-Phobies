using UnityEngine;

public class PlayerNovement : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float RotationSpeed = 10f;

    public Transform SpiderVisual;
    public Transform CamaraTransform;

    private CharacterController Controller;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");

        Vector3 Direction = new Vector3(X, 0, Z).normalized;

        if (CamaraTransform != null)
        {
            Direction = Quaternion.Euler(
                0, CamaraTransform.eulerAngles.y, 0
            ) * Direction;
        }

        if (Direction != Vector3.zero)
        {
            SpiderVisual.rotation = Quaternion.Slerp(
                SpiderVisual.rotation,
                Quaternion.LookRotation(Direction),
                RotationSpeed * Time.deltaTime
            );
        }

        Direction = Direction * MoveSpeed;
        Direction.y = -10f;

        Controller.Move(Direction * Time.deltaTime);
    }
}