using UnityEngine;

public class Pushing : MonoBehaviour
{
    public float PushForce = 10f;

    void OnControllerColliderHit(ControllerColliderHit Hit)
    {
        Rigidbody Body = Hit.collider.attachedRigidbody;

        if (Body == null || Body.isKinematic)
        {
            return;
        }

        Vector3 Direction = Hit.moveDirection;
        Direction.y = 0;

        Body.AddForceAtPosition(Direction.normalized * PushForce,Hit.point, ForceMode.Force);
    }
}