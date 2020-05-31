using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReelScript : MonoBehaviour
{
    public delegate void ReelEvents(int index);
    public static event ReelEvents OnSpinStart;
    public static event ReelEvents OnSpinComplete;

    [SerializeField] private VerticalLayoutGroup verticalLayout;
    
    public List<SlotScript> slots;
    private int _index;
    private bool _inSpin;
    private bool _inClamp;
    private float _yOffset;
    private RectTransform _rectTransform;
    private SpinSettings _spinSettings;
    private Vector2 _targetPos;
    private float _currentSpeed;
    private bool _increaseSpeed;
    private float _delayAmount;
    private float _timeCounter;
    private bool _clampedDown;
    private float _acceleration;
    public void Initialize(int index)
    {
        _rectTransform = GetComponent<RectTransform>();
        this._inSpin = false;
        this._index = index;
        for (var i = 0; i < this.slots.Count; i++)
        {
            slots[i].Initialize(this,i);
        }
        _spinSettings = SlotMachine.Instance.settings.spinSettings;
    }

    public void ResetShape()
    {
        
    }

    public void UpdateVerticalLayout(float spacing)
    {
        verticalLayout.spacing = spacing;
    }

    public void UpdateSlotScale(float scale)
    {
        foreach (var slot in slots)
        {
            slot.UpdateScale(scale);
        }
    }

    public List<SlotScript> GetCoins()
    {
        return this.slots.Where(s => s.type == SlotType.Coin && s.index > 0).ToList();
    }
    
    public SlotType GetSlotType(int index)
    {
        return slots[index + 1].type;
        return _clampedDown ? slots[index].type : slots[index + 1].type;
    }
    public RectTransform GetSlotTransform(int index)
    {
        return slots[index + 1].gameObject.GetComponent<RectTransform>();
    }
    
    public void Spin(float delay, float acceleration, float speed)
    {
        if (_spinSettings.endSpin == SpinType.All)
        {
            _delayAmount = 0f;
        }
        else
        {
            _delayAmount = _index * delay;
        }

        _acceleration = acceleration <= 0 ? GameExtension.GetRandomValue(_spinSettings.acceleration) : acceleration;
        _timeCounter = 0f;
        _currentSpeed = speed <= 0f ? GameExtension.GetRandomValue(_spinSettings.startSpeed) : speed;
        _yOffset = 0f;
        _increaseSpeed = true;
        _inClamp = false;
        _inSpin = true;
        OnSpinStart?.Invoke(this._index);
    }

    public void Stop()
    {
        _inClamp = true;
        _inSpin = false;
        var xPos = _rectTransform.anchoredPosition.x;
        var topPos = new Vector2(xPos, _spinSettings.topBoundary);
        var bottomPos = new Vector2(xPos, _spinSettings.bottomBoundary);
        if (Vector3.Distance(_rectTransform.anchoredPosition, topPos) <
            Vector3.Distance(_rectTransform.anchoredPosition, bottomPos))
        {
            //clamp to top
            _targetPos = topPos;
            _clampedDown = false;
        }
        else
        {
            //clamp to bottom
            _targetPos = bottomPos;
            _clampedDown = true;
        }
    }

    private void OnClampComplete()
    {
        if (_clampedDown)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _spinSettings.topBoundary);
            //shift types
            for (var i = slots.Count-1; i > 0 ; i--)
            {
                var res = slots[i-1].currentResource;
                slots[i].SetType(res);
            }
        }
        OnSpinComplete?.Invoke(this._index);
    }

    private void Update()
    {
        if (_inClamp)
        {
            if(_currentSpeed > (_spinSettings.speedRange.x /2)) _currentSpeed -= _acceleration;
            _rectTransform.anchoredPosition = Vector3.LerpUnclamped(_rectTransform.anchoredPosition, _targetPos,
                _currentSpeed * Time.deltaTime);

            if (Vector3.Distance(_rectTransform.anchoredPosition, _targetPos) < _spinSettings.minClamp)
            {
                _rectTransform.anchoredPosition = _targetPos;
                _inClamp = false;
                OnClampComplete();
            }
        }
        
        if(!_inSpin) return;

        if (_increaseSpeed)
        {
            if (_currentSpeed < _spinSettings.speedRange.y)
            {
                _currentSpeed += _acceleration;
            }
            else
            {
                if (_timeCounter >= _delayAmount)
                {
                    _increaseSpeed = false;
                }
                else
                {
                    _timeCounter += Time.deltaTime;
                }
            }
        }
        else
        {
            if (_currentSpeed > _spinSettings.speedRange.x)
            {
                _currentSpeed -= _acceleration;
            }
            else
            {
                var distance = (Vector3.Distance(_rectTransform.anchoredPosition, _targetPos)/50)/2;
                if((_currentSpeed - distance) > 1) _currentSpeed = _currentSpeed - distance;
                Stop();
            }
        }
        
        _rectTransform.Translate(Vector3.down * (_currentSpeed * Time.deltaTime));
        var currentPos = _rectTransform.anchoredPosition;
        if (currentPos.y <= _spinSettings.bottomBoundary)
        {
            //reset pos
            _yOffset = _spinSettings.bottomBoundary - currentPos.y;
            _rectTransform.anchoredPosition = new Vector2(currentPos.x, _spinSettings.topBoundary + _yOffset);

            //shift types
            for (var i = slots.Count-1; i > 0 ; i--)
            {
                var res = slots[i-1].currentResource;
                slots[i].SetType(res);
            }
            
            //generate new
            slots[0].GetRandom();
        }
    }
}
