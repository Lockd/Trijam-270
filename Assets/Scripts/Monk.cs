using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Monk : MonoBehaviour
{
    [SerializeField] private float growthSpeed = 3f;
    [SerializeField] private float disturbanceRangeReduction = 0.25f;
    private float initialScale;

    public static Monk instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        initialScale = transform.localScale.x;
        UIBehaviour.instance.onGameStartEvent.AddListener(() =>
        {
            transform.localScale = new Vector3(initialScale, initialScale, 1);
        });
    }

    void Update()
    {
        float currentSize = transform.localScale.x + growthSpeed * Time.deltaTime;
        if (currentSize < 0f) currentSize = 0f;
        
        transform.localScale = new Vector3(currentSize, currentSize, 1);
    }

    public void disturb()
    {
        float currentSize = transform.localScale.x - disturbanceRangeReduction;
        transform.localScale = new Vector3(currentSize, currentSize, 1);

        if (currentSize <= 0)
        {
            UIBehaviour.instance.onGameOver();
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
