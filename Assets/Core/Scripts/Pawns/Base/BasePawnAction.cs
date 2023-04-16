using UnityEngine;

namespace YMSoft.Core.Pawns
{
    [System.Flags]
    public enum TargetMaskFlags : int
    {
        None = 0,
        Pawn = 1 << 6,
        Enemy = 1 << 7
    }

    public abstract class BasePawnAction : MonoBehaviour, IPawnAction
    {
        [SerializeField] protected BasePawnAnimation PawnAnimation;
        [SerializeField] protected BasePawnState PawnState;
        [SerializeField] protected TargetMaskFlags TargetMaskLayer;
        private readonly WaitTimer _waitTimer = new();

        protected void Update()
        {
            if (_waitTimer.IsWait()) return;
            else PawnState.State = Pawns.PawnState.Wait;

            if (PawnState.State == Pawns.PawnState.Wait)
            {
                int count = DetectTargetSphere(this);
                if (count != 0)
                {
                    OnAttack(s_SharedDetectColliders, count);

                    PawnState.State = Pawns.PawnState.Attack;
                    _waitTimer.WaitTime = PawnState.AttackDelayPoint;
                }
            }
        }

        public bool OnHit(float damage)
        {
            PawnState.OnHit(damage);
            PawnAnimation.OnHit(damage);

            return true;
        }

        protected abstract void OnAttack(Collider[] targets, int count);

        private const int MAX_BUFFER = 12;
        private readonly static Collider[] s_SharedDetectColliders = new Collider[MAX_BUFFER];

        private static int DetectTargetSphere(BasePawnAction component)
        {
            int count = Physics.OverlapSphereNonAlloc(
                component.transform.position, component.PawnState.RangePoint,
                s_SharedDetectColliders, (int)component.TargetMaskLayer);

            return count;
        }

    }
}