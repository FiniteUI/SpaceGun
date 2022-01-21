using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UI;

public class Refocus : MonoBehaviour
{
    GameObject lastSelect;

    private void Start() {
        lastSelect = new GameObject();
    }

    private void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
