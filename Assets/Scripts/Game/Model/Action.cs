using System;

namespace Game.Model
{
    public enum Action
    {
        None = 0,
        Give = 1,
        Take = -1
    }

    public readonly struct GameAction : IEquatable<GameAction>
    {
        public readonly int NodeIndex;
        public readonly Action Action;

        public GameAction(int nodeIndex, Action action)
        {
            Action = action;
            NodeIndex = nodeIndex;
        }

        public bool Equals(GameAction other)
        {
            return NodeIndex == other.NodeIndex && Action == other.Action;
        }

        public override bool Equals(object obj)
        {
            return obj is GameAction other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (NodeIndex * 397) ^ (int) Action;
            }
        }
    }
}