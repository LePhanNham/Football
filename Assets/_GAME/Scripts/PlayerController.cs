using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody _rb;
    private Vector3 _movementInput;
    [Header("Animation")]
    
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleInput();
    }
    
    
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void HandleInput()
    {
        _movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void MovePlayer()
    {
        Vector3 movement = _movementInput * moveSpeed;
        if (_rb.linearVelocity.magnitude > 0)
        {
            _animator.SetTrigger("Move");
        }
        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);
    }

    private void RotatePlayer()
    {
        if (_movementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f,targetAngle,0f);
            
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
