using Game.View;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameAgentVisualizer))]
    public class GameAgentVisualizerEditor : GameStateVisualizerEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameAgentVisualizer gameAgentVisualizer = (GameAgentVisualizer) target;
            if (GUILayout.Button("Play"))
            {
                gameAgentVisualizer.Play();
            }
        }
    }
}