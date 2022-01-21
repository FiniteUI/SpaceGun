using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateStars : MonoBehaviour
{
    [SerializeField] private Sprite[] _starsSprites;
    [SerializeField] private Color[] _starColors;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject background;
    public float density = 1.0f;
    public int maxOffSetPercent = 100;
    public int maxScalePercent = 50;
    public int minScalePercent = 5;
    private float counterX;
    private float counterY;
    private int minimumX;
    private int minimumY;
    private int maximumX;
    private int maximumY;
    public float preloadOffset = 1.0f;
    private Star baseStar;

    // Start is called before the first frame update
    void Start()
    {   
        baseStar = star.GetComponent<Star>();
        
        int levelWidth = (int) background.transform.localScale.x;
        minimumX = 0 - (int) levelWidth / 2; 
        maximumX = 0 + (int) levelWidth / 2;
        
        int levelHeight = (int) background.transform.localScale.y / 2;
        minimumY = (int) background.transform.position.y - levelHeight;
        maximumY = (int) background.transform.position.y + levelHeight;

        counterX = minimumX;
        counterY = minimumY;

        loadStars();
    }

    void Update() {
        loadStars();
    }

    void loadStars() {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(new Vector3(0, counterY, 0));
        Vector3 cameraPoint = Camera.main.WorldToViewportPoint(Camera.main.transform.position);

        while (screenPoint.y - cameraPoint.y <= preloadOffset) {
            do {
                Star newStar = Instantiate(baseStar);
                Sprite sprite = _starsSprites[Random.Range(0, _starsSprites.Length)];
                Color color = _starColors[Random.Range(0, _starColors.Length)];

                //calculate scale
                float scale = Random.Range(minScalePercent, maxScalePercent + 1) / 100.0f;

                //calculate rotation
                float rotation = Random.Range(-359, 360);

                //calculate new position, include offset
                float positionX = counterX + (Random.Range(-maxOffSetPercent, maxOffSetPercent + 1) / 100.0f);
                float positionY = counterY + (Random.Range(-maxOffSetPercent, maxOffSetPercent + 1) / 100.0f);

                newStar.SetStar(positionX, positionY, rotation, sprite, scale, color);
                //newStar.SetStar(positionX, positionY, rotation, sprite, scale);
                counterX += 1/density;
            } while (counterX < maximumX);
            counterY += 1/density;
            counterX = minimumX;
            screenPoint = Camera.main.WorldToViewportPoint(new Vector3(0, counterY, 0));
            cameraPoint = Camera.main.WorldToViewportPoint(Camera.main.transform.position);
        }
    }
}
