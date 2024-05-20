using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Cursor : MonoBehaviour
{
    CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _collider.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _collider.enabled = false;
        }
    }
}
