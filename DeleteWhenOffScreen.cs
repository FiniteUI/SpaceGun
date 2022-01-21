using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenOffScreen : MonoBehaviour
{
    private float _objectHeight;
    public bool aboveScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        _objectHeight = GetComponent<SpriteRenderer>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        //account for size of sprite
        position.y = position.y + _objectHeight;

        //if object goes off screen, delete
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(position);
        if (screenPosition.y < 0) {
            Destroy(this.gameObject);
        }

        if (aboveScreen) {
            if(screenPosition.y > 1) {
                Destroy(this.gameObject);
            }
        }
    }
}
