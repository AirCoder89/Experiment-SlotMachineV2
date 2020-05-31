using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PayLinePopUp : MonoBehaviour
{
    [BoxGroup("Animation")]
    [SerializeField] private Vector2 startScale = Vector2.one;
    [BoxGroup("Animation")]
    [SerializeField] private Vector2 startPosition = Vector2.zero;
    [BoxGroup("Animation")] [SerializeField][Range(0,2)]
    private float duration;
    [BoxGroup("Animation")] [SerializeField]
    private Ease ease;
    
    
    [SerializeField] private string paylineName;
    [SerializeField] private Image bg;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform holder;
    [SerializeField] private Button closeBtn;
    [SerializeField] private ContentSizeFitter sizeFitter;
    
    private List<Transform> _paylineItems;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(List<PayLines> paylines)
    {
        if(_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
        ClearHolder();
        
        _paylineItems = new List<Transform>();
        sizeFitter.enabled = false;
        foreach (var p in paylines)
        {
            var payline = PoolManager.Pools[UIManager.Instance.poolName].Spawn(this.paylineName, this.holder).gameObject
                .GetComponent<PayLineItem>();
            payline.Initialize(p.color, p.points.ToString(), p.payline);
            payline.transform.localScale = Vector3.one;
            _paylineItems.Add(payline.transform);
        }
        sizeFitter.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(holder);
        closeBtn.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        
        _rectTransform.localScale = startScale;
        _rectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 0;
        bg.enabled = true;
        bg.DOFade(0, 0);

        bg.DOFade(0.5f, duration);
        canvasGroup.DOFade(1, duration);
        _rectTransform.DOScale(Vector3.one, duration).SetEase(ease);
        _rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(ease);
    }

    private void ClosePanel()
    {
        bg.DOFade(0f, duration);
        canvasGroup.DOFade(0, duration);
        _rectTransform.DOScale(startScale, duration).SetEase(ease);
        _rectTransform.DOAnchorPos(startPosition, duration).SetEase(ease).OnComplete(() =>
        {
            bg.enabled = false;
            gameObject.SetActive(false);
        });
    }

    private void ClearHolder()
    {
        if(_paylineItems == null || _paylineItems.Count == 0) return;
        foreach (var p in _paylineItems)
        {
            PoolManager.Pools[UIManager.Instance.poolName].Despawn(p,PoolManager.Pools[UIManager.Instance.poolName].transform);
        }
    }
}
