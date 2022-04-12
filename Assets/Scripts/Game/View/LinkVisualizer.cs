using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    [RequireComponent(typeof(LineRenderer))]
    public class LinkVisualizer : MonoBehaviour
    {
        private Transform From { get; set; }
        private Transform To { get; set; }

        private LineRenderer _lineRenderer;

        public void Initialize(Transform from, Transform to)
        {
            From = from;
            To = to;
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            UpdatePositions();
        }

        private void Update()
        {
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            _lineRenderer.SetPositions(new[] {From.position, To.position});
        }
    }
}