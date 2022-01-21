using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    //set the cursor state
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
