using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D gridArea;
    public void Start()
    {
        RandomizePosition();
    }
    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x=Random.Range(bounds.min.x, bounds.max.x);
        float y=Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
