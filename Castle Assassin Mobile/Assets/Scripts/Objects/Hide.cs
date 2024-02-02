using System.Collections;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Transform teleportTarget; // Belirli bir hedef noktası
    public bool isHidden;
    private bool isJoystickEnabled = true; // Joystick etkileşimini kontrol etmek için
    public GameObject player;
    public GameObject barrelTxt;

    void Start()
    {
        isHidden = false;
        barrelTxt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HidePlayer();
            barrelTxt.SetActive(true);
            StartCoroutine(DisableJoystickForDuration(1f)); // Joystick'i belirli bir süre için devre dışı bırak
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHidden = false;
        }
    }

    void Update()
    {
        if (isHidden && Input.GetMouseButton(0) && isJoystickEnabled)
        {
            ShowPlayer();
            barrelTxt.SetActive(false);
        }
    }

    private void HidePlayer()
    {
        isHidden = true;
        player.SetActive(false);
    
        if (teleportTarget != null)
        {
            player.transform.position = new Vector3(player.transform.position.x+ 2f, player.transform.position.y, player.transform.position.z+ 1f);
        }
    }

    private void ShowPlayer()
    {
        isHidden = false;
        player.SetActive(true);
    }

    private IEnumerator DisableJoystickForDuration(float duration)
    {
        isJoystickEnabled = false;
        yield return new WaitForSeconds(duration);
        isJoystickEnabled = true;
    }
}
