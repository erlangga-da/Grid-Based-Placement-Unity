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
    private Vector3 spawnStart;
    private Vector3 spawnEnd;

    //kode ini dijalankan setiap frame
    void Update() {
        getInputKeyboard();
        updateRaycast();
        if(preview) {
            getInputMouse();
        }
    }

    //manager input checking
    void getInputKeyboard() {
        //cek jika player klik angka 1
        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            SetObject(0);
        }
        //cek jika player klik angka 2
        if (Input.GetKeyUp(KeyCode.Alpha2)) {     
            SetObject(1);
        }
        //cek jika player klik angka 3
        if (Input.GetKeyUp(KeyCode.Alpha3)) {     
            SetObject(2);
        }
        //cek jika player klik angka 4
        if (Input.GetKeyUp(KeyCode.Alpha4)) {     
            SetObject(3);
        }
        //cek jika player klik E
        if (Input.GetKeyUp(KeyCode.Q)) {     
            if(preview) {
                preview.transform.Rotate(0f, 90f, 0f);
            }
        }
        //cek jika player klik E
        if (Input.GetKeyUp(KeyCode.E)) {     
            if(preview) {
                preview.transform.Rotate(0f, -90f, 0f);
            }
        }
    }
    
    void getInputMouse() {
        if (Input.GetMouseButtonDown(0)) {
            if (canBuild) {
                SpawnObject();
                // if (spawnStart != Vector3.zero) {
                //     spawnEnd = preview.transform.position;
                //     Debug.Log("spawn end " + spawnEnd);
                // }else {
                //     spawnStart = preview.transform.position;
                //     Debug.Log("spawn start " + spawnStart);
                // }
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
            if(CellValidator(hit.transform.name, snappedPosition) && preview) {
                preview.transform.position = snappedPosition;
                // Debug.Log("batas maju " + (4 - snappedPosition.x) + "| batas mundur " + (snappedPosition.x + 4));
                canBuild = true;
            }else {
                canBuild = false;
            }
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
    void SetObject(int value) {
        PlacementManager PlacementManager = GetComponent<PlacementManager>();
        ObjectPreview ObjectPreview = GetComponent<ObjectPreview>();
        //call method from script placementManager
        objectToPlace = PlacementManager.GetRoad(value);
        ObjectPreview.changePreview(value);
        preview = ObjectPreview.getObjects(value);
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
        GameObject road = Instantiate(objectToPlace, preview.transform.position, preview.transform.rotation);
        road.AddComponent<BoxCollider>();
        boxCollider = road.GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 0.1f, 1f);
        boxCollider.center = Vector3.zero;
    }
}
