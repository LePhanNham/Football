using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCanvas : CanvasUI
{
    [SerializeField] private Button kickButton;
    protected override void Awake()
    {
        base.Awake();
        kickButton.gameObject.SetActive(false);
        kickButton.onClick.AddListener(PlayerAction.HandleKick);
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
