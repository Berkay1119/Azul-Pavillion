using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    
    private GameObject deselected;
    [SerializeField] SelectionScript selectionScript;
    private int str_count;


    void Start()
    {
        
    }
    // Update is called once per frame
     void Update()
    {
        
        //pre_str_count = selectionScript.storage_count--;
        if (Input.GetMouseButtonDown(1))
        {
            
            if (deselected == null)
            {
                
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    
                    if (!(hit.collider.transform.parent.CompareTag("Store") || hit.collider.transform.parent.CompareTag("Tiles"))) { return; }

                    deselected = hit.collider.gameObject;
                    Cursor.visible = false;
                    if (deselected.transform.parent.tag =="Store" )
                    {
                        Debug.Log(str_count);
                        selectionScript.stored_tiles[selectionScript.storage_count] = null;
                        selectionScript.storage_count--;
                        deselected.transform.parent = null;
                        deselected.transform.parent = selectionScript.tilesParent.transform;
                    }
                    
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(deselected.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                deselected.transform.position = new Vector3(worldPosition.x,0, worldPosition.z);

                deselected = null;
                Cursor.visible=true;
            }  
        }
        if (deselected != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(deselected.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            deselected.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
        }
    }
    public RaycastHit CastRay()
    {
        //Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        //Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        //Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        //Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000);

        return hit;
    }
}
