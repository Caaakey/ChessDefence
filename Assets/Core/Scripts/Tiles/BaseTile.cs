using UnityEngine;
using YMSoft.Core.Pawns;

namespace YMSoft.Core
{
    public class BaseTile : MonoBehaviour
    {
        private BasePawn _child = null;

        public BasePawn Child => _child;

        public void CreateChild(BasePawn prefab)
        {
            BasePawn p = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            p.transform.localPosition = new Vector3(0, 0.5f, 0);

            _child = p;
        }

        public void DestroyChild()
        {
            if (_child == null) return;

            Destroy(_child);
        }

    }
}