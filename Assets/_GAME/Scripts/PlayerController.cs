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
        PlayerAction.OnAutoKick += AutoKickBall;
    }

    private void OnDisable()
    {
        PlayerAction.OnKickHandle -= KickBall;
        PlayerAction.OnAutoKick -= AutoKickBall;
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
        if (!CanKickNow())
        {
            PlayerAction.HandleKickBlocked("Waiting...");
            return;
        }

        GameManager gameManager = GameManager.Instance;
        Transform ballTransform = gameManager.GetBallMinDistanceWithPlayer(transform);
        if (ballTransform == null)
        {
            PlayerAction.HandleKickBlocked("NO BALL AVAILABLE");
            return;
        }

        Ball ball = ballTransform.GetComponent<Ball>();
        if (ball == null)
            return;

        Transform target = gameManager.GetTargetMinDistanceWithBall(ball.transform);
        if (target == null)
            return;

        _playerAnim.UpdateKickAnim();
        Vector3 dir = (target.position - ball.transform.position).normalized;
        ball.Kick(dir, 20f);
        
    }

    private void AutoKickBall()
    {
        if (!CanKickNow())
        {
            PlayerAction.HandleKickBlocked("Waiting...");
            return;
        }

        GameManager gameManager = GameManager.Instance;
        Transform ballTransform = gameManager.GetBallMaxDistanceWithPlayer(transform);
        if (ballTransform == null)
        {
            if (gameManager.HasAnyAvailableBall(false))
            {
                PlayerAction.HandleKickBlocked("Ball is not ready for auto kick yet.");
            }
            else
            {
                PlayerAction.HandleKickBlocked("NO BALL AVAILABLE");
            }
            return;
        }

        Ball ball = ballTransform.GetComponent<Ball>();
        if (ball == null)
            return;

        Transform target = gameManager.GetTargetMinDistanceWithBall(ball.transform);
        if (target == null)
            return;

        _playerAnim.UpdateKickAnim();
        Vector3 dir = (target.position - ball.transform.position).normalized;
        ball.Kick(dir, 20f);
    }

    private bool CanKickNow()
    {
        return CameraFollower.Instance == null || CameraFollower.Instance.IsPlayerReady;
    }
}