using Game.View;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameStateVisualizer))]
    public class GameStateVisualizerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameStateVisualizer gameStateVisualizer = (GameStateVisualizer) target;
            if (GUILayout.Button("Generate"))
            {
                gameStateVisualizer.Generate();
            }

            if (GUILayout.Button("Clear"))
            {
                gameStateVisualizer.Clear();
            }
        }
    }
}