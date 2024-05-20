
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flyPrefab;
    private BoxCollider2D _collider;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    private float spawnRate = 0f;
    private float lastSpawn = 0f;
    public List<Fly> spawnedFlies = new List<Fly>();

    [SerializeField] private List<float> increaseSpawnRateAt = new List<float>();
    public int currentIncreaseIndex = 0;

    public static FlySpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (lastSpawn + spawnRate < Time.time && Time.time < 100f)
        {
            spawnFly();
        }

        if (increaseSpawnRateAt.Count - 1 >= currentIncreaseIndex && Time.time > increaseSpawnRateAt[currentIncreaseIndex])
        {
            currentIncreaseIndex++;
            minSpawnRate -= 0.2f;
            maxSpawnRate -= 0.2f;
        }
    }

    public void increaseScale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    private void spawnFly()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        lastSpawn = Time.time;

        float x = Random.Range(0, 2) > 0.5f ? _collider.bounds.min.x : _collider.bounds.max.x;
        float y =Random.Range(_collider.bounds.min.y, _collider.bounds.max.y);

        GameObject fly = Instantiate(flyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        spawnedFlies.Add(fly.GetComponent<Fly>());
    }
}
