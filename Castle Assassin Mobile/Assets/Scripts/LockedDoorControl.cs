using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorControl : MonoBehaviour
{
    private Collider doorCollider; 

    public KeyController keyController;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
        LockedColliderEnabled();
    }

    void Update()
    {
        if (keyController.isDoorLocked)
        {
            LockedColliderEnabled();
        }
        else
        {
            LockedColliderDisabled();
        }
    }

    void LockedColliderEnabled()
    {
        doorCollider.enabled = true;
    }

    void LockedColliderDisabled()
    {
        doorCollider.enabled = false;
    }
}
