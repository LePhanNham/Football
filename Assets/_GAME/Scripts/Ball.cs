using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _hasScored;
    [Header("Setting")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float drag = 0.5f;
    [SerializeField] private float deactivateDelayAfterGoal = 2.5f;
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

    public bool CanBeKicked => !_hasScored;
    public bool CanAutoKick => _rb != null && _rb.IsSleeping();
    

    public void Kick(Vector3 direction, float force)
    {
        _rb.WakeUp();
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
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
        if (_hasScored)
            return;

        if (other.gameObject.CompareTag(CONSTANT.GameTag.GoalTag))
        {
            _hasScored = true;
            OnGoal(transform);
        }
    }
    
    public void OnGoal(Transform ball)
    {
        PlayerAction.HandleGoal(ball.position);
        CameraFollower.Instance?.FollowBall(transform);
        StartCoroutine(DeactivateBallAfterGoalRoutine());
        StartCoroutine(GoalEffectRoutine());
        CameraFollower.Instance?.OnBallGoal();
    }

    private IEnumerator DeactivateBallAfterGoalRoutine()
    {
        yield return new WaitForSecondsRealtime(deactivateDelayAfterGoal);
        gameObject.SetActive(false);
    }
    IEnumerator GoalEffectRoutine()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
    }
    
}