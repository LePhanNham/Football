using System;
using UnityEngine;

public static class PlayerAction
{
    public static event Action OnKickStarted;
    public static event Action OnKickEnded;
    public static event Action OnKickHandle;
    public static event Action OnAutoKick;
    public static event Action<Vector3> OnGoal;
    public static event Action OnReset;

    public static void StartedKick()
    {
        OnKickStarted?.Invoke();
    }
    public static void EndedKick()
    {
        OnKickEnded?.Invoke();
    }
    public static void HandleKick()
    {
        OnKickHandle?.Invoke();
    }
    public static void AutoKick()
    {
        OnAutoKick?.Invoke();
    }

    public static void HandleGoal(Vector3 position)
    {
        OnGoal?.Invoke(position);
    }

    public static void HandleReset()
    {
        OnReset?.Invoke();
    }
}
