using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public string poolName;
    
    [SerializeField] private Text goldTxt;
    [SerializeField] private SpinBtn spinBtn;
    [SerializeField] private Button payLineBtn;
    public PayLinePopUp paylinePopup;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    private void Start()
    {
        spinBtn.GetButton().onClick.AddListener(OnClickSpin);
        payLineBtn.onClick.AddListener(OpenPaylinesPopUp);
    }

    public void UpdateGold(uint amount)
    {
        goldTxt.text = GameExtension.ConvertGold(amount);
    }
    
    private void OpenPaylinesPopUp()
    {
        this.paylinePopup.OpenPanel();
    }

    private void OnClickSpin()
    {
        SlotMachine.Instance.Spin();
    }

    public void UpdateSpinBtn(bool isSpin)
    {
        spinBtn.UpdateBtn(isSpin);
    }
    
}
