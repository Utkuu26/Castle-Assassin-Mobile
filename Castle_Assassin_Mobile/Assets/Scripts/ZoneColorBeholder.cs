using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneColorBeholder : MonoBehaviour
{
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        rend.material.color = Color.green;
    }
}
