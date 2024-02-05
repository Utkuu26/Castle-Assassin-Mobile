using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorControl : MonoBehaviour
{
    private Collider doorCollider; 
    public KeyController keyController;

    public AudioSource lockedDoorAudioSource;
    public AudioClip lockedDoorSfx;

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            lockedDoorAudioSource.clip = lockedDoorSfx;
            lockedDoorAudioSource.Play();
        }
    }

    void LockedColliderDisabled()
    {
        doorCollider.enabled = false;
        doorCollider.isTrigger = true;
    }
}
