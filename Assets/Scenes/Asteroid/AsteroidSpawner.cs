using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject prefab;
    public float spawnInterval = 2f;
    public float radius = 5f;

    float timer = 0f;

   void Update()
    {
        // shoot when timer runs out
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnAtRandomAngle();
        }
    }

    private float GetRandomAngle()
    {
        // Random spawn angle, degrees to radians
        float spawnAngleDeg = Random.Range(0f, 360f);
        float spawnAngleRad = spawnAngleDeg * Mathf.Deg2Rad;
        return spawnAngleRad;
    }

    void SpawnAtRandomAngle()
    {
        // Position on the circle
        float angle = GetRandomAngle();
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        Vector2 spawnPos = (Vector2)transform.position + offset;

        // Spawn
        GameObject asteroidGO = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Hand the enemy its precomputed direction + center
        Asteroid asteroid = asteroidGO.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.angleDeg = (angle * Mathf.Rad2Deg) + 180f; // face the center
            asteroid.center   = transform.position;
        }
    }
}