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
    private float moveSpeed;
    public bool IsMoving;
    
    void Start()
    {
        moveSpeed =0.40f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = tempPlayer.transform.GetChild(0);
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
       _mngrJoystick = GameObject.Find("Joystick").GetComponent<JoystickController>();
        IsMoving = false;
       
    }

    void Update()
    {

        inputX = _mngrJoystick.direction.x;
        inputZ = _mngrJoystick.direction.y;
     

        if (inputX == 0 && inputZ==00)
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

    private void FixedUpdate()
    {
        vMovement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
        _charController.Move(vMovement);
        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(vMovement.x, 0, vMovement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);
        }

    }

}
