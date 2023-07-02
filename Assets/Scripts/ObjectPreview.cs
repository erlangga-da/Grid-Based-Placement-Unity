using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    public GameObject[] previewObjects;
    private bool toggleKeyPressed = false;
    // Start is called before the first frame update
    void Start() {
        foreach (GameObject item in previewObjects) {
            item.SetActive(toggleKeyPressed);
        }
    }

    public void changePreview(int value) {
        Start();
        previewObjects[value].SetActive(true);
    }

    public GameObject getObjects(int value) {
        return previewObjects[value];
    }
}
