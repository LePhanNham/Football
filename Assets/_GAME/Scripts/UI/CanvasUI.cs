using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;


[RequireComponent(typeof(CanvasGroup))]
public abstract class CanvasUI : MonoBehaviour
{
    [FormerlySerializedAs("PanelType")] public UIPanelType panelType;
    
    [Header("Animation Settings")]
    [SerializeField] private float animDuration = 0.3f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;

    protected CanvasGroup CanvasGroup;
    protected RectTransform RectTransform;

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        RectTransform = GetComponent<RectTransform>();
    }

    public virtual void Setup()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show(System.Action onCompleted = null)
    {
        gameObject.SetActive(true);
        
        CanvasGroup.DOKill();
        RectTransform.DOKill();

        CanvasGroup.alpha = 0f;
        RectTransform.localScale = Vector3.one * 0.8f;

        CanvasGroup.DOFade(1f, animDuration).SetUpdate(true);
        RectTransform.DOScale(Vector3.one, animDuration)
            .SetEase(showEase)
            .SetUpdate(true)
            .OnComplete(() => 
            {
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;
                onCompleted?.Invoke();
            });
    }

    public virtual void Hide(System.Action onCompleted = null)
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;

        CanvasGroup.DOKill();
        RectTransform.DOKill();

        // Chạy hiệu ứng ẩn đi (Mờ dần, thụt nhỏ lại)
        CanvasGroup.DOFade(0f, animDuration * 0.8f).SetUpdate(true);
        RectTransform.DOScale(Vector3.one * 0.8f, animDuration * 0.8f)
            .SetEase(hideEase)
            .SetUpdate(true)
            .OnComplete(() => 
            {
                gameObject.SetActive(false);
                onCompleted?.Invoke();
            });
    }
}
public enum UIPanelType
{
    None,
    Home,
    Loading,
    Gameplay,
    Win,
    Lose,
    Setting
}
