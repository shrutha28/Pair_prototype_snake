using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CircleObstacleScript : MonoBehaviour
{
    public float attractionRadius = 5.0f;
    public float attractionForce = 1.0f;

    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = attractionRadius;
        circleCollider.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 direction = transform.position - other.transform.position;
            other.GetComponent<Snake_Move>().AttractToPosition(transform.position, direction.normalized * attractionForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
