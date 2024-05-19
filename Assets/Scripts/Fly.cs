using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class Fly : MonoBehaviour
{
    [SerializeField] private float deathAnimationDuration = 0.5f;
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float actualSpeed = 1f;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    void Start()
    {
        _rigidbody  = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        actualSpeed= Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        Transform target = Monk.instance.transform;
        Vector2 direction = Vector2.MoveTowards(transform.position, target.position, actualSpeed * Time.deltaTime);
        _rigidbody.MovePosition(direction);
    }

    IEnumerator Die() {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Should die, player touched me!");
            StartCoroutine(Die());
        }
    }
}
