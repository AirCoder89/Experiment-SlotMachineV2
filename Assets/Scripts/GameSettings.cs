using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public enum SlotType
{
    Red, Green, Blue, Yellow, Energy, Heart, Diamond, Coin
}

public enum SpinType
{
    Single, All
}
[System.Serializable]
public struct PayLineData
{
    public string storedData;
    public string payLineValue;
    public Color payLineColor;
}

[System.Serializable]
public struct SlotResource
{
    public SlotType type;
    [PreviewField] public Sprite background;
    [PreviewField] public Sprite icon;
}

[System.Serializable]
public class SpinSettings
{
    [Title("Reel Spinning")] 
    [EnumToggleButtons] public SpinType startSpin;
    [EnumToggleButtons] public SpinType endSpin;
    [MinMaxSlider(0f,2f,true)]public Vector2 delayAmongReels;
    public bool useSameAcceleration;
    public bool useSameSpeed;
    
    [Title("Boundaries")] 
    [Range(0,0.5f)] public float minClamp;
    public float topBoundary;
    public float bottomBoundary;
    [Title("Spinning")]
    [MinMaxSlider(10f,500f,true)]public Vector2 startSpeed;
    [MinMaxSlider(0.1f,5f,true)]public Vector2 acceleration;
    [MinMaxSlider(0,1000,true)]public Vector2 speedRange;
}

[CreateAssetMenu(menuName = "Settings", fileName = "New_Settings")]
public class GameSettings : ScriptableObject
{
    public delegate void SettingsEvents();
    public static event SettingsEvents UpdateLayout;
    public static event SettingsEvents UpdateScale;
    public static event SettingsEvents OnSavePaylines;
    
    [TabGroup("Resources")][TableList] public List<SlotResource> resourcesList;

    [TabGroup("Spin Settings")] [HideLabel] 
    public SpinSettings spinSettings;

    [TabGroup("Paylines")][HideLabel] public List<PayLines> paylines;
    

    [TabGroup("Layout")][LabelText("Horizontal")][Range(1,300)][OnValueChanged("OnUpdateLayout")] public float horizontalLayout;
    [TabGroup("Layout")][LabelText("Vertical")][Range(1,60)][OnValueChanged("OnUpdateLayout")] public float verticalLayout;
    [TabGroup("Layout")][LabelText("Slot Scale")][Range(0.5f,2f)][OnValueChanged("OnUpdateScale")] public float slotScale = 1f;


    [HideInInspector] public List<PayLineData> payLinesData;
    public void Initialize()
    {
        LoadPayLines();
    }
    
    private void OnUpdateLayout()
    {
        UpdateLayout?.Invoke();
    }
    
    private void OnUpdateScale()
    {
        UpdateScale?.Invoke();
    }

    [TabGroup("Paylines")][Button("Save", ButtonSizes.Medium)]
    private void SavePaylines()
    {
        payLinesData = new List<PayLineData>();
        foreach (var payLine in paylines)
        {
            payLinesData.Add(new PayLineData()
            {
                storedData = payLine.ToString(),
                payLineValue = payLine.GetValue(),
                payLineColor = payLine.color
            });
        }
        
        OnSavePaylines?.Invoke();
    }
    
    private void LoadPayLines()
    {
        var y = 0;
        paylines = new List<PayLines>();
        foreach (var payLine in payLinesData)
        {
            var pre = payLine.storedData.Split(char.Parse("&"));
            var points = 0;
            int.TryParse(pre[1], out points);
            var m = new bool[5, 3];
            var rows = pre[0].Split(char.Parse("#"));
            foreach (var row in rows)
            {
                if(string.IsNullOrEmpty(row)) continue;
                for (var x = 0; x < row.Length; x++)
                {
                    var ch = row.Substring(x, 1);
                    m[x, y] = ch == "1";
                }

                y++;
            }

            y = 0;
            var pl = new PayLines();
            pl.color = payLine.payLineColor;
            pl.points = points;
            pl.SetPayLine(m);
            paylines.Add(pl);
            
        }
    }
}
