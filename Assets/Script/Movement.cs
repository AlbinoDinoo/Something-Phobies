using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController MyBoyController;
    //private float GravityValue = 1f;
    private Vector3 PlayerVal;

    private bool IsGronded;
    void Start()
    {
        MyBoyController = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        IsGronded = MyBoyController.isGrounded;
        if (IsGronded)
        {
            PlayerVal.y = 0f;
        }
        //PlayerVal.y += GravityValue * Time.deltaTime;

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f ,Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(moveInput);

        MyBoyController.Move(move * Time.deltaTime);
        MovePlayer();
    }

    void MovePlayer()
    {
        
    }
}
