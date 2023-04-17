using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YMSoft.Core.Pawns;

namespace YMSoft.Core
{
    public class WaveController : MonoBehaviour
    {
        private void Awake()
        {
            _waypoints = _waypointTransforms.Select(x => x.transform.position).ToArray();

            IsStart = true;
        }

        [SerializeField] private BaseMinion _prefab;
        [SerializeField] private Transform[] _waypointTransforms;
        [SerializeField] private float _instanceDelayTime = 1;
        private Vector3[] _waypoints = null;
        private WaitTimer _waitTimer = new();
        private bool _isStart = false;

        public bool IsStart
        {
            get => _isStart;
            set
            {
                if (_isStart == value) return;

                if (value) OnStartWave();
                else OnStopWave();

                _isStart = value;
            }
        }

        private void Update()
        {
            if (!_isStart) return;

            if (!_waitTimer.IsWait())
            {
                BaseMinion m = Instantiate(_prefab, _waypoints[0], Quaternion.identity);
                m.Create(_waypoints);

                _waitTimer.WaitTime = _instanceDelayTime;
            }
        }

        private void OnStartWave()
        {
            _waitTimer.WaitTime = _instanceDelayTime;
            _waitTimer.IsPause = false;
        }

        private void OnStopWave()
        {
            _waitTimer.IsPause = true;
        }

    }
}