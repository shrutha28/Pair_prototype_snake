/*using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // Drag your CircleObstacleScript GameObject prefab here
    public BoxCollider2D gridArea; // Drag your grid area collider here
    public int obstacleCount = 4; // Number of obstacles to create

    void Start()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 randomPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
    }
}/*

/*using System.Collections;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public BoxCollider2D gridArea;
    public int obstacleCount = 4;

    private float hideDuration = 2f;
    private float visibilityDuration = 6f;
    private Coroutine[] visibilityCoroutines;

    void Start()
    {
        visibilityCoroutines = new Coroutine[obstacleCount];

        for (int i = 0; i < obstacleCount; i++)
        {
            GameObject obstacle = SpawnObstacle();
            visibilityCoroutines[i] = StartCoroutine(VisibilityTimer(obstacle.GetComponent<Renderer>()));
        }
    }

    private IEnumerator VisibilityTimer(Renderer renderer)
    {
        while (true)
        {
            renderer.enabled = true;
            RandomizePosition(renderer.gameObject);
            yield return new WaitForSeconds(visibilityDuration);
            renderer.enabled = false;
            yield return new WaitForSeconds(hideDuration);
        }
    }

    public GameObject SpawnObstacle()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 randomPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        return Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
    }

    public void RandomizePosition(GameObject gameObject)
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        gameObject.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
            {
                RandomizePosition(obstacle);
            }
        }
    }

    private void OnDestroy()
    {
        if (visibilityCoroutines != null)
        {
            foreach (var coroutine in visibilityCoroutines)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
        }
    }
}*/

using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // Drag your CircleObstacleScript GameObject prefab here
    public BoxCollider2D gridArea; // Drag your grid area collider here
    public int obstacleCount = 4; // Number of obstacles to create

    public float minInvisibleTime = 1.0f; // Minimum time for which the obstacle remains invisible
    public float maxInvisibleTime = 3.0f; // Maximum time for which the obstacle remains invisible
    public float minVisibleTime = 1.0f; // Minimum time for which the obstacle remains visible
    public float maxVisibleTime = 3.0f; // Maximum time for which the obstacle remains visible

    void Start()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            GameObject obstacle = SpawnObstacle();
            StartCoroutine(ToggleVisibility(obstacle));
        }
    }

    GameObject SpawnObstacle()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 randomPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        return Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
    }

    IEnumerator ToggleVisibility(GameObject obstacle)
    {
        SpriteRenderer sr = obstacle.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            yield break; // Exit if there's no SpriteRenderer component
        }

        while (true)
        {
            // Make the obstacle invisible
            sr.enabled = false;
            yield return new WaitForSeconds(Random.Range(minInvisibleTime, maxInvisibleTime));

            // Change the obstacle's position before making it visible again
            ChangeObstaclePosition(obstacle);

            // Make the obstacle visible
            sr.enabled = true;
            yield return new WaitForSeconds(Random.Range(minVisibleTime, maxVisibleTime));
        }
    }

    void ChangeObstaclePosition(GameObject obstacle)
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 randomPosition = new Vector3(x, y, 0.0f);
        obstacle.transform.position = randomPosition;
    }
}
