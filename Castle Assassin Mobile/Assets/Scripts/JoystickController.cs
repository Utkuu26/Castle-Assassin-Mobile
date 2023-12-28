using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public RectTransform Center;
    public RectTransform Knob;
    public float range;
    public bool FixedJoystick;

    [HideInInspector]
    public Vector2 direction;
    public bool active;

    void Start()
    {
        ShowJoystick(false);
        active = false;
    }

    void Update()
    {
        Vector2 pos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            ShowJoystick(true);
            active = true;
            Knob.position = pos;
            Center.position = pos;
        }
        else if (Input.GetMouseButton(0))
        {
            Knob.position = pos;
            Knob.position = Center.position + Vector3.ClampMagnitude(Knob.position - Center.position, Center.sizeDelta.x * range);

            if (Knob.position != Input.mousePosition && !FixedJoystick)
            {
                Vector3 OBV = Input.mousePosition - Knob.position;
                Center.position += OBV;
            }

            direction = (Knob.position - Center.position).normalized;
        }
        else
        {
            active = false;
            ShowJoystick(false);
            direction = Vector2.zero;
        }
    }

    void ShowJoystick(bool state)
    {
        Center.gameObject.SetActive(state);
        Knob.gameObject.SetActive(state);
    }
}
