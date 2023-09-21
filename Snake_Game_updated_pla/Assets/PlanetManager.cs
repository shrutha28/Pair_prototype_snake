using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public BoxCollider2D gridArea;
    private float hideDuration = 2f;
    private float visibilityDuration = 6f;
    private Coroutine visibilityCoroutine;
    public GameObject WarningIndicatorPrefab; // Reference to your square prefab
    private GameObject WarningIndicator;

    public void Start()
    {
        visibilityCoroutine = StartCoroutine(VisibilityTimer());
    }

    private IEnumerator VisibilityTimer()
    {
        Renderer renderer = PlanetPrefab.GetComponent<Renderer>();
        Canvas canvas = FindObjectOfType<Canvas>(); // Find the canvas in your scene

        while (true)
        {
            renderer.enabled = false;

            if (Snake_Move.IsGameOver) // Adjust according to your game's game over check
            {
                yield return null;
                continue;
            }

            RandomizePosition(); // Call this first to determine the new position

            // Instantiate the warning indicator at the new position of the planet
            WarningIndicator = Instantiate(WarningIndicatorPrefab, PlanetPrefab.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);

            Destroy(WarningIndicator); // Destroy the warning indicator
            renderer.enabled = true;
            yield return new WaitForSeconds(visibilityDuration);

            renderer.enabled = false;
            yield return new WaitForSeconds(hideDuration - 1f); // Adjust the hide duration to account for the warning duration
        }
    }


    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        Renderer renderer = PlanetPrefab.GetComponent<Renderer>();
        float planetHalfWidth = renderer.bounds.size.x / 2;
        float planetHalfHeight = renderer.bounds.size.y / 2;

        float x, y;
        Vector2 center = new Vector2(bounds.extents.x, bounds.extents.y);
        float minDistanceFromCenter = 2.0f; // Minimum distance from the center

        do
        {
            x = Random.Range(bounds.min.x + planetHalfWidth, bounds.max.x - planetHalfWidth);
            y = Random.Range(bounds.min.y + planetHalfHeight, bounds.max.y - planetHalfHeight);
        } while (Vector2.Distance(center, new Vector2(x, y)) < minDistanceFromCenter);

        PlanetPrefab.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        //Debug.Log("Planet Position: " + PlanetPrefab.transform.position);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }

    private void OnDestroy()
    {
        if (visibilityCoroutine != null)
        {
            StopCoroutine(visibilityCoroutine);
        }
    }
}
