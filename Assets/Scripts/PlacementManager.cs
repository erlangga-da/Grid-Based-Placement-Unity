using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour {
    //roads
    public GameObject[] Roads;
    //building
    public GameObject GetRoad(int value) {
        return Roads[value];
    }
}
