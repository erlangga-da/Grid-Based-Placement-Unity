using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour {
    public int gridSize = 1;
    public GameObject Groundbase;
    private BoxCollider boxCollider;
    private GameObject objectToPlace;
    private GameObject preview;
    private bool canBuild;

    void Update() {
        getInputKeyboard();
        updateRaycast();
        if(preview) {
            getInputMouse();
        }
    }
    //manager input checking
    void getInputKeyboard() {
        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            SetObject("Straight", "set to road straight");
            SetPreview(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {     
            SetObject("Bend", "set to road Bend");
            SetPreview(1);
        }
    }
    void getInputMouse() {
        if (Input.GetMouseButtonDown(0)) {
            if (canBuild) {
                SpawnObject();
            }
        }
    }

    // raycast
    void updateRaycast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Check if the ray intersects with a collider
        if (Physics.Raycast(ray, out hit)) {
            Vector3 snappedPosition = SnapToGrid(hit.point);
            if(CellValidator(hit.transform.name, snappedPosition)) {
                if(preview) {
                    preview.transform.position = snappedPosition;
                }
                canBuild = true;
            }else {
                canBuild = false;
            }
            // Debug.Log("batas maju " + (4 - snappedPosition.x) + "| batas mundur " + (snappedPosition.x + 4));
            // Debug.Log(snappedPosition);
            // int items = Mathf.RoundToInt(4 - snappedPosition.x);
            // for (int i = 0; i < items; i++)
            // {
            //     snappedPosition.x = snappedPosition.x + 1f;
            //     Debug.Log(snappedPosition);
            // }
        }
    }

    // check if mouse inside grid area and the cell is empty
    public bool CellValidator(String ObjectName, Vector3 snappedPosition) {
        bool isValid = false;
        // check if the ground plane mesh
        if (ObjectName == "Plane") {
            // prevent player build outside the grid
            if (snappedPosition.z >= 5 ||snappedPosition.z <= -5) {
                isValid = false;
            }else if (snappedPosition.x >= 5 ||snappedPosition.x <= -5) {
                isValid = false;
            }else {
                isValid = true;
            }
        }else {
            // prevent player build in the filled area
            isValid = false;
        }
        return isValid;
    }

    //set ready object
    void SetObject(String type, String logText) {
        PlacementManager PlacementManager = GetComponent<PlacementManager>();
        //call method from script placementManager
        objectToPlace = PlacementManager.GetRoad(type);
        Debug.Log(logText);
    }

    //set object preview
    void SetPreview(int value) {
        ObjectPreview ObjectPreview = GetComponent<ObjectPreview>();
        ObjectPreview.changePreview(value);
        preview = ObjectPreview.getObjects(value);
    }

    //set object spawn position
    Vector3 SnapToGrid(Vector3 position) {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        //snapping Object to the base
        float y = Groundbase.transform.position.y;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x, y, z);
    }

    // Spawn object at specific area
    void SpawnObject() {
        GameObject road = Instantiate(objectToPlace, preview.transform.position, Quaternion.identity);
        road.AddComponent<BoxCollider>();
        boxCollider = road.GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 0.1f, 1f);
        boxCollider.center = Vector3.zero;
    }

    //next:
    //add rotate
}
