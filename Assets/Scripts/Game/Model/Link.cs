using System;

namespace Game.Model
{
    public readonly struct Link : IEquatable<Link>
    {
        public readonly int From, To;

        public Link(int @from, int to)
        {
            From = @from;
            To = to;
        }

        public int NeighbourIndex(int nodeIndex)
        {
            return From + To - nodeIndex;
        }

        public bool Equals(Link other)
        {
            return From == other.From && To == other.To;
        }

        public override bool Equals(object obj)
        {
            return obj is Link other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From * 397) ^ To;
            }
        }
    }
}