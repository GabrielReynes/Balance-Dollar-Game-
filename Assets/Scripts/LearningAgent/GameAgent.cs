using Game.Controller;
using Game.Model;
using GraphSearchUtilities.QLearning;
using UnityEngine;

namespace LearningAgent
{
    public abstract class GameAgent : ScriptableObject
    {
        public GameStateGenerator gameStateGenerator;
        [Range(0f, 1f)] public float epsilon, alpha, discount;
        [Range(1, 10_000)] public int trainingRepetition;
        [Range(1, 10_000)] public int maximumTrainingIteration;

        protected LearningAgent<GameState, GameAction> LearningAgent;

        private void OnEnable()
        {
            Reset();
        }
        
        public GameAction GetAction(GameState state)
        {
            GameAction action = LearningAgent.GetAction(state);
            // LearningAgent.UpdateValue(state, action);
            return action;
        }
        
        public float Train()
        {
            float mean = 0;
            for (int i = 0; i < trainingRepetition; i++)
            {
                GameState gameState = gameStateGenerator.Generate();
                mean += LearningAgent.Train(gameState, maximumTrainingIteration);
            }

            return mean / trainingRepetition;
        }
        
        public virtual void Update()
        {
            LearningAgent.Epsilon = epsilon;
            LearningAgent.Alpha = alpha;
            LearningAgent.Discount = discount;
        }

        public abstract void Reset();
    }
}