using System;
using System.Collections.Generic;
using Base.Singleton;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private List<Transform> targetList = new List<Transform>();
    [SerializeField] private List<Transform> ballList = new List<Transform>();
    protected override void OnAwake()
    {
        
    }


    public Transform GetTargetMinDistanceWithBall(Transform ball)
    {
        if (targetList == null || targetList.Count == 0 || ball == null)
            return null;

        Transform closest = null;
        float minSqrDist = Mathf.Infinity;

        Vector3 pos = ball.position;

        foreach (var t in targetList)
        {
            if (t == null) continue;

            float sqrDist = (t.position - pos).sqrMagnitude;

            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closest = t;
            }
        }

        return closest;
    }

    public Transform GetBallMaxDistanceWithPlayer(Transform player)
    {
        if (ballList == null || ballList.Count == 0 || player == null)
            return null;

        Transform farthest = null;
        float maxSqrDist = -Mathf.Infinity;

        Vector3 playerPos = player.position;

        foreach (var ball in ballList)
        {
            if (ball == null) continue;
            float sqrDist = (ball.position - playerPos).sqrMagnitude;

            if (sqrDist > maxSqrDist)
            {
                maxSqrDist = sqrDist;
                farthest = ball;
            }
        }

        return farthest;
    }
    
    public Transform GetBallMinDistanceWithPlayer(Transform player)
    {
        if (ballList == null || ballList.Count == 0 || player == null)
            return null;

        Transform closest = null;
        float minSqrDist = Mathf.Infinity;

        Vector3 playerPos = player.position;

        foreach (var ball in ballList)
        {
            if (ball == null) continue;
            float sqrDist = (ball.position - playerPos).sqrMagnitude;

            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closest = ball;
            }
        }

        return closest;
    }
}
