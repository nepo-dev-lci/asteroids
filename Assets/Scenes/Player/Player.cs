using Unity.VisualScripting;
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

    public float normalFireInterval = 0.5f;
    public float rapidFireInterval = 0.15f;
    private Timer rapidFireActiveTime = new Timer();
    private Timer rapidFireCooldown = new Timer();
    public GameObject rapidFireUI;
    private RadialProgressBar rapidFireProgress;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rapidFireProgress = new RadialProgressBar(rapidFireUI);
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

        if (rapidFireCooldown.IsFinished() && Input.GetKeyDown(KeyCode.Q))
        {
            rapidFireActiveTime.Begin(3f);
            rapidFireCooldown.Begin(5f);
            fireInterval = rapidFireInterval;
            sprite.color = Color.coral;
        }
        rapidFireProgress.SetProgress(rapidFireCooldown.GetProgress());

        if (rapidFireActiveTime.IsFinished())
        {
            fireInterval = normalFireInterval;
            sprite.color = Color.white;
        }

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