using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Factory : MonoBehaviour
{
    [SerializeField] public GameObject[] TilePrefabs;
    int i;
    int n;
    int k;
    public bool isEmpty;
    void Awake()
    {
        FulfillFactories();
    }

    public void FulfillFactories()
    {
        for (int a = 0; a < 4; a++)
        {
            if(this.gameObject.transform.childCount==0) { return; }
            i = Random.Range(0, 6);
            Instantiate(TilePrefabs[i], this.gameObject.transform.GetChild(a).gameObject.transform);
        }
    }

    public void EmptyFactories()
    {
        if(this.gameObject.CompareTag("Middle Factory")) 
        {
            if (this.gameObject.transform.childCount != 0) 
            {
                isEmpty = false;
                return; 
            }
        }
        if(this.gameObject.CompareTag("Factory"))
        {
            for (int k = 0; k < 4; k++)
            {
                if (this.gameObject.transform.GetChild(k).childCount != 0)
                {
                    isEmpty = false;
                    return;
                }
            }
        }
        isEmpty = true;        
    }

}
