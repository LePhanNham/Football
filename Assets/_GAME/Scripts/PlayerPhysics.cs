using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(CONSTANT.GameTag.BallTag))
            return;

        Ball ball = other.GetComponent<Ball>();
        if (ball == null || !ball.CanBeKicked)
            return;

        if (other.CompareTag(CONSTANT.GameTag.BallTag))
        {
            PlayerAction.StartedKick();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(CONSTANT.GameTag.BallTag))
            return;

        Ball ball = other.GetComponent<Ball>();
        if (ball == null || !ball.CanBeKicked)
            return;

        if (other.CompareTag(CONSTANT.GameTag.BallTag))
        {
            PlayerAction.EndedKick();
        }
    }
}
