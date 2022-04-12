using LearningAgent;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameAgent), true)]
    public class GameAgentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameAgent gameAgent = (GameAgent) target;
            if (GUILayout.Button("Set"))
            {
                gameAgent.Update();
                Debug.Log("The Agent has been updated.");
            }

            if (GUILayout.Button("Reset"))
            {
                gameAgent.Reset();
                Debug.Log("The Agent has been reset.");
            }

            if (GUILayout.Button("Train"))
            {
                float meanRound = gameAgent.Train();
                Debug.Log($"Mean number of round : {meanRound}");
            }
        }
    }
}