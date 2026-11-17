using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Aiming")]
    public Camera cam;

    [Header("Firing")]
    public GameObject bulletPrefab;
    public float fireInterval = 0.5f;
    private float originalFireInterval;
    public float fireIntervalWhenBoosted = 0.25f;
    public float speedBoostDuration = 2f;
    public float bulletSpeed = 8f;

    Timer fireTimer = new Timer();
    Timer speedBoostTimer = new Timer();

    SpriteRenderer sprite;

    void Start()
    {
        originalFireInterval = fireInterval;
    }

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
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

        HandleSpeedBoostAbility();

        // shoot when timer runs out
        if (fireTimer.IsFinished())
        {
            Fire();
            fireTimer.Start(fireInterval);
        }
    }

    void HandleSpeedBoostAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireInterval = fireIntervalWhenBoosted;
            speedBoostTimer.Start(speedBoostDuration);
            sprite.color = Color.red;
        }

        if (speedBoostTimer.IsFinished())
        {
            fireInterval = originalFireInterval;
            sprite.color = Color.white;
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