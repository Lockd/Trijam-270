using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Monk : MonoBehaviour
{
    [SerializeField] private float growthSpeed = 3f;
    [SerializeField] private float disturbanceRangeReduction = 0.25f;

    public static Monk instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        float currentSize = transform.localScale.x + growthSpeed * Time.deltaTime;
        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    public void disturb()
    {
        float currentSize = transform.localScale.x - disturbanceRangeReduction;
        transform.localScale = new Vector3(currentSize, currentSize, 1);

        if (currentSize <= 0)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fly fly = other.gameObject.GetComponent<Fly>();
        if (fly == null) return;

        Debug.Log("Should disturb, enemy touched me!");
        disturb();
        fly.onDeath();
    }
}
