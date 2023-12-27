using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public CubeSpawner managerCubeList;
    public ZoneController zoneController;
    public int indexNo;

    public void Initiliaze(ZoneController _zoneController, CubeSpawner _managerCubeList)
    {
        zoneController = _zoneController;
        managerCubeList = _managerCubeList;

    }

}
