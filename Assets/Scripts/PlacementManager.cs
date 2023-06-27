using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour {
    //roads
    public GameObject[] Roads;
    //building
    public GameObject GetRoad(string value) {
        if (value == "Straight") {
            return Roads[0];
        }
        if (value == "Bend") {
            return Roads[1];
        }
        return null;
    }
}
