using UnityEngine;

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
}