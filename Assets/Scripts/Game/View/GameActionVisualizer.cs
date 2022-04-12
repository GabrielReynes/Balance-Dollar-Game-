using UnityEngine;

namespace Game.View
{
    public class GameActionVisualizer : MonoBehaviour
    {
        private NodeVisualizer _from, _to;
        private float _speed, _lerpValue;

        public void Initialize(NodeVisualizer from, NodeVisualizer to, float speed)
        {
            _from = from;
            _to = to;
            _speed = speed;
            _lerpValue = 0;
            _from.UpdateValue(-1);
        }

        private void Update()
        {
            if (_lerpValue >= 1)
            {
                _to.UpdateValue(+1);
                DestroyImmediate(gameObject);
                return;
            }

            _lerpValue += Time.deltaTime * _speed;

            transform.position = Vector3.Lerp(_from.transform.position, _to.transform.position, _lerpValue);
        }
    }
}