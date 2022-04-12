using System;

namespace Game.Model
{
    public readonly struct Node : IEquatable<Node>
    {
        public readonly int Value;

        public Node(int value)
        {
            Value = value;
        }

        public static Node operator +(Node node, int add)
        {
            return new Node(node.Value + add);
        }

        public bool Equals(Node other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Node other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}