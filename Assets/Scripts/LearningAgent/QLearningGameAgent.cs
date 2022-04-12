using Game.Controller;
using Game.Model;
using GraphSearchUtilities.QLearning;
using UnityEngine;

namespace LearningAgent
{
    [CreateAssetMenu(fileName = "QLearningGameAgent", menuName = "DollarGame/QLearningGameAgent")]
    public class QLearningGameAgent : GameAgent
    {
        public override void Reset()
        {
            LearningAgent = 
                new QLearningAgent<GameState, GameAction>(epsilon, alpha, discount, Referee.QLearningProblem);
        }
    }
}