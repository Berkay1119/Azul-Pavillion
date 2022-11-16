using LibNoise.Unity.Operator;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SelectionScript : MonoBehaviour
{

    [SerializeField] public GameObject[] stored_tiles;
    [SerializeField] public GameObject[] placed_tiles;
    public GameObject selected;
    bool isDragging;
    GameObject storage;
    public int storage_count = 0;
    int placed_count = 0;
    [SerializeField] public GameObject storedTilesParent;
    [SerializeField] public GameObject tilesParent;
    [SerializeField] public GameObject placedTilesParent;
    [SerializeField] GameObject middleFactory;
    Scoring scoringScript;
    [SerializeField]GameObject cameraManager;
    [SerializeField] public int firsttotalScore = 0;
    [SerializeField] public int secondtotalScore = 0;
    float rotationWLayer;
    [SerializeField] GameObject firstPlayerScore;
    [SerializeField] GameObject secondPlayerScore;
    Zoom zoomScript;
    string selected_tag;
    GameObject otherTile;
    Transform selectedParent;
    int factoryTileCount;
    float x = 7.45f;
    float z = 18f;
    public bool isItFirstPlayer = true;
    GameObject frontBoard;
    Factory factoryScript;
    GameObject[] factories;
    List<GameObject> factoryLists = new List<GameObject>();
    bool startPlacement;
    public bool allFactoriesEmpty;
    public bool tableEmpty;
    public bool storageEmpty;
    Transform transformOfFirstFrontboard;
    Transform transformOfSecondFrontboard;


    void Awake()
    {
        transformOfFirstFrontboard = GameObject.Find("First Incoming Tiles").transform;
        transformOfSecondFrontboard = GameObject.Find("Second Incoming Tiles").transform;
        //Debug.Log(transformOfFirstFrontboard.gameObject.name);

        factories = GameObject.FindGameObjectsWithTag("Factory");
        foreach (GameObject factory in factories)
        {
            factoryLists.Add(factory);
        }
        factoryLists.Add(GameObject.FindGameObjectWithTag("Middle Factory"));
    }

    // Update is called once per frame
    public void Update()
    {
        Selecting();
        firstPlayerScore.GetComponent<TextMeshProUGUI>().text=firsttotalScore.ToString();
        secondPlayerScore.GetComponent<TextMeshProUGUI>().text = secondtotalScore.ToString();
        //if (Input.GetKeyDown(KeyCode.Escape)) { zoomScript.ZoomOutFunction(); }
        if (storedTilesParent.gameObject.transform.childCount == 0) { storageEmpty = true; }
        else { storageEmpty = false; }
        if (GameObject.Find("Tiles").transform.childCount == 0) { tableEmpty = true; }
        else { tableEmpty = false; }

        CheckRestart();
        

    }

    public void Selecting()
    {


        if (Input.GetMouseButtonDown(0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                selected = hit.collider.gameObject;
                
                switch (selected.transform.parent.tag)
                {
                    case "Tiles":
                        switch (selected.tag)
                        {
                            case "Blue":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Blue" ) { return; }
                                }
                                TableToStorage();
                                break;

                            case "Red":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Red") { return; }                                    
                                }
                                TableToStorage();
                                break;
                            case "Yellow":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Yellow") { return; }
                                }
                                TableToStorage();
                                break;
                            case "Green":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Green") { return; }
                                }
                                TableToStorage();
                                break;
                            case "Purple":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Purple") { return; }
                                }
                                TableToStorage();
                                break;
                            case "Orange":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Orange") { return; }
                                }
                                TableToStorage();
                                break;
                            case "Galaxy":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (stored_tiles[i].tag != "Galaxy") { return; }
                                }
                                TableToStorage();
                                break;
                        };
                        break;
                    case "Factory Location":
                        FactoryToTable();
                        break;
                    case "Middle Factory":
                        FactoryToTable();
                        break;
                }
                if(selected.transform.parent.transform.parent != null)
                {
                    if (selected.transform.parent.transform.parent.tag == "Player Board")
                    {


                        switch (selected.transform.parent.tag)
                        {

                            case "Blue Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Blue") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 9f;
                                StorageToBoard();
                                break;
                            case "Red Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Red") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 8f;
                                StorageToBoard();
                                break;
                            case "Yellow Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Yellow") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 7f;
                                StorageToBoard();
                                break;
                            case "Green Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Green") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 6f;
                                StorageToBoard();
                                break;
                            case "Purple Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Purple") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 5f;
                                StorageToBoard();
                                break;
                            case "Orange Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Orange") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 4f;
                                StorageToBoard();
                                break;
                            case "Galaxy Places":
                                for (int i = 1; i <= storage_count; i++)
                                {
                                    if (!(stored_tiles[i].tag == "Galaxy") || stored_tiles[i] == null) { return; }
                                }
                                rotationWLayer = 5f;
                                StorageToBoard();
                                break;
                        }


                    }
                }
                
            }
        }


    }

    private void FactoryToTable()
    {
        selected_tag = selected.gameObject.tag;
        selectedParent= selected.transform.parent;
        //selectedParent = selected.transform.parent;
        if (isItFirstPlayer)
        {
            frontBoard = GameObject.Find("First Incoming Tiles");
        }
        else { frontBoard = GameObject.Find("Second Incoming Tiles"); }

        isItFirstPlayer = !isItFirstPlayer;

        if (selectedParent.transform.parent.tag == "Factory")
        {
            for (int a = 0; a < 4; a++)
            {
                otherTile = selectedParent.transform.parent.GetChild(a).GetChild(0).gameObject;

                if (otherTile.tag == selected_tag)
                {
                    otherTile.transform.parent = null;
                    otherTile.transform.parent = tilesParent.transform;
                    otherTile.transform.position = frontBoard.transform.position;
                    frontBoard.transform.position = frontBoard.transform.position 
                        + new Vector3(1f,0f);
                }
                else
                {
                    otherTile.transform.parent = null;
                    otherTile.transform.parent = middleFactory.transform;
                    otherTile.transform.position = new Vector3(x,0,z);

                    if (x != 12.45f)
                    {
                        x++;
                    }
                    else if (x == 12.45f)
                    {
                        z=z+2;
                        x = 7.45f;
                    }
                }
            }
        }
        if (selectedParent.tag == "Middle Factory")
        {
            
            factoryTileCount = selectedParent.childCount;
            for (int a = 0; a < (factoryTileCount); a++)
            {
                
                otherTile = selectedParent.GetChild(a).gameObject;

                if (otherTile.tag == selected_tag)
                {
                    
                    otherTile.transform.parent = null;
                    otherTile.transform.parent = tilesParent.transform;
                    otherTile.transform.position = frontBoard.transform.position;
                    frontBoard.transform.position = frontBoard.transform.position
                        + new Vector3(1f, 0f);
                    a--;
                    factoryTileCount--;
                    
                }
            }
        }

        //factories = GameObject.FindGameObjectsWithTag("Factory");
        foreach( GameObject factory in factories)
        {
            // factoryLists.Add(factory);
            factory.GetComponent<Factory>().EmptyFactories(); 
        }
        GameObject.FindGameObjectWithTag("Middle Factory").GetComponent<Factory>().EmptyFactories();
        
        foreach(GameObject factory in factoryLists)
        {
            if(!factory.GetComponent<Factory>().isEmpty)
            {
                return;
            }
        }
        allFactoriesEmpty = true;
        cameraManager.GetComponent<CameraChange>().PlacementStarts(isItFirstPlayer);

    }

    public void TableToStorage()
    {
        if (isItFirstPlayer)
        {
            storage = GameObject.Find("First Player Storage");
        }
        else { storage = GameObject.Find("Second Player Storage"); }

        Vector3 pos = storage.transform.position;
        selected.transform.position = pos;
        storage_count++;
        stored_tiles[storage_count] = selected;
        stored_tiles[storage_count].transform.position = new Vector3(pos.x + storage_count - 1, pos.y, pos.z);
        selected.transform.parent = null;
        selected.transform.parent = storedTilesParent.transform;
        
    }


    public void StorageToBoard()
    {
        if (storage_count >= (selected.layer - 7f))
        {
            placed_count++;
            selected.GetComponent<Scoring>().isItOccupied = true;
            if (isItFirstPlayer) { firsttotalScore = selected.GetComponent<Scoring>().PlacementScoring() + firsttotalScore; }
            else { secondtotalScore = selected.GetComponent<Scoring>().PlacementScoring() + secondtotalScore; }
            
            float rotation_angle = (selected.layer - rotationWLayer) * 60f;
            stored_tiles[storage_count].transform.position = selected.transform.position;
            stored_tiles[storage_count].transform.rotation = Quaternion.AngleAxis(rotation_angle, Vector3.up);
            placed_tiles[placed_count] = stored_tiles[storage_count];
            placed_tiles[placed_count].transform.parent = placedTilesParent.transform;
            stored_tiles[storage_count] = null;
            int tempt = storage_count;
            for (int i = tempt; i > (tempt - (selected.layer - 7)); i--)
            {
                
                Destroy(stored_tiles[i]);
                storage_count--;
            }
            
            
            isItFirstPlayer = !isItFirstPlayer;
            if (isItFirstPlayer) { cameraManager.GetComponent<CameraChange>().ActiveFirstCamera(); }
            else { cameraManager.GetComponent<CameraChange>().ActiveSecondCamera(); }

            
        }
    }

    public void CheckRestart()
    {
        
        if(tableEmpty&&storageEmpty&&allFactoriesEmpty)
        {
            foreach(GameObject factory in factoryLists)
            {
                factory.GetComponent<Factory>().FulfillFactories();
            }
            cameraManager.GetComponent<CameraChange>().ActiveOutCamera();
            tableEmpty = false;
            storageEmpty = false;
            allFactoriesEmpty = false;
            x = 7.45f;
            z = 18f;

            GameObject.Find("First Incoming Tiles").transform.localPosition = new Vector3(-7.25f, 0, 6) ;
            GameObject.Find("Second Incoming Tiles").transform.localPosition = new Vector3(-6.65f, 0, -7);
            
                
        }
    }

    public void Pass()
    {
        isItFirstPlayer = !isItFirstPlayer;
        if (isItFirstPlayer) { cameraManager.GetComponent<CameraChange>().ActiveFirstCamera(); }
        else { cameraManager.GetComponent<CameraChange>().ActiveSecondCamera(); }

    }
}
