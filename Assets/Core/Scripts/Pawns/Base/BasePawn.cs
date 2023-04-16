using UnityEngine;

namespace YMSoft.Core.Pawns
{
    public class BasePawn : MonoBehaviour
    {
        [SerializeField] private BasePawnState _state;
        [SerializeField] private BasePawnAction _action;


    }
}