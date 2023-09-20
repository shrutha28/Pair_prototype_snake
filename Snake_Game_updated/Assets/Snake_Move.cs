using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Move : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;

    public float pullRange = 8f; // The range within which the magnetic field can affect the snake
    public float pullStrength = 19f; // The strength of the magnetic field's pulling force
    public FoodScript foodscript;
    public GameObject gameOverPanel;
    public bool isPaused=false;
    public static bool IsGameOver = false;


    
    public void Start()
    {
        ResetState();
        foodscript.ResetFoodCount();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    public void FixedUpdate()
    {
        if (isPaused) return;

        ApplyMagneticPull();

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + _direction.x,
           Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

private void ApplyMagneticPull()
{
    foreach (GameObject magneticField in GameObject.FindGameObjectsWithTag("Planet"))
    {
        float distanceToField = Vector2.Distance(transform.position, magneticField.transform.position);

        if (distanceToField < pullRange)
        {
            Vector2 directionToField = (magneticField.transform.position - transform.position).normalized;
            float pullForce = pullStrength * (1 - (distanceToField / pullRange));
            _direction = Vector2.Lerp(_direction, directionToField, pullForce * Time.fixedDeltaTime);
        }
    }
}


public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = (_segments[_segments.Count - 1].position);
        _segments.Add(segment);
    }
    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle") // for wall
        {
            ResetState();
            foodscript.ResetFoodCount();
            RestartGame();
            ShowGameOverScreen();
        }
        else if (other.tag == "Planet")
        {
            ResetforObst();
            foodscript.FoodObs();
        }
    }
    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true); // Show the "Game Over" panel
        isPaused = true; // Pause the game

        Snake_Move.IsGameOver = true;
    }
    public void RestartGame()
    {
        gameOverPanel.SetActive(false); // Hide the "Game Over" panel
        ResetState(); // Reset the snake's state
        foodscript.ResetFoodCount(); // Reset the food count
        isPaused = false; // Unpause the game

        Snake_Move.IsGameOver = false;
    }
    public void ResetforObst(){
        this.transform.position=new Vector3(0,0,0);
    }

}