using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    SelectionScript selectionScript;    
    public void ZoomInFunction()
    {
        selectionScript.selected.SetActive(true);
        GameObject.Find("Out Camera").SetActive(false);
    }

    public void ZoomOutFunction()
    {
        GameObject.Find("Out Camera").SetActive(true);
        GameObject.Find("Second Player Camera").SetActive(false);
        GameObject.Find("First Player Camera").SetActive(false);
        
    }

}
