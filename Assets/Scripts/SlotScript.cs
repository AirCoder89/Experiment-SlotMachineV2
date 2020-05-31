using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SlotScript : MonoBehaviour
{
   [SerializeField] private Image icon;
   public SlotType type;
   public int index;
   
   private Image _background;
   private ReelScript _parent;
   
   private RectTransform _rectTransform;

   public SlotResource currentResource;
   private Image Background
   {
      get
      {
         if (_background == null) _background = GetComponent<Image>();
         return _background;
      }
   }

   public void UpdateScale(float scale)
   {
      _rectTransform.localScale = new Vector2(scale,scale);
   }
   public void Initialize(ReelScript parentReel, int index)
   {
      this.index = index;
      this._parent = parentReel;
      _rectTransform = GetComponent<RectTransform>();
      GetRandom();
   }

   public void SetVisibility(bool status)
   {
      gameObject.SetActive(status);
   }

   public void SetType(SlotResource newType)
   {
      IconVisibility(true);
      this.currentResource = newType;
      this.Background.sprite = newType.background;
      this.icon.sprite = newType.icon;
      this.type = newType.type;
   }

   public void IconVisibility(bool status)
   {
      icon.gameObject.SetActive(status);
   }
   public void GetRandom()
   {
      var random = SlotMachine.Instance.settings.resourcesList[Random.Range(0, SlotMachine.Instance.settings.resourcesList.Count)];
      SetType(random);
   }
   
   
}
