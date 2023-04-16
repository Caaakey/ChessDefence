
namespace YMSoft.Core.Pawns
{
    public enum PawnState : int
    {
        None = 0,
        Building, Destorying,
        Wait, Attack, Busy
    }
}

namespace YMSoft.Core.Pawns
{
    public readonly struct BulletPointStruct : IPawnStatePoint
    {
        //  I NEED 'readonly record struct' T_T..
        public BulletPointStruct(float HP = 0, float AP = 1, float ADP = 0.5f, float SP = 0.1f, float DP = 0, float RP = 2)
        {
            HealthPoint = HP;
            AttackPoint = AP;
            AttackDelayPoint = ADP;
            ShootSpeedPoint = SP;
            DefensePoint = DP;
            RangePoint = RP;
        }
        public BulletPointStruct(IPawnStatePoint state)
        {
            HealthPoint = state.HealthPoint;
            AttackPoint = state.AttackPoint;
            AttackDelayPoint = state.AttackDelayPoint;
            ShootSpeedPoint = state.ShootSpeedPoint;
            DefensePoint = state.DefensePoint;
            RangePoint = state.RangePoint;
        }

        public float HealthPoint { get; }
        public float AttackPoint { get; }
        public float AttackDelayPoint { get; }
        public float ShootSpeedPoint { get; }
        public float DefensePoint { get; }
        public float RangePoint { get; }

#if UNITY_EDITOR
        public PawnState State =>
            throw new System.Exception("DONT USED IN State by 'BulletPointStruct'");
#else
        public PawnState State => PawnState.None;
#endif
    }

    public interface IPawnStatePoint
    {
        public float HealthPoint { get; }
        public float AttackPoint { get; }
        public float AttackDelayPoint { get; }
        public float ShootSpeedPoint { get; }
        public float DefensePoint { get; }
        public float RangePoint { get; }
        public PawnState State { get; }

    }
}