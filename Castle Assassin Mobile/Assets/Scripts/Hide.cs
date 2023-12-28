using System.Collections;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Transform teleportTarget; // Belirli bir hedef noktası
    private bool isHidden;
    private bool isJoystickEnabled = true; // Joystick etkileşimini kontrol etmek için
    public GameObject player;

    void Start()
    {
        isHidden = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HidePlayer();
            StartCoroutine(DisableJoystickForDuration(1f)); // Joystick'i belirli bir süre için devre dışı bırak
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Burada sadece karakterin varilden çıktığını belirten bir işaret bırakıyoruz
            isHidden = false;
        }
    }

    void Update()
    {
        // Update fonksiyonunda kontrolü yaparak karakterin varilden çıkmasını sağlıyoruz
        if (isHidden && Input.GetMouseButton(0) && isJoystickEnabled)
        {
            ShowPlayer();
        }
    }

    private void HidePlayer()
    {
        isHidden = true;
        // Make the player invisible
        player.SetActive(false);

        // Işınlanma
        if (teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
        }
    }

    private void ShowPlayer()
    {
        isHidden = false;
        // Make the player visible again
        player.SetActive(true);
    }

    private IEnumerator DisableJoystickForDuration(float duration)
    {
        isJoystickEnabled = false;
        yield return new WaitForSeconds(duration);
        isJoystickEnabled = true;
    }
}
