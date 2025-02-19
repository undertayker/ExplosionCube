using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action<Vector3, Vector3> Clicked;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(transform.localScale, transform.position);
        Destroy(gameObject);
    }
}