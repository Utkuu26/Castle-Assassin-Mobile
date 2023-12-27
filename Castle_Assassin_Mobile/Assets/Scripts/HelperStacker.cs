using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperStacker : MonoBehaviour
{
    NavMeshAgent meshAgent;
    public GameObject helper;
    public int yVal;
    public int xVal;
    public int zVal;
    public float ySpace;
    public float xSpace;
    public float zSpace;
    public int index;
    public int collectedItem;
    public int droppableItem;
    public int MaxCapacity;
    public Transform stackHolderPos;
    public List<Vector3> stackPosList = new List<Vector3>();
    public List<CollectableObject> CollectedObjectList = new List<CollectableObject>();
    public ZoneController zoneController;
    public HelperMovement helperMovement;

    void Start()
    {
        CreateStackHolder();
        collectedItem = 0;
        droppableItem = 0;
    }

    void Update()
    {
        
    }
    private void CreateStackHolder()
    {
        Vector3 pos = Vector3.zero;
        for (int h = 0; h < zVal; h++)
        {
            for (int i = 0; i < xVal; i++)
            {
                for (int j = 0; j < yVal; j++)
                {
                    pos = stackHolderPos.localPosition + Vector3.up * j * ySpace + Vector3.right * xSpace * i + Vector3.back * zSpace * h;
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

    private bool IsHolderFull()
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
        if (collectableObject && collectableObject.CompareTag("Collect"))
        {
            collectableObject.managerCubeList.ACollectableObjectCollected(collectableObject);
            CollectedObjectList.Add(collectableObject);
            other.GetComponent<Collider>().enabled = false;
            other.transform.SetParent(stackHolderPos.transform, false);
            var ps = GetStackPos();
            other.transform.localPosition = ps;
            collectedItem++;
            droppableItem = collectedItem;
        }

        if (other.CompareTag("Helper"))
        {
           // helperMovement.WaitForOtherHelper();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        var collectableObject = other.GetComponent<CollectableObject>();
        if (other.CompareTag("Storage") && droppableItem > 0)
        {

            var zone = other.GetComponent<ZoneController>();
            if (zone.IsAreaFull())
            {
                return;
            }
            zone.TakeCollactableStorage(GetCollactable());
            droppableItem--;
            collectedItem--;
            zoneController.itemInList++;

        }

    }

    public CollectableObject GetCollactable()
    {
        var index = CollectedObjectList.Count - 1;
        if (index < 0)
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
