using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelCounter : MonoBehaviour 
{
    public TextMeshProUGUI currentLevelTxt;
    private int currentLevelNo = 1;

    void Start()
    {
        currentLevelTxt.text = ("Level " + currentLevelNo);
    }

    public void UpdateLevelCounter()
    {
        currentLevelNo ++;
        currentLevelTxt.text = ("Level " + currentLevelNo);
    }

}
