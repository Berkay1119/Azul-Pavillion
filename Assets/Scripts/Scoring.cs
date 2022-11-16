using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    [SerializeField] public bool isItOccupied;
    SelectionScript selectionScript;
    int currentLayer;
    int tempcurrentLayer;
    GameObject downPlace;
    GameObject upPlace;
    int downAdjLayer;
    int upAdjLayer;
    [SerializeField] int currentscore=0;
    string currentPlaceName;
    string placeParentName;
    string boardName;
    
    void Awake()
    {
        isItOccupied = false;
    }
    public int PlacementScoring()
    {
        if (GameObject.Find("Selection").GetComponent<SelectionScript>().isItFirstPlayer) 
        {
            boardName = "First Player Board";
        }
        else
        {
            boardName = "Second Player Board ";
        }

        currentscore++;
        currentLayer = this.gameObject.layer;

        downAdjLayer = currentLayer-1;
        if (downAdjLayer == 7) { downAdjLayer = 13; }
        upAdjLayer = currentLayer + 1;
        if (upAdjLayer == 14) { upAdjLayer = 8; }
        currentPlaceName = this.gameObject.name;
        
        placeParentName = this.gameObject.transform.parent.name;
        //Debug.Log(boardName);
        downPlace = GameObject.Find("/"+boardName+"/"+placeParentName+"/"+ (downAdjLayer-7).ToString());
        
        upPlace= GameObject.Find("/" + boardName + "/" + placeParentName + "/" + (upAdjLayer-7).ToString());

        
        while (downPlace.GetComponent<Scoring>().isItOccupied)
        {
            currentscore++;
            tempcurrentLayer = downAdjLayer;
            downAdjLayer= tempcurrentLayer - 1 ;
            if (downAdjLayer == 7) { downAdjLayer = 13; }
            downPlace = GameObject.Find("/"+boardName+"/" + placeParentName + "/" + (downAdjLayer-7).ToString());
        }

        while(upPlace.GetComponent<Scoring>().isItOccupied)
        {
            currentscore++;
            tempcurrentLayer = upAdjLayer;
            upAdjLayer = tempcurrentLayer + 1 ;
            if (upAdjLayer == 14) { upAdjLayer = 8; }
            upPlace = GameObject.Find("/"+boardName+"/" + placeParentName + "/" + (upAdjLayer-7).ToString());
        }

        return currentscore;
    }

    public void NegativePoints()
    {
        //GameObject.Find("Selection").GetComponent<SelectionScript>()
    }
}