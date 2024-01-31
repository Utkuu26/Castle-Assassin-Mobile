using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelCounter : MonoBehaviour 
{
    public TextMeshProUGUI currentLevelTxt;
    public TextMeshProUGUI winLevelTxt;
    private int currentLevelNo = 0;

    void Start()
    {
        currentLevelTxt.text = ("Level " + (currentLevelNo+1));
    }

    public void UpdateLevelCounter()
    {
        currentLevelTxt.text = ("Level " + (currentLevelNo + 1));
    }

    public void UpdateWinScreenLevel()
    {
        currentLevelNo ++;
        winLevelTxt.text = ("CONGRATULATIONS\nYou Completed Level " + currentLevelNo);
    }

}
