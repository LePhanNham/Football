using UnityEngine;
using System.Collections;
using Base.Singleton;
using Unity.Cinemachine;

public class CameraFollower : SingletonMono<CameraFollower>
{
    [SerializeField] private CinemachineCamera camPlayer;
    [SerializeField] private CinemachineCamera camBall;

    [SerializeField] private float blendDelay = 2f;
    [SerializeField] private float followTimeout = 4f;

    private Coroutine _returnRoutine;
    private bool _isPlayerReady = true;

    public bool IsPlayerReady => _isPlayerReady;

    protected override void OnAwake()
    {
        SetFollowPlayer();
    }

    public void FollowBall(Transform ball)
    {
        if (ball == null) return;

        _isPlayerReady = false;
        camBall.Follow = ball;
        camBall.Priority = 20;
        camPlayer.Priority = 10;
        StartReturnRoutine(followTimeout);
    }

    public void OnBallGoal()
    {
        StartReturnRoutine(blendDelay);
    }

    private void StartReturnRoutine(float delay)
    {
        if (_returnRoutine != null)
        {
            StopCoroutine(_returnRoutine);
        }

        _returnRoutine = StartCoroutine(ReturnToPlayer(delay));
    }

    IEnumerator ReturnToPlayer(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        SetFollowPlayer();
        _returnRoutine = null;
    }

    private void SetFollowPlayer()
    {
        _isPlayerReady = true;
        camBall.Follow = null;
        camPlayer.Priority = 20;
        camBall.Priority = 10;
    }
}