#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.Model
{
    public readonly struct GameState : IEquatable<GameState>
    {
        public readonly Node[] Nodes;
        public readonly Link[] Links;
        public int Size => Nodes.Length;

        public GameState(Node[] nodes, Link[] links)
        {
            Nodes = nodes;
            Links = links;
        }

        public GameState NextState(GameAction gameAction)
        {
            Node[] copy = (Node[]) Nodes.Clone();
            int actionShift = (int) gameAction.Action;

            int[] neighboursIndexes = NeighboursOf(gameAction.NodeIndex).ToArray();
            
            foreach (int neighbourIndex in neighboursIndexes)
            {
                copy[neighbourIndex] += actionShift;
            }

            copy[gameAction.NodeIndex] += actionShift * -neighboursIndexes.Length;
            return new GameState(copy, Links);
        }

        public IEnumerable<Link> LinksWith(int nodeIndex)
        {
            return Links.Where(link => link.From == nodeIndex || link.To == nodeIndex);
        }

        public IEnumerable<int> NeighboursOf(int nodeIndex)
        {
            return LinksWith(nodeIndex).Select(link => link.NeighbourIndex(nodeIndex));
        }

        public bool Equals(GameState other)
        {
            return Nodes.SequenceEqual(other.Nodes) && Links.SequenceEqual(other.Links);
        }

        public override bool Equals(object? obj)
        {
            return obj is GameState other && Equals(other);
        }

        public override int GetHashCode()
        {
            int nodeHash = ((IStructuralEquatable) Nodes).GetHashCode(EqualityComparer<Node>.Default);
            int linkHash = ((IStructuralEquatable) Links).GetHashCode(EqualityComparer<Link>.Default);
            unchecked
            {
                return (nodeHash * 397) ^ linkHash;
            }
        }

        public static bool operator ==(GameState g1, GameState g2)
        {
            return g1.Equals(g2);
        }

        public static bool operator !=(GameState g1, GameState g2)
        {
            return !g1.Equals(g2);
        }
    }
}