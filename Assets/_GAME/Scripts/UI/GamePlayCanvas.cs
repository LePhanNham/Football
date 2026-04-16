using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCanvas : CanvasUI
{
    [SerializeField] private Button kickButton;
    [SerializeField] private Button autoKickButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI kickMessageText;

    private Sequence _messageSequence;
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
        HideKickMessageImmediate();
        
    }
    
    private void OnEnable()
    {
        PlayerAction.OnKickStarted += KickButtonReady;
        PlayerAction.OnKickEnded += KickButtonEnd;
        PlayerAction.OnKickBlocked += ShowKickMessage;
    }

    private void OnDisable()
    {
        PlayerAction.OnKickStarted -= KickButtonReady;
        PlayerAction.OnKickEnded -= KickButtonEnd;
        PlayerAction.OnKickBlocked -= ShowKickMessage;
    }

    private void Update()
    {
        bool canKick = CameraFollower.Instance == null || CameraFollower.Instance.IsPlayerReady;
        kickButton.interactable = canKick;
        autoKickButton.interactable = canKick;
    }

    private void KickButtonReady()
    {
        kickButton.gameObject.SetActive(true);
    }

    private void KickButtonEnd()
    {
        kickButton.gameObject.SetActive(false);
    }

    private void ShowKickMessage(string message)
    {
        if (kickMessageText == null)
            return;

        if (_messageSequence != null)
        {
            _messageSequence.Kill();
            _messageSequence = null;
        }

        kickMessageText.DOKill();
        kickMessageText.gameObject.SetActive(true);
        kickMessageText.text = message;
        kickMessageText.alpha = 0f;

        _messageSequence = DOTween.Sequence()
            .SetUpdate(true)
            .Append(kickMessageText.DOFade(1f, 0.2f))
            .AppendInterval(1.2f)
            .Append(kickMessageText.DOFade(0f, 0.25f))
            .OnComplete(() =>
            {
                if (kickMessageText != null)
                    kickMessageText.gameObject.SetActive(false);

                _messageSequence = null;
            });
    }

    private void HideKickMessageImmediate()
    {
        if (kickMessageText == null)
            return;

        if (_messageSequence != null)
        {
            _messageSequence.Kill();
            _messageSequence = null;
        }

        kickMessageText.DOKill();
        kickMessageText.alpha = 0f;
        kickMessageText.gameObject.SetActive(false);
    }

}
