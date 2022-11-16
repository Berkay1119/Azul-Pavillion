using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public  bool outCamera = true;
    public  bool firstCamera = false;
    public  bool secondCamera = false;

     public GameObject firstPlayerCamera ;
     public GameObject secondPlayerCamera ;
     public GameObject outPlayerCamera ;
    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(DeactiveCameras(0.05f));
    }

    public IEnumerator DeactiveCameras(float waitTime)
    {
        firstPlayerCamera = GameObject.Find("First Player Camera");
        secondPlayerCamera = GameObject.Find("Second Player Camera");
        outPlayerCamera = GameObject.Find("Out Camera");
        yield return new WaitForSeconds(waitTime);
        firstPlayerCamera.SetActive(false);
        secondPlayerCamera.SetActive(false);
        outPlayerCamera.SetActive(true); ;
    }
    public void ChangeCameras()
    {
        outPlayerCamera.SetActive(outCamera);
        firstPlayerCamera.SetActive(firstCamera);
        secondPlayerCamera.SetActive(secondCamera);
    }

    public void ActiveFirstCamera()
    {
        outCamera = false;
        firstCamera = true;
        secondCamera = false;
        ChangeCameras();
    }
    public void ActiveSecondCamera()
    {
        //Debug.Log("aa");
        outCamera = false;
        firstCamera = false;
        secondCamera = true;
        ChangeCameras();
    }
    public void ActiveOutCamera()
    {
        // Debug.Log("aa");
        outCamera = true;
        firstCamera = false;
        secondCamera = false;
        ChangeCameras();
    }

    public void PlacementStarts(bool isItFirstPlayer)
    {
        if(isItFirstPlayer)
        {
            ActiveFirstCamera();
        }
        else
        {
            ActiveSecondCamera();
        }
    }

}
