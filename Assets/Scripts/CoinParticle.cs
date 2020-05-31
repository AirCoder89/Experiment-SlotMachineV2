using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

public class CoinParticle : MonoBehaviour
{
    [SerializeField] private CanvasGroup cGroup;
    [SerializeField] private Vector3 goldPosition;
    [SerializeField] private Vector2 targetScale;
    [SerializeField][Range(0,1)] private float fadeOut;
    [SerializeField][Range(0,2)] private float spawnDuration;
    [SerializeField] private Ease spawnEase;
    [SerializeField][Range(0,2)] private float showDuration;
    [SerializeField][Range(0,5)] private float moveDuration;
    [SerializeField] private Ease moveEase;
    
    private RectTransform _rectTransform;
    private Action _callback;

    public void SetCallBack(Action callback)
    {
        _callback = callback;
    }
    
    public void Play(Vector3 target)
    {
        if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = Vector3.one;
        transform.position = target;
        cGroup.alpha = 1f;

        _rectTransform.DOScale(targetScale, spawnDuration).SetEase(spawnEase).OnComplete(() =>
        {
            _rectTransform.DOScale(new Vector2(0.3f, 0.3f), moveDuration).SetDelay(showDuration);
            transform.DOMove(goldPosition, moveDuration).SetDelay(showDuration).SetEase(moveEase).OnComplete(() =>
            {
                _callback?.Invoke();
                PoolManager.Pools[UIManager.Instance.poolName].Despawn(this.transform, PoolManager.Pools[UIManager.Instance.poolName].transform);
            });
            cGroup.DOFade(fadeOut, moveDuration).SetDelay(showDuration);
        });
    }
}
