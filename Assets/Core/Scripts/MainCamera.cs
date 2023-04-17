using UnityEngine;

namespace YMSoft.Core
{
    public class MainCamera : DynamicMonoInstance<MainCamera>
    {
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private Camera _camera;

        public Camera GetCamera => _camera;

    }
}
