using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public Transform worldSpaceCanvas;
    public Transform player;
    public Transform mainCamera;
    public Vector3 offset;
    public Transform levelText;

    void Start()
    {

    }

    void Update()
    {
        float cameraRotation = mainCamera.eulerAngles.y;

        // Canvas'ı kameranın dönüşüne göre döndür
        worldSpaceCanvas.rotation = Quaternion.Euler(0f, cameraRotation, 0f);

        // Canvas'ın pozisyonunu güncelle
        worldSpaceCanvas.position = player.position + offset;
    }
    
}
