using Unity.Mathematics;
using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BaseMinionMovement : MonoBehaviour
    {
        public void Create(Vector3[] waypoints, float speed)
        {
            _waypoints = waypoints;
            _moveSpeed = speed;
            _moveIndex = 0;

            SetNextWaypoint();

            enabled = true;
        }

        private Vector3[] _waypoints = null;
        private float3 _nextPoint = float3.zero;
        private int _moveIndex = 0;
        private float _moveSpeed = 0;

        private void Update()
        {
            float z = _moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, 0, z));
            
            if (math.distance(_nextPoint, transform.position) <= z * 2f)
            {
                transform.position = _nextPoint;

                if (SetNextWaypoint())
                    OnArriveGoal();       
            }
        }

        private bool SetNextWaypoint()
        {
            if (_moveIndex == _waypoints.Length)
                return true;

            _nextPoint = _waypoints[_moveIndex++];

            float3 dir = _nextPoint - (float3)transform.position;
            transform.rotation = quaternion.LookRotation(dir, new(0, 1, 0));

            return false;
        }

        private void OnArriveGoal()
        {
            enabled = false;

            Destroy(gameObject);
        }
    }
}