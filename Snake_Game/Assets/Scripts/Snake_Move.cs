using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake_Move : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;
    public int initialSize = 2;
    public bool isPaused = false;
    public FoodScript foodScript; // Reference to the FoodScript to access the ResetFoodCount method
                                  //public GameTimer gameTimer; // Reference to the GameTimer script
    public GameObject gameOverPanel; // Reference to the panel containing the "Game Over" text and restart button
    public float moveSpeed = 0.6f; // Time in seconds between each move
    private float moveTimer = 0.0f; // Timer to keep track of movement

    public void AttractToPosition(Vector3 targetPosition, Vector2 force)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        _direction += force * Time.fixedDeltaTime;
        
        ResetforObst();
        foodScript.FoodObs();
    }
    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true); // Show the "Game Over" panel
        isPaused = true; // Pause the game
    }

    public void Start()
    {
        ResetState();
        foodScript.ResetFoodCount();
    }
    public void RestartGame()
    {
        gameOverPanel.SetActive(false); // Hide the "Game Over" panel
        ResetState(); // Reset the snake's state
        foodScript.ResetFoodCount(); // Reset the food count
        isPaused = false; // Unpause the game
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused) return;
      
      
            if (Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
            }
        }
    

    public void FixedUpdate()
    {
        if (isPaused) return;

        moveTimer += Time.fixedDeltaTime; // Increment the timer by the time since the last frame

        if (moveTimer >= moveSpeed) // Check if it's time to move
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }
            this.transform.position = new Vector3(
               Mathf.Round(this.transform.position.x) + _direction.x,
               Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
                );

            moveTimer = 0.0f; // Reset the timer
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
        this.transform.position = new Vector3(0, 0, 0);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
            foodScript.ResetFoodCount();
            RestartGame();
            ShowGameOverScreen();

        }
  
    }
    public void ResetforObst()
    {
        this.transform.position = new Vector3(0, 0, 0);

    }
    public void IncreaseSegmentByTwo()
    {
        initialSize += 2; // Increase the initial size by 2
    }


}
