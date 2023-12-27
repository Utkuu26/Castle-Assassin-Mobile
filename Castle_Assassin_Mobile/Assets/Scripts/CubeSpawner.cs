using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CubeSpawner : MonoBehaviour
{
    public List<CollectableObject> collactables = new List<CollectableObject>();
    public int MaxColoumnCountForItemStack;
    public int yVal;
    public int MaxRowCountForItemStack;
    private int listSize;
    public float spwnTime, spwnDelay;
    public int collactabelsCount;
    public CollectableObject collactablePrefab;
    public Vector3 objectOffset;
    public bool spwnStop;
    public int i=0;
    public bool isStartWithFullStack;
    public Transform TargetPoint1;
    public Transform TargetPoint2;

    public ZoneController managerZoneController;
    void Start()
    {
        CubeList();
        spwnStop = false;
        if(isStartWithFullStack)
            Spawn(false);

    }

    void Update()
    {

        if (spwnTime > 0f)
            spwnTime -= Time.deltaTime;
        else
        {
            Spawn(true);
            spwnTime = spwnDelay;
        }


    }


    public bool IsAnyCubeAvaible()
    {
        bool IsAnyAvaible = true;

        var ýtemcount = 0;
        foreach (var item in collactables)
        {
            if (item != null)
                ýtemcount++;
        }
        if (ýtemcount == 0)
            IsAnyAvaible = false;

        return IsAnyAvaible;
    }
    private void CubeList()
    {
        listSize = MaxColoumnCountForItemStack * yVal * (MaxRowCountForItemStack);
        for (int i = 0; i < listSize; i++)
        {
            collactables.Add(null);
        }
        Debug.Log(collactables.Capacity);
    }


    public Vector3 GetLocalPositionByIndex(int index)
    {
        Vector3 targetLocalPos = Vector3.zero;

        targetLocalPos.y = (int)(index / (MaxColoumnCountForItemStack * MaxRowCountForItemStack));
        int inFloorIndex = (int)(index - (Mathf.Abs(targetLocalPos.y) * MaxColoumnCountForItemStack * MaxRowCountForItemStack));
        targetLocalPos.z = (inFloorIndex / MaxColoumnCountForItemStack);
        targetLocalPos.x = (inFloorIndex % MaxColoumnCountForItemStack);

        targetLocalPos.x = this.transform.position.x + targetLocalPos.x;
        targetLocalPos.y = this.transform.position.y + targetLocalPos.y;
        targetLocalPos.z = this.transform.position.z + targetLocalPos.z;

        return targetLocalPos;
    }

    public void ACollectableObjectCollected(CollectableObject collectableObject)
    {
        collactables[collectableObject.indexNo] = null;
    }

  
    void Spawn(bool justOneTime)
    {
        collactables.Capacity = 30;
        collactabelsCount = collactables.Count;

        for (int i = 0; i < collactabelsCount; i++)
        {
            if (collactables[i] == null)
            {
                collactables[i] = Instantiate(collactablePrefab, GetLocalPositionByIndex(i), collactablePrefab.transform.rotation, transform);
                collactables[i].Initiliaze(managerZoneController, this);
                collactables[i].indexNo = i;
                if(justOneTime)
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

}
