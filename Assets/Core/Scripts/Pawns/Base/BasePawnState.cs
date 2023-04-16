using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BasePawnState : MonoBehaviour, IPawnStatePoint
    {
        [SerializeField] private float _healthPoint;
        [SerializeField] private float _attackPoint;
        [SerializeField] private float _attackaDelayPoint;
        [SerializeField] private float _shootSpeedPoint;
        [SerializeField] private float _defensePoint;
        [SerializeField][Range(0, 12)] private float _rangePoint;

        public float HealthPoint => _healthPoint;
        public float AttackPoint => _attackPoint;
        public float AttackDelayPoint => _attackaDelayPoint;
        public float ShootSpeedPoint => _shootSpeedPoint;
        public float DefensePoint => _defensePoint;
        public float RangePoint => _rangePoint;
        public PawnState State { get; set; } = PawnState.None;


        public virtual bool OnHit(float damage)
        {

            return false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            //if (!UnityEditor.EditorApplication.isPlaying) return;
            if (RangePoint <= 0f) return;

            Color stateColor = Color.green;
            Vector3 position = transform.position;
            position.y += 0.05f;

            Matrix4x4 mat = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, 0.01f, 1));

            using (new GizmosExtension.Scope(stateColor, mat))
            {
                Gizmos.DrawWireSphere(Vector3.zero, RangePoint);
            }

        }
#endif
    }

}