using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Setting")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float drag = 0.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // LimitSpeed();
    }

    public void Kick(Vector3 direction, float force)
    {
        CameraFollower.Instance.FollowBall(transform);
        _rb.linearVelocity = Vector3.zero;
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }

    void LimitSpeed()
    {
        if (_rb.linearVelocity.magnitude > maxSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
        }

        _rb.linearVelocity *= (1 - drag * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(CONSTANT.GameTag.GoalTag))
        {
            CameraFollower.Instance.OnBallGoal();
            Debug.Log(other.gameObject.name);
        }
    }
}