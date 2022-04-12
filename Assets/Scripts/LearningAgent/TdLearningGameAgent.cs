using Game.Controller;
using Game.Model;
using GraphSearchUtilities.QLearning;
using UnityEngine;

namespace LearningAgent
{
    [CreateAssetMenu(fileName = "TDLearningGameAgent", menuName = "DollarGame/TDLearningGameAgent")]
    public class TdLearningGameAgent : GameAgent
    {
        public override void Reset()
        {
            LearningAgent = new TdLearningAgent<GameState, GameAction, GameStateFeature>(epsilon, alpha, discount,
                Referee.TdLearningProblem(GameStateFeatureExtractor.SimpleExtractor));
        }
    }
}