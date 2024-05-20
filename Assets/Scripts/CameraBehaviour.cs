using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float maxSize = 14f;
    private Camera _camera;
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private AnimationCurve curve;
    private float baseTime = 0f;
    private float startSize;

    void Start()
    {
        _camera = GetComponent<Camera>();

        baseTime = Time.time;

        startSize = _camera.orthographicSize;

        UIBehaviour.instance.onGameStartEvent.AddListener(() =>
        {
            baseTime = Time.time;
            _camera.orthographicSize = startSize;
        });
    }

    void Update()
    {
        if (_camera.orthographicSize >= maxSize || !UIBehaviour.instance.isPlaying) return;

        _camera.orthographicSize = curve.Evaluate(Time.time - baseTime);
        FlySpawner.instance.increaseScale(_camera.orthographicSize / 2.5f);
    }
}
