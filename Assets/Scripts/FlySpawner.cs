
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flyPrefab;
    private BoxCollider2D _collider;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    private float initialMinSpawnRate;
    private float initialMaxSpawnRate;
    private float spawnRate = 0f;
    private float nextSpawn = 0f;
    public List<Fly> spawnedFlies = new List<Fly>();

    [SerializeField] private List<float> increaseSpawnRateAt = new List<float>();
    public int currentIncreaseIndex = 0;
    private float baseTime = 0f;

    public static FlySpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        initialMinSpawnRate = minSpawnRate;
        initialMaxSpawnRate = maxSpawnRate;

        UIBehaviour.instance.onGameOverEvent.AddListener(() =>
        {
            foreach (Fly fly in spawnedFlies)
            {
                Destroy(fly.gameObject);
            }

            spawnedFlies = new List<Fly>();
        });

        UIBehaviour.instance.onGameStartEvent.AddListener(() =>
        {
            baseTime = Time.time;
            Debug.Log("Base time");
            minSpawnRate = initialMinSpawnRate;
            maxSpawnRate = initialMaxSpawnRate;
            currentIncreaseIndex = 0;
            nextSpawn = 0f;
        });
    }

    void Update()
    {
        if (!UIBehaviour.instance.isPlaying) return;

        float currentTime = Time.time - baseTime;
        if (currentTime > nextSpawn && currentTime < 100f)
        {
            spawnFly();
        }

        if (increaseSpawnRateAt.Count - 1 >= currentIncreaseIndex && currentTime > increaseSpawnRateAt[currentIncreaseIndex])
        {
            currentIncreaseIndex++;
            minSpawnRate -= 0.15f;
            maxSpawnRate -= 0.15f;

            if (minSpawnRate < 0f) minSpawnRate = 0f;
            if (maxSpawnRate < 0f) maxSpawnRate = 0f;
        }

        if (currentTime > 100f && spawnedFlies.Count == 0)
        {
            UIBehaviour.instance.onGameWin();
        }
    }

    public void increaseScale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    private void spawnFly()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        nextSpawn = Time.time - baseTime + spawnRate;

        float x = Random.Range(0, 2) > 0.5f ? _collider.bounds.min.x : _collider.bounds.max.x;
        float y = Random.Range(_collider.bounds.min.y, _collider.bounds.max.y);

        GameObject fly = Instantiate(flyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        spawnedFlies.Add(fly.GetComponent<Fly>());
    }
}
