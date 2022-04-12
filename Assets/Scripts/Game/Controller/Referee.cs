using System.Collections.Generic;
using System.Linq;
using Game.Model;
using GraphSearchUtilities.QLearning;
using LearningAgent;
using Action = Game.Model.Action;

namespace Game.Controller
{
    public static class Referee
    {
        public static QLearningProblem<GameState, GameAction> QLearningProblem =>
            new QLearningProblem<GameState, GameAction>(LegalActions, Score, NextState, EndState);

        public static TdLearningProblem<GameState, GameAction, GameStateFeature> TdLearningProblem(
            TdLearningProblem<GameState, GameAction, GameStateFeature>.FeatureExtractor featureExtractor) =>
            new TdLearningProblem<GameState, GameAction, GameStateFeature>(LegalActions, Score, NextState,
                EndState, featureExtractor);


        private static GameAction[] LegalActions(GameState gameState)
        {
            GameAction[] gameActions = new GameAction[2 * gameState.Size];
            for (int i = 0; i < gameState.Size; i++)
            {
                int index = i << 1; // 2 * i
                gameActions[index] = new GameAction(i, Action.Give);
                gameActions[index + 1] = new GameAction(i, Action.Take);
            }

            return gameActions;
        }

        private static GameState NextState(GameState gameState, GameAction gameAction)
        {
            return gameState.NextState(gameAction);
        }

        private static bool EndState(GameState gameState)
        {
            return gameState.Nodes.All(node => node.Value >= 0);
        }

        private static float Score(GameState gameState, GameAction gameAction)
        {
            return EndState(NextState(gameState, gameAction)) ? 10 : 0;
        }

        private static float GameStateScore(GameState gameState)
        {
            return gameState.Nodes.Where(n => n.Value < 0).Sum(n => n.Value);
        }
    }
}