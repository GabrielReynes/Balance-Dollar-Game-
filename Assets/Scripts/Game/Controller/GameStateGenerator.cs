using System;
using System.Collections.Generic;
using Game.Model;
using UnityEngine;
using Random = System.Random;

namespace Game.Controller
{
    [CreateAssetMenu(fileName = "GameStateGenerator", menuName = "DollarGame/GameStateGenerator")]
    public class GameStateGenerator : ScriptableObject
    {
        public int seed;
        [Range(0, 10)] public int size, valueSpread;
        [Range(0f, 1f)] public float linkProbability;

        private Random _random;

        public GameState Generate()
        {
            _random = seed < 0 ? new Random() : new Random(seed);
            Link[] links = GenerateLinks();
            int genus = links.Length - size + 1;
            Node[] nodes = GenerateValues(genus);

            return new GameState(nodes, links);
        }

        private Node[] GenerateValues(int genus)
        {
            int meanDistribution = Mathf.CeilToInt((float) genus / size);
            int shift = 0;
            Node[] nodes = new Node[size];
            for (int i = 0; i < size; i++)
            {
                int value = _random.Next(-valueSpread, meanDistribution + valueSpread) + shift;
                shift += meanDistribution - value;
                nodes[i] += value;
            }

            if (shift > 0) nodes[_random.Next(size)] += shift;
            return nodes;
        }

        private Link[] GenerateLinks()
        {
            List<Link> links = new List<Link>();
            bool maxNumberOfLinks = false; // To be planar
            for (int i = 0; i < size - 1; i++)
            {
                bool createdOne = false;
                for (int j = i + 1; j < size; j++)
                {
                    if (maxNumberOfLinks) break;
                    if (_random.NextDouble() >= linkProbability) continue;
                    links.Add(new Link(i, j));
                    createdOne = true;
                    maxNumberOfLinks = links.Count - i == 2 * size - 4;
                }

                if (!createdOne) links.Add(new Link(i, _random.Next(i + 1, size)));
            }

            return links.ToArray();
        }
    }
}