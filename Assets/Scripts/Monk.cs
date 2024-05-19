using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Monk : MonoBehaviour
{
    // private SpriteRenderer spriteRenderer;
    [SerializeField] private float growthSpeed = 3f;
    [SerializeField] private float disturbanceRangeReduction = 0.25f;
    private CircleCollider2D _collider;

    public static Monk instance;
    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        _collider.radius += growthSpeed * Time.deltaTime;
        transform.localScale = new Vector3(_collider.radius, _collider.radius, 1);
    }
}
