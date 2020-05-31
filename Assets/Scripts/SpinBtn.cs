using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpinBtn : MonoBehaviour
{
        [SerializeField] private Text label;
        private Button _btn;
        
        public void UpdateBtn(bool isSpin)
        {
                if (isSpin)
                {
                        GetButton().interactable = false;
                        label.color = Color.gray;
                        label.text = "Spin";
                }
                else
                {
                        GetButton().interactable = true;
                        label.color = Color.black;
                        label.text = "Spin";
                }
        }

        public Button GetButton()
        {
                if(_btn == null) _btn = GetComponent<Button>();
                return _btn;
        }
}