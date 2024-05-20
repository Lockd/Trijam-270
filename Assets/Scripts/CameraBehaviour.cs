using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float maxSize = 14f;
    private Camera _camera;
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private AnimationCurve curve;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {

        if (_camera.orthographicSize >= maxSize) return;
        
        // float additionalScale = scaleSpeed * Time.deltaTime;

        // curve.Evaluate(Time.time / 100f);
        
        _camera.orthographicSize = curve.Evaluate(Time.time);
        FlySpawner.instance.increaseScale(_camera.orthographicSize / 2);
    }
}
