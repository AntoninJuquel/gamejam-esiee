using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    private Transform _camTransform;

    private Quaternion _originalRotation;

    private void Awake()
    {
        _camTransform = Camera.main.transform;
    }

    void Start()
    {
        _originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = _camTransform.rotation * _originalRotation;
    }
}