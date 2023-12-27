using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStackDropper : MonoBehaviour
{
    private PlayerStackHolder managerStackHolder;
    private void Awake()
    {
        managerStackHolder = GetComponent<PlayerStackHolder>();
    }

  
    private void OnTriggerStay(Collider other)
    {
        // dropping      
        if (other.CompareTag("Zone") && managerStackHolder.droppableItem > 0 )
        {

            var zone = other.GetComponent<ZoneController>();         
            if (zone.IsAreaFull())
            {
                zone.IsAreaFullController();
                return;
            }
            zone.TakeCollactable(managerStackHolder.GetCollactable());
            managerStackHolder.droppableItem--;
            managerStackHolder.collectedItem--;
            managerStackHolder.cubeCountTxt.text = "Cubes: " + managerStackHolder.collectedItem.ToString();

        }

    }


}
