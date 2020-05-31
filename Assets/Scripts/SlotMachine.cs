using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SlotMachine : MonoBehaviour
{
    public static SlotMachine Instance;

    public uint goldAmount = 0;
    
    [OnValueChanged("UpdateSettings")]public GameSettings settings;
    public List<ReelScript> reels;
    [SerializeField] private HorizontalLayoutGroup horizontalLayout;
    [SerializeField] private UILineConnector payLineRenderer;
    [SerializeField] private string coinVfxName;
    [SerializeField] private RectTransform vfxHolder;
    [ShowInInspector][Sirenix.OdinInspector.ReadOnly] public SlotType[,] resultMatrix;
    
    private bool _inSpin;
    private float _timeCounter;
    private int _reelIndex;
    private bool _isSingleSpin;
    private float _delayAmongReel;
    private float _acceleration;
    private float _speed;
    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    private void Start()
    {
        PayLines.DrawPaylineEvent += DrawPayline;
        GameSettings.OnSavePaylines += SavePaylines;
        GameSettings.UpdateLayout += UpdateLayout;
        GameSettings.UpdateScale += UpdateScale;
        ReelScript.OnSpinComplete += OnReelSpinComplete;
        _inSpin = false;
        UpdateSettings();
        UIManager.Instance.UpdateGold(goldAmount);
    }
    private void OnDestroy()
    {
        PayLines.DrawPaylineEvent -= DrawPayline;
        GameSettings.OnSavePaylines -= SavePaylines;
        GameSettings.UpdateLayout -= UpdateLayout;
        GameSettings.UpdateLayout -= UpdateLayout;
        ReelScript.OnSpinComplete -= OnReelSpinComplete;
    }
    
    private void DrawPayline(bool[,] payline, Color color)
    {
        if(payline == null) return;
        var list = new List<RectTransform>();
        for (var x = 0; x < payline.GetLength(0); x++)
        {
            for (var y = 0; y < payline.GetLength(1); y++)
            {
                if (payline[x, y])
                {
                    list.Add(reels[x].GetSlotTransform(y));
                }
            }
        }
        payLineRenderer.SetPayline(list, color);
    }
    
    private void SavePaylines()
    {
        UIManager.Instance.paylinePopup.Initialize(settings.paylines);
    }

    private void UpdateSettings()
    {
        for (var i = 0; i < this.reels.Count; i++)
        {
            this.reels[i].Initialize(i);
        }
        settings.Initialize();
        UpdateScale();
        UpdateLayout();
        UIManager.Instance.paylinePopup.Initialize(settings.paylines);
    }
    private void UpdateScale()
    {
        foreach (var reel in reels)
        {
            reel.UpdateSlotScale(settings.slotScale);
        }
    }

    private void UpdateLayout()
    {
        var lastStatus = horizontalLayout.enabled;
        horizontalLayout.enabled = true;
        horizontalLayout.spacing = settings.horizontalLayout;
       
        foreach (var reel in reels)
        {
            reel.UpdateVerticalLayout(settings.verticalLayout);
        }
        horizontalLayout.enabled = lastStatus;
    }

    private void OnReelSpinComplete(int index)
    {
        if (settings.spinSettings.endSpin == SpinType.Single)
        {
            if (index == reels.Count - 1)
            {
                //_inSpin = false;
                PlaySlotVFX();
                UpdateMatrix();
            }
        }
        else
        {
            //_inSpin = false;
            PlaySlotVFX();
            UpdateMatrix();
        }
       // UIManager.Instance.UpdateSpinBtn(_inSpin);
    }

   
    private void PlaySlotVFX()
    {
        CoinParticle lastVfx = null;
        foreach (var reel in reels)
        {
            var rList = reel.GetCoins();
            foreach (var c in rList)
            {
                c.IconVisibility(false);
                var coinVfx = PoolManager.Pools[UIManager.Instance.poolName].Spawn(coinVfxName, vfxHolder).gameObject
                    .GetComponent<CoinParticle>();
                coinVfx.SetCallBack(() =>
                {
                    goldAmount += 100;
                    UIManager.Instance.UpdateGold(goldAmount);
                });
                lastVfx = coinVfx;
                coinVfx.Play(c.transform.position);
            }
        }
        if(lastVfx != null)  lastVfx.SetCallBack(() =>
        {
            goldAmount += 100;
            UIManager.Instance.UpdateGold(goldAmount);
            _inSpin = false;
            UIManager.Instance.UpdateSpinBtn(_inSpin);
        });
        else
        {
            _inSpin = false;
            UIManager.Instance.UpdateSpinBtn(_inSpin);
        }
    }
    
    
    private void UpdateMatrix()
    {
        horizontalLayout.enabled = true;
        this.resultMatrix = new SlotType[reels.Count,3];
        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < reels.Count; x++)
            {
                this.resultMatrix[x, y] = reels[x].GetSlotType(y);
            }
        }
    }
    
    public void Spin()
    {
        if (!_inSpin)
        {
            horizontalLayout.enabled = false;
            _inSpin = true;
            _acceleration = settings.spinSettings.useSameAcceleration
                ? GameExtension.GetRandomValue(settings.spinSettings.acceleration)
                : 0f;
            
            _speed = settings.spinSettings.useSameSpeed
                ? GameExtension.GetRandomValue(settings.spinSettings.startSpeed)
                : 0f;
            
            _delayAmongReel = GameExtension.GetRandomValue(settings.spinSettings.delayAmongReels);
            
            if (settings.spinSettings.startSpin == SpinType.All)
            {
                foreach (var reel in reels)
                {
                    reel.ResetShape();
                    reel.Spin(_delayAmongReel,_acceleration,_speed);
                }
            }
            else if (settings.spinSettings.startSpin == SpinType.Single)
            {
                //start spin the first reel
                reels[0].ResetShape();
                reels[0].Spin(_delayAmongReel,_acceleration,_speed);
                
                //init delay variables
                
                _timeCounter = 0;
                _reelIndex = 1;
                _isSingleSpin = true;
            }
        }
        UIManager.Instance.UpdateSpinBtn(_inSpin);
    }

    private void Update()
    {
        if(!_isSingleSpin) return;

        if (_timeCounter >= _delayAmongReel)
        {
            if (_reelIndex >= reels.Count)
            {
                _isSingleSpin = false;
                return;
            }
            _timeCounter = 0;
            reels[_reelIndex].ResetShape();
            reels[_reelIndex].Spin(_delayAmongReel,_acceleration,_speed);
            _reelIndex++;
        }
        else
        {
            _timeCounter += Time.deltaTime;
        }
    }
}
