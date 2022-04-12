using System.Collections.Generic;
using System.Linq;
using Game.Controller;
using Game.Model;

namespace LearningAgent
{
    public enum GameStateFeature
    {
        PositiveNodesRatio,
        MaximumValue,
        MinimumValue,
    }

    public static class GameStateFeatureExtractor
    {
        public static Dictionary<GameStateFeature, float> SimpleExtractor(GameState state, GameAction action)
        {
            Dictionary<GameStateFeature, float> featureVector = new Dictionary<GameStateFeature, float>();
            GameState nextState = state.NextState(action);

            float positiveNodesRation = (float) nextState.Nodes.Select(n => n.Value >= 0).Count() / state.Size;

            int[] nodeValues = nextState.Nodes.Select(n => n.Value).ToArray();

            featureVector[GameStateFeature.PositiveNodesRatio] = positiveNodesRation;
            featureVector[GameStateFeature.MaximumValue] = nodeValues.Max() / 10f;
            featureVector[GameStateFeature.MinimumValue] = nodeValues.Min() / 10f;

            featureVector.DivideAll(10f);
            return featureVector;
        }

        private static void DivideAll(this Dictionary<GameStateFeature, float> dictionary, float div)
        {
            GameStateFeature[] temp = dictionary.Keys.ToArray();
            foreach (GameStateFeature key in temp)
            {
                dictionary[key] /= div;
            }
        }
    }
}