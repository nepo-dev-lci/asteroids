using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    [HideInInspector] public float angleDeg;   // set by spawner
    [HideInInspector] public Vector2 center;   // set by spawner

    public float speed = 2f;
    public float destroyDistance = 0.2f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Direction from the precomputed angle
        float angleRad = angleDeg * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
        // Alternative:
        // transform.position = Vector3.MoveTowards(transform.position, center, speed * Time.deltaTime);

        // Destroy when close to the center
        if (Vector2.Distance(newPos, center) <= destroyDistance)
        {
            Destroy(gameObject);
        }
    }

}