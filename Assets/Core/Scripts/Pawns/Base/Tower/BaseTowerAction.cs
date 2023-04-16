using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BaseTowerAction : BasePawnAction
    {
        [SerializeField] protected Transform ShootStartTransform;
        [SerializeField] protected BaseBullet BulletPrefab;

        protected override void OnAttack(Collider[] targets, int count)
        {
            BaseBullet instance = Instantiate(BulletPrefab, ShootStartTransform.position, Quaternion.identity);
            instance.Create(PawnState, targets[0].GetComponent<BasePawnState>());

        }
    }


}