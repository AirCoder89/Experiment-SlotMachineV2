using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[System.Serializable]
public struct PayLines
{
   public delegate void PayLinesEvents(bool[,] payline, Color color);
   public static event PayLinesEvents DrawPaylineEvent;
   
   [Button("Draw Payline", ButtonSizes.Medium)]
   private void DrawPayline()
   {
      DrawPaylineEvent?.Invoke(payline, color);
   }
   
   [Button("Initialize", ButtonSizes.Medium)]
   private void Initialize()
   {
      payline = new bool[5,3];
   }

   public Color color;
   public int points;
   [ShowInInspector]
   [TableMatrix(HorizontalTitle = "PayLine 5 x 3",DrawElementMethod = "DrawCell",ResizableColumns = false)] public bool[,] payline;

   private string saved;
   
   private static bool DrawCell(Rect rect, bool value)
   {
      if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
      {
         value = !value;
         GUI.changed = true;
         Event.current.Use();
      }
      UnityEditor.EditorGUI.DrawRect(rect.Padding(1),value ? new Color(0.1f,0.8f,0.2f) : new Color(0,0,0,0.5f));
      return value;
   }

   [Button("Clear", ButtonSizes.Medium)]
   private void Clear()
   {
      payline = new bool[5,3];
   }

   public void SetPayLine(bool[,] m)
   {
      payline = m;
   }
   
   public string ToString()
   {
      saved = "";
      for (var y = 0; y < 3; y++)
      {
         for (var x = 0; x < 5; x++)
         {
            var p = payline[x, y] ? 1 : 0;
            saved += p.ToString();
         }
         if(y < 2) saved += "#";
      }

      saved += "&" + points;
      return saved;
   }

   public string GetValue()
   {
      var value = "";
      for (var x = 0; x < 5; x++)
      {
         var intValue = -1;
         for (var y = 0; y < 3; y++)
         {
            if (!payline[x, y]) continue;
            intValue = y + 1;
            break;
         }
         
         value += intValue.ToString();
      }

      return value;
   }
}
