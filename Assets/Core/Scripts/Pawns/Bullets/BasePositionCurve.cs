using Unity.Mathematics;
using UnityEngine;

namespace YMSoft.Core
{
    [System.Serializable]
    public class BasePositionCurve
    {
        public void Create(float playSpeed, Transform transform)
        {
            _currentTime = 0;
            
            _playSpeed = playSpeed;
            _transform = transform;
            _prevPosition = _transform.position;
        }

        [SerializeField] private AnimationCurve _xPosition = null;
        [SerializeField] private AnimationCurve _yPosition = null;
        [SerializeField] private AnimationCurve _zPosition = null;
        [SerializeField] private float _curveTime;
        private float _currentTime;
        private float _playSpeed;
        private Transform _transform;
        private float3 _prevPosition;

        public bool UpdateTracking(Transform target)
        {
            if (_currentTime > _curveTime)
                return true;

            _currentTime += _playSpeed * Time.deltaTime;

            float3 position = target.position;
            float3 vec = new(
                _xPosition.Evaluate(_currentTime) * position.x,
                _yPosition.Evaluate(_currentTime) * position.y,
                _zPosition.Evaluate(_currentTime) * position.z);

            _transform.position = math.lerp(_prevPosition, vec, _currentTime); 

            return false;
        }
    }
}