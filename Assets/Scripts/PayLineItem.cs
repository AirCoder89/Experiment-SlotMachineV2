using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayLineItem : MonoBehaviour
{
    [SerializeField] private List<Image> slots;
    [SerializeField] private Text valueTxt;
    [SerializeField] private Color emptyColor;

    public void Initialize(Color color, string val, bool[,] paylines)
    {
        valueTxt.text = val;
        valueTxt.color = color;
        var width = paylines.GetLength(0);
        for (var y = 0; y < paylines.GetLength(1); y++)
        {
            for (var x = 0; x < width; x++)
            {
                slots[(y * width) + x].color = paylines[x, y] ? color : emptyColor;
            }
        }
    }
}
