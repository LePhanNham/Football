using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimBinding : MonoBehaviour
{
    [FormerlySerializedAs("_animator")] 
    [SerializeField] private Animator animator;
    public Animator Animator => animator;

    private int _speedHash;
    private int _normalHash;

    private void Awake()
    {
        Init();
        
    }

    private void Start()
    {
        animator.SetTrigger(_normalHash);
    }

    private void Init()
    {
        _speedHash = Animator.StringToHash("Blend");
        _normalHash = Animator.StringToHash("normal");
    }
    
    public void UpdateMoveAnim(float speedValue)
    {
        animator.SetFloat(_speedHash, speedValue);
    }

    
}