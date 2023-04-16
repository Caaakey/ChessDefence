using System;
using Unity.Mathematics;
using UnityEngine;

namespace System.Collections
{
    public static class ArrayExtension
    {
        public static bool IsNullOrEmpty(this Array arr)
            => arr == null || arr.Length == 0;
    }
}

namespace UnityEngine
{
    public static class UnityEngineExtension
    {
        public static T GetParentInComponent<T>(this Component obj) where T : Component
            => GetParentInComponent<T>(obj.transform);

        public static T GetParentInComponent<T>(this Transform t)
            where T : Component
        {
            if (t == null)
                return null;

            if (t.TryGetComponent(out T comp))
                return comp;
            else
                return GetParentInComponent<T>(t.parent);
        }
    }

    public class OnGUIScaler : IDisposable
    {
        public OnGUIScaler(int width, int height)
        {
            _mat = GUI.matrix;

            Vector3 scale = new(Screen.width / width, Screen.height / height, 1);
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, quaternion.identity, scale);
        }

        private readonly Matrix4x4 _mat;

        public void Dispose()
        {
            GUI.matrix = _mat;
        }
    }
}

#if UNITY_EDITOR
namespace UnityEngine
{
    public static class GizmosExtension
    {
        public class Scope : IDisposable
        {
            public Scope(Color color, Matrix4x4 mat)
            {
                _prevColor = Gizmos.color;
                _prevMatrix = Gizmos.matrix;

                Gizmos.color = color;
                Gizmos.matrix = mat;
            }

            private readonly Color _prevColor;
            private readonly Matrix4x4 _prevMatrix;

            public void Dispose()
            {
                Gizmos.matrix = _prevMatrix;
                Gizmos.color = _prevColor;
            }
        }
    }

}
#endif

namespace YMSoft.Core
{
    public class WaitTimer
    {
        private float _currentTime = 0;
        private float _waitTime = 0;

        public float WaitTime
        {
            get => _waitTime;
            set
            {
                _waitTime = value;
                _currentTime = 0;
            }
        }

        public bool IsWait()
        {
            if (_currentTime >= _waitTime)
                return false;

            _currentTime += Time.deltaTime;

            return true;
        }
    }
}