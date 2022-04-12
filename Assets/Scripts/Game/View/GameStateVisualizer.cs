using System.Linq;
using Game.Controller;
using Game.Model;
using UnityEngine;

namespace Game.View
{
    public class GameStateVisualizer : MonoBehaviour
    {
        public NodeVisualizer nodePrefab;
        public LinkVisualizer linkPrefab;
        public GameStateGenerator gameStateGenerator;

        [Range(1f, 10f)] public float targetBoundSize;
        [Range(0f, 10f)] public float repulsionConstant, springConstant;
        public float minimumApplyingForce;
        
        protected GameState GameState;
        protected bool Cleared;
        protected NodeVisualizer[] Nodes;
        
        private bool _applyForces;
        private float _linkSize;

        private void Start()
        {
            Generate();
        }

        private void Update()
        {
            if (!_applyForces || Cleared) return;
            Vector3[] forces = CalculateForces();
            ApplyForces(forces);
            Bounds bounds = CalculateBounds();
            AdjustLinkSize(bounds.size);
            transform.position = -bounds.center;
            _applyForces = forces.Any(v => v.sqrMagnitude > minimumApplyingForce * minimumApplyingForce);
        }

        private void AdjustLinkSize(Vector3 size)
        {
            const float threshold = 1f;
            float maxSize = Mathf.Max(size.x, size.y);
            float diff = targetBoundSize - maxSize;
            if (Mathf.Abs(diff) < threshold) return;
            _linkSize = Mathf.Max(1e-2f, _linkSize + Time.deltaTime * diff);
        }

        private Bounds CalculateBounds()
        {
            Bounds bounds = new Bounds();
            foreach (NodeVisualizer node in Nodes) bounds.Encapsulate(node.transform.localPosition);
            return bounds;
        }

        public void Generate()
        {
            GameState = gameStateGenerator.Generate();
            Nodes = new NodeVisualizer[GameState.Size];
            _applyForces = true;
            _linkSize = 1;

            Clear();
            GenerateNodes();
            GenerateLinks();
            ApplyValues();

            Cleared = false;
        }

        protected void ApplyValues()
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].SetValue(GameState.Nodes[i].Value);
            }
        }

        private void GenerateLinks()
        {
            foreach (Link link in GameState.Links)
            {
                LinkVisualizer linkVisualizer = Instantiate(linkPrefab, transform);
                linkVisualizer.Initialize(Nodes[link.From].transform, Nodes[link.To].transform);
            }
        }

        private void GenerateNodes()
        {
            int arraySize = Mathf.CeilToInt(Mathf.Sqrt(GameState.Size));
            for (int y = 0; y < arraySize; y++)
            {
                for (int x = 0; x < arraySize; x++)
                {
                    int index = y * arraySize + x;
                    if (index >= GameState.Size) break;
                    Vector3 relativePos = _linkSize * (x * Vector3.right + y * Vector3.up);
                    Vector3 pos = transform.position + relativePos;
                    Nodes[index] = Instantiate(nodePrefab, pos);
                }
            }
        }

        private Vector3 RepulsionVector(Vector3 directionVector)
        {
            // FruchtermanReingold algorithm
            // return linkSize / directionVector.magnitude * directionVector;
            
            //Fades algorithm : 
            return repulsionConstant / directionVector.sqrMagnitude * directionVector; 
        }

        private Vector3 SpringVector(Vector3 directionVector)
        {
            // FruchtermanReingold algorithm : 
            // return directionVector.magnitude / linkSize * directionVector;

            // Fades algorithm (we should subtract the Repulsion vector here but didn't to avoid densely packed group) : 
            return springConstant * Mathf.Log(directionVector.magnitude / _linkSize) * directionVector;
        }

        private Vector3[] CalculateForces()
        {
            Vector3[] forces = new Vector3[Nodes.Length];
            for (int i = 0; i < Nodes.Length; i++)
            {
                for (int j = i + 1; j < Nodes.Length; j++)
                {
                    Vector3 diff = DirectionVector(i, j);
                    Vector3 repulsion = RepulsionVector(diff);
                    forces[i] -= repulsion;
                    forces[j] += repulsion;
                }
            }
            foreach (Link link in GameState.Links)
            {
                Vector3 diff = DirectionVector(link.From, link.To);
                Vector3 attraction = SpringVector(diff);
                forces[link.From] += attraction;
                forces[link.To] -= attraction;
            }

            return forces;
        }

        private void ApplyForces(Vector3[] forces)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].transform.Translate(Time.deltaTime * forces[i]);
            }
        }

        public void Clear()
        {
            Cleared = true;
            Transform[] temp = transform.Cast<Transform>().ToArray();
            foreach(Transform child in temp)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        private Vector3 DirectionVector(int @from, int to)
        {
            return Nodes[to].transform.position - Nodes[@from].transform.position;
        }

        protected T Instantiate<T>(T prefab, Vector3 position) where T : Object
        {
            return Instantiate(prefab, position, Quaternion.identity, transform);
        } 
    }
}