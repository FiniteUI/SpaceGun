using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public void SetStar(float offsetX, float offsetY, float rotationOffset, Sprite sprite, float scale, Color color) {
        GetComponent<SpriteRenderer>().sprite = sprite;
        transform.position = new Vector3(offsetX, offsetY, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationOffset);
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetStar(float offsetX, float offsetY, float rotationOffset, Sprite sprite, float scale) {
        GetComponent<SpriteRenderer>().sprite = sprite;
        transform.position = new Vector3(offsetX, offsetY, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationOffset);
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}
