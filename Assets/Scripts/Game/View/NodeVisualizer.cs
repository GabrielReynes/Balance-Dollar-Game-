using System;
using TMPro;
using UnityEngine;

namespace Game.View
{
    public class NodeVisualizer : MonoBehaviour
    {
        public TextMeshProUGUI textMeshPro;

        public void SetValue(int value)
        {
            textMeshPro.text = value.ToString();
        }

        public void UpdateValue(int shift)
        {
            int actual = Convert.ToInt32(textMeshPro.text);
            SetValue(actual + shift);
        }
    }
}