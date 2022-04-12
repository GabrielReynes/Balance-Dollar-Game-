using System.Linq;
using Game.Model;
using LearningAgent;
using UnityEngine;

namespace Game.View
{
    public class GameAgentVisualizer : GameStateVisualizer
    {
        public GameAgent gameAgent;
        public GameActionVisualizer gameActionVisualizerPrefab;
        [Range(0f, 2f)] public float actionSpeed;

        public void Play()
        {
            if (Cleared) return;
            GameAction action = gameAgent.GetAction(GameState);
            GameState = GameState.NextState(action);
            Animate(action);
        }

        private void Animate(GameAction gameAction)
        {
            foreach (int neighbourIndex in GameState.NeighboursOf(gameAction.NodeIndex))
            {
                int startIndex, endIndex;
                if (gameAction.Action is Action.Take)
                {
                    startIndex = neighbourIndex;
                    endIndex = gameAction.NodeIndex;
                }
                else // gameAction.Action is Action.Give
                {
                    startIndex = gameAction.NodeIndex;
                    endIndex = neighbourIndex;
                }
                
                GameActionVisualizer gameActionVisualizer =
                    Instantiate(gameActionVisualizerPrefab, Nodes[startIndex].transform.position);
                gameActionVisualizer.Initialize(Nodes[startIndex], Nodes[endIndex], actionSpeed);
            }
        }
    }
}