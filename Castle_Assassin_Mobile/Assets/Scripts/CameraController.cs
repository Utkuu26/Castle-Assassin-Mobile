using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float DistanceToPlayer;
    public float baseDistanceToPlayer;
    public PlayerController managerContChar;
    public Transform TargetTransform;

    public float speed;
    void Start()
    {
        baseDistanceToPlayer = Vector3.Distance(transform.position, TargetTransform.position);

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (managerContChar.IsMoving)
        {
            transform.position = Vector3.Lerp(transform.position, CalculatePos(), 0.03f);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, InitPos(), 0.03f);
        }


    }

     
    private Vector3 CalculatePos()
    {
        var pos = transform.position;

        var direction = transform.forward * -1;

        pos = TargetTransform.position + direction.normalized * DistanceToPlayer;

        return pos;
    }

    private Vector3 InitPos()
    {
        var pos = transform.position;

        var direction = transform.forward * -1;       

        pos = TargetTransform.position + direction.normalized * baseDistanceToPlayer;

        return pos;
    }
}
