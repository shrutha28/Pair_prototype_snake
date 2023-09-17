using System.Collections;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public BoxCollider2D gridArea;
    private float hideDuration = 2f;
    private float visibilityDuration = 6f;
    private Coroutine visibilityCoroutine;

    public void Start()
    {
        visibilityCoroutine = StartCoroutine(VisibilityTimer());
    }

    private IEnumerator VisibilityTimer()
{
    Renderer renderer = PlanetPrefab.GetComponent<Renderer>();
    while (true)
    {
        renderer.enabled = true;
        RandomizePosition();
        yield return new WaitForSeconds(visibilityDuration);
        renderer.enabled = false;
        yield return new WaitForSeconds(hideDuration);
    }
}


    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        PlanetPrefab.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
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
