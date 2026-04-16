using UnityEngine;
using System.Collections;
using Base.Singleton;
using Unity.Cinemachine;

public class CameraFollower : SingletonMono<CameraFollower>
{
    [SerializeField] private CinemachineCamera camPlayer;
    [SerializeField] private CinemachineCamera camBall;

    [SerializeField] private float blendDelay = 2f;

    private Coroutine _returnRoutine;

    protected override void OnAwake()
    {
        SetFollowPlayer();
    }

    public void FollowBall(Transform ball)
    {
        if (ball == null) return;

        camBall.Follow = ball;
        camBall.Priority = 20;
        camPlayer.Priority = 10;
        if (_returnRoutine != null)
        {
            StopCoroutine(_returnRoutine);
            _returnRoutine = null;
        }
    }

    public void OnBallGoal()
    {
        if (_returnRoutine != null)
            StopCoroutine(_returnRoutine);

        _returnRoutine = StartCoroutine(ReturnToPlayer());
    }

    IEnumerator ReturnToPlayer()
    {
        yield return new WaitForSeconds(blendDelay);

        SetFollowPlayer();
    }

    private void SetFollowPlayer()
    {
        camPlayer.Priority = 20;
        camBall.Priority = 10;
    }
}