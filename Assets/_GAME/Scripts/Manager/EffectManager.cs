using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject goalVFX;

    private void OnEnable()
    {
        PlayerAction.OnGoal += PlayVFX;
    }

    private void OnDisable()
    {
        PlayerAction.OnGoal -= PlayVFX;
    }

    void PlayVFX(Vector3 pos)
    {
        Instantiate(goalVFX, pos, Quaternion.identity);
    }
}