using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Cursor : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clickSounds;
    CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!UIBehaviour.instance.isPlaying) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _collider.enabled = true;
            playSound();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _collider.enabled = false;
        }
    }

    private void playSound()
    {
        int randomIndex = Random.Range(0, clickSounds.Count);
        AudioSource.PlayClipAtPoint(clickSounds[randomIndex], Vector3.zero);
    }
}
