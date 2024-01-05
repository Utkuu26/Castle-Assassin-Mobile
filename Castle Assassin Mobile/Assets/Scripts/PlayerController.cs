using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _charController;
    private Animator _animator;
    private JoystickController _mngrJoystick;
    public Transform meshPlayer;
    public float inputX;
    public float inputZ;
    private Vector3 vMovement;
    public float moveSpeed;
    public bool IsMoving;
  
    void Start()
    {
        moveSpeed = 4f;
        //meshPlayer = GetComponent<MeshCollider>();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        GameObject joystickObject = GameObject.Find("Joystick");

        if (joystickObject != null)
        {
            _mngrJoystick = joystickObject.GetComponent<JoystickController>();

            if (_mngrJoystick == null)
            {
                Debug.LogError("JoystickController component not found on Joystick GameObject.");
            }
        }
        else
        {
            Debug.LogError("Joystick GameObject not found in the scene.");
        }

        IsMoving = false;
    }

    void Update()
    {
        // Check if _mngrJoystick is not null before accessing its properties
        if (_mngrJoystick != null)
        {
            inputX = _mngrJoystick.direction.x;
            inputZ = _mngrJoystick.direction.y;

            if (inputX == 0 && inputZ == 0)
            {
                _animator.SetBool("isRun", false);
                IsMoving = false;
            }
            else
            {
                _animator.SetBool("isRun", true);
                IsMoving = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_mngrJoystick != null)
        {
            vMovement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
            _charController.Move(vMovement * Time.deltaTime);

            if (inputX != 0 || inputZ != 0)
            {
                Vector3 lookDir = new Vector3(vMovement.x, 0, vMovement.z);
                transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }
    }
}
