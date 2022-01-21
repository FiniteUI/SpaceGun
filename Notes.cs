using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea]
    [Tooltip("Notes for the inspector.")]
    public string notes = "";
}
