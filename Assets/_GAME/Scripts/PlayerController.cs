using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotationSpeed = 15f; 
    
    private Rigidbody _rb;
    private Vector3 _movementInput;
    private PlayerAnimBinding _playerAnim;
    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _playerAnim = GetComponent<PlayerAnimBinding>();
    }

    private void OnEnable()
    {
        PlayerAction.OnKickHandle += KickBall;
    }

    private void OnDisable()
    {
        PlayerAction.OnKickHandle -= KickBall;
    }

    private void Update()
    {
        _movementInput = new Vector3(Input.GetAxisRaw(CONSTANT.InputHandle.HorizontalInput), 0f, Input.GetAxisRaw(CONSTANT.InputHandle.VerticalInput)).normalized;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = _movementInput * moveSpeed;
        if (movement.magnitude >= 0.1f)
        {
            _playerAnim.UpdateMoveAnim(1f);
        }
        else
        {
            _playerAnim.UpdateMoveAnim(0f);
        }
        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);
    }

    private void RotatePlayer()
    {
        if (_movementInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void KickBall()
    {
        _playerAnim.UpdateKickAnim();
        Ball ball = GameManager.Instance
            .GetBallMinDistanceWithPlayer(transform)
            .GetComponent<Ball>();

        if (ball ==null) return;
        Vector3 dir = (GameManager.Instance.GetTargetMinDistanceWithBall(ball.transform).position - ball.transform.position).normalized;
        ball.Kick(dir, 20f);
        
    }
}