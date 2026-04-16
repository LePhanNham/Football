using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCanvas : CanvasUI
{
    [SerializeField] private Button kickButton;
    [SerializeField] private Button autoKickButton;
    [SerializeField] private Button resetButton;
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        kickButton.gameObject.SetActive(false);
        autoKickButton.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
        kickButton.onClick.AddListener(PlayerAction.HandleKick);
        autoKickButton.onClick.AddListener(PlayerAction.AutoKick);
        resetButton.onClick.AddListener(PlayerAction.HandleReset);
        
    }
    
    private void OnEnable()
    {
        PlayerAction.OnKickStarted += KickButtonReady;
        PlayerAction.OnKickEnded += KickButtonEnd;
    }

    private void OnDisable()
    {
        PlayerAction.OnKickStarted -= KickButtonReady;
        PlayerAction.OnKickEnded -= KickButtonEnd;
    }

    private void KickButtonReady()
    {
        kickButton.gameObject.SetActive(true);
    }

    private void KickButtonEnd()
    {
        kickButton.gameObject.SetActive(false);
    }
    
    
}
