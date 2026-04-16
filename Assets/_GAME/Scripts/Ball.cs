using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    [Header("Setting")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float drag = 0.5f;
    // private bool _used =  false;
    //
    // public bool Used
    // {
    //     get { return _used; }
    //     set { _used = value; }
    // }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
            OnGoal(transform);
        }
    }
    
    public void OnGoal(Transform ball)
    {
        PlayerAction.HandleGoal(ball.position);
        StartCoroutine(GoalEffectRoutine());
        CameraFollower.Instance.OnBallGoal();
    }
    IEnumerator GoalEffectRoutine()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
    }
    
}