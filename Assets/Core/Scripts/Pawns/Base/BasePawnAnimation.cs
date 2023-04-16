using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BasePawnAnimation : MonoBehaviour, IPawnAction
    {
        [SerializeField] private Animator _animator;

        public bool OnHit(float damage)
        {
            return true;
        }
    }
}