using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodScript : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public static int foodCount = 0;
    public TextMeshProUGUI foodCountText;


    public void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    public Vector3 GetFoodPosition()
    {
        return this.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
            foodCount=foodCount+10;
            UpdateFoodCountText();
        }
    }

    private void UpdateFoodCountText()
    {
        foodCountText.text = "Score: " + foodCount;
    }

    public void ResetFoodCount()
    {
        foodCount = 0;
        UpdateFoodCountText();
        // Debug.Log(foodCount);
    }
    public void FoodObs()
    {
        if (foodCount == 0)
        {
            foodCount = 0;
        }
        else
        {
            foodCount = foodCount - 3;
        }
        UpdateFoodCountText(); 
    }
}