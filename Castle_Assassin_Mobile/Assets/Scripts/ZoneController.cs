using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ZoneController : MonoBehaviour
{
    public int yVal;
    public int xVal;
    public int zVal;
    public float ySpace;
    public float xSpace;
    public float zSpace;
    private int index;
    public Transform dropHolderPos;
    public List<StoreData> storeDataList = new List<StoreData>();
    public GameObject currentZone;
    public GameObject nextZone;
    public GameObject helper;
    public bool helperControl = false;
    public bool zoneControl;
    public int itemInList =0;
    public int itemInStorage = 0;
    public int listSize;
    public int zoneSize;
    public TextMeshPro zoneCubeCountText;
    public Transform Storage;
    public CollectableObject collactablePrefab;
    private ZoneColorBeholder zoneColorBeholder;

    private PlayerStackHolder managerStackHolder;

    void Start()
    {
        CreateDropHolder();
        DOTween.Init();
        nextZone.GetComponent<Collider>().enabled = false;
        zoneColorBeholder = nextZone.GetComponentInChildren<ZoneColorBeholder>();
        helper.SetActive(false);
        listSize = xVal * yVal * zVal;
    }
    private void Update()
    {
        IsAreaFullController();
     
    }

    private void CreateDropHolder()
    {
        Vector3 pos = Vector3.zero;

        for (int h = 0; h < zVal; h++)
        {
            for (int i = 0; i < xVal; i++)
            {
                for (int j = 0; j < yVal; j++)
                {
                    StoreData storeData = new StoreData();
                    pos =  Vector3.up * j * ySpace + Vector3.right * xSpace * i + Vector3.back * zSpace * h;                           
                    storeData.StackPos = pos;
                    storeDataList.Add(storeData);
                    
                }
            }
        }
    }

    public Vector3 GetDropPos()
    {
       
        var newPos = storeDataList[index].StackPos;
        index++;
        return newPos;
    }
    public bool IsAreaFull()
    {
        return storeDataList.FindAll(o => !o.collectable).Count <= 0;
    }

    public void TakeCollactable(CollectableObject collectable)
    {
       
        foreach (var item in storeDataList)
        {
            if (item.collectable==null)
            {
                collectable.transform.DOScale(Vector3.zero, 1.5f);
                collectable.transform.DOJump(transform.position, 1, 1, 2f);
                collectable.transform.eulerAngles = Vector3.zero;
                item.collectable = collectable;
                listSize--;
                zoneCubeCountText.text = listSize.ToString();              
                break;
            }
        }
       
    }

    public void TakeCollactableStorage(CollectableObject collectable)
    {
     
        foreach (var item in storeDataList)
        {
            if (item.collectable == null)
            {
                collectable.GetComponent<Collider>().enabled = true;
                collectable.transform.DOLocalJump(item.StackPos, 1, 1, 0.5f);             
                collectable.transform.eulerAngles = Vector3.zero;                            
                item.collectable = collectable;
                item.collectable.transform.SetParent(currentZone.transform, false);
                item.collectable.tag = "StorageObject";
                itemInStorage++;

                break;
            }

        }     
    }



    public void IsAreaFullController()
    {
        if (IsAreaFull())
        {
            if (zoneControl)
            {
                nextZone.GetComponent<Collider>().enabled = true;
                // zoneColorBeholder.ChangeColor();
                
                
            }
            if (helperControl)
            {
                helper.SetActive(true);
            }

        }

    }

}
[System.Serializable]
public class StoreData
{
    public Vector3 StackPos;
    public CollectableObject collectable;

}