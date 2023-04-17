using Unity.Mathematics;
using UnityEngine;
using YMSoft.Core.Pawns;

namespace YMSoft.Core
{
    public class BaseBullet : MonoBehaviour, IPawnStatePoint
    {
        public void Create(IPawnStatePoint data, Collider target)
            => Create(new BulletPointStruct(data), target);

        public void Create(BulletPointStruct data, Collider target)
        {
            HealthPoint = data.HealthPoint;
            AttackPoint = data.AttackPoint;
            AttackDelayPoint = data.AttackDelayPoint;
            ShootSpeedPoint = data.ShootSpeedPoint;
            DefensePoint = data.DefensePoint;
            RangePoint = data.RangePoint;

            _target = target;
            _targetState = target.GetComponent<BasePawnState>();
            _curve.Create(ShootSpeedPoint, transform);

            gameObject.SetActive(true);
        }

        [SerializeField] private BasePositionCurve _curve = null;
        private Collider _target = null;
        private BasePawnState _targetState = null;

        public float HealthPoint { get; private set; }
        public float AttackPoint { get; private set; }
        public float AttackDelayPoint { get; private set; }
        public float ShootSpeedPoint { get; private set; }
        public float DefensePoint { get; private set; }
        public float RangePoint { get; private set; }
        public PawnState State { get; set; } = PawnState.None;

        private void Update()
        {
            if (_target == null) Release();
            else
            {
                if (OnMovement(_target))
                    OnHit();
            }
        }

        protected virtual bool OnMovement(Collider target)
        {
            float3 dir = target.transform.position - transform.position;
            transform.rotation = quaternion.LookRotation(dir, new float3(0, 1, 0));

            return _curve.UpdateTracking(target);
        }

        protected virtual void OnHit()
        {
            _targetState.OnHit(AttackPoint);

            Release();
        }

        protected virtual void Release()
        {
            _target = null;
            _targetState = null;

            gameObject.SetActive(false);

            Destroy(gameObject);
        }

    }
}