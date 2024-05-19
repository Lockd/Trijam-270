
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flyPrefab;
    private BoxCollider2D _collider;
    private float spawnRate = 1f;
    private float lastSpawn = 0f;
    public List<Fly> spawnedFlies = new List<Fly>();

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (lastSpawn + spawnRate < Time.time)
        {
            spawnFly();
        }
    }

    private void spawnFly()
    {
        lastSpawn = Time.time;

        // should spawn at the border of the collider
        float guesserX = Random.Range(0, 2);
        float guesserY = Random.Range(0, 2);

        float x = guesserX > 0.5f ? _collider.bounds.min.x : _collider.bounds.max.x;
        float y = guesserY > 0.5f ? _collider.bounds.min.y : _collider.bounds.max.y;

        GameObject fly = Instantiate(flyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        spawnedFlies.Add(fly.GetComponent<Fly>());
    }
}
