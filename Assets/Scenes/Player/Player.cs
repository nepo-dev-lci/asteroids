using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    public Camera cam;

    [Header("Firing")]
    public GameObject bulletPrefab;
    public float fireInterval = 0.5f;
    public float bulletSpeed = 8f;

    float timer;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    float GetAngleFromMouse()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (Vector2)mouseWorld - (Vector2)transform.position;

        return Mathf.Atan2(direction.y, direction.x);
    }

    void Update()
    {
        // point to player
        float angle = GetAngleFromMouse();
        Vector2 newUp = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        transform.up = newUp;

        // shoot when timer runs out
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            timer = 0f;
            Fire();
        }
    }

    void Fire()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletGO.transform.up = transform.up;

        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.speed = bulletSpeed;
    }
}