using UnityEngine;

namespace YMSoft.Core
{
    public class BaseTile : MonoBehaviour, ISelectableObject
    {
        private void Awake()
        {
            _Collider = GetComponent<Collider>();
        }

        private Collider _Collider;
        private GameObject _ChildObject = null;
        private bool _isSelected = false;

        public bool IsSelected => _isSelected;

        public void OnStartSelect()
        {
            
        }

        public void OnSelected()
        {
            
        }

        public void OnEndSelect()
        {
            
        }
    }
}