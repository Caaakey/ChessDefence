using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BaseMinion : BasePawn
    {
        public void Create(Vector3[] waypoints)
        {
            _movement.Create(waypoints, _speed);
        }

        [SerializeField] private BaseMinionMovement _movement;
        [SerializeField] private float _speed;

    }

}
