using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerStackHolder : MonoBehaviour
{
    public Vector3 stackHolderSizes;
    public Vector3 objectOffsetSizes;
    //public int yVal;
    //public int xVal;
    //public int zVal;
    //public float ySpace;
    //public float xSpace;
    //public float zSpace;
    public int index;
    public int collectedItem;
    public int droppableItem;
    public int MaxCapacity;
    public TextMeshProUGUI cubeCountTxt;
    public Transform stackHolderPos;
    public List<Vector3> stackPosList = new List<Vector3>();
    public List<CollectableObject> CollectedObjectList = new List<CollectableObject>();
    public ZoneController managerZoneController;

    void Start()
    {
        CreateStackHolder();
        collectedItem = 0;
        droppableItem = 0;
      
    }

    private void CreateStackHolder()
    {
        Vector3 pos = Vector3.zero;
        for (int h = 0; h < stackHolderSizes.z; h++)
        {
            for (int i = 0; i < stackHolderSizes.y; i++)
            {
                for (int j = 0; j < stackHolderSizes.x; j++)
                {
                    pos = stackHolderPos.localPosition + Vector3.up*j* objectOffsetSizes.y + Vector3.right* objectOffsetSizes .x* i + Vector3.back* objectOffsetSizes.z* h;
                    stackPosList.Add(pos);
                }
            }
        }
    }

    private Vector3 GetStackPos()
    {
        var newPos = stackPosList[index];
        index++;
        return newPos;
    }

    private Boolean IsHolderFull()
    {
        return CollectedObjectList.Count >= MaxCapacity;
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsHolderFull())
        {
            return;
        }
        var collectableObject = other.GetComponent<CollectableObject>();
        var zoneController = other.GetComponent<ZoneController>();

        // collecting
        if (other.CompareTag("Collect"))
        {
            collectableObject.managerCubeList.ACollectableObjectCollected(collectableObject);
            CollectedObjectList.Add(collectableObject);
            other.GetComponent<Collider>().enabled = false;
            other.transform.SetParent(stackHolderPos.transform, false);
            var ps = GetStackPos();
            other.transform.DOLocalJump(ps, 1, 1, 0.5f);
            collectedItem++;
            droppableItem = collectedItem;
            cubeCountTxt.text = "Cubes: " + collectedItem.ToString();

        }

        if (other.CompareTag("StorageObject"))
        {

            for (int i = 0; i < managerZoneController.storeDataList.Count; i++)
            {
                if (managerZoneController.storeDataList[i].collectable==other.GetComponent<CollectableObject>())
                {
                    managerZoneController.storeDataList[i].collectable = null;
                    break;
                }

            }
            CollectedObjectList.Add(collectableObject);
            other.GetComponent<Collider>().enabled = false;
            other.transform.SetParent(stackHolderPos.transform, false);
            var ps = GetStackPos();
            other.transform.DOLocalJump(ps, 1, 1, 0.5f);
            collectedItem++;
            droppableItem = collectedItem;
            cubeCountTxt.text = "Cubes: " + collectedItem.ToString();
            managerZoneController.itemInStorage--;

        }

     }

    public CollectableObject GetCollactable()
    {
        var index = CollectedObjectList.Count - 1;
        if (index<0)
        {
            return null;
        }
        
        var collactable = CollectedObjectList[index];
        CollectedObjectList.Remove(collactable);
        collactable.transform.SetParent(null);
        this.index--;
        return collactable;
      
    }

}
