using System;
using System.Collections.Generic;

namespace YMSoft.Core.Internal
{
    public class DynamicInstanceManager : IDisposable
    {
        public DynamicInstanceManager()
        {
            _list = new();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += (state) =>
            {
                if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
                    Release();
            };
#endif
        }

        private static DynamicInstanceManager _instance = null;

        private readonly List<IDynamicInstance> _list = null;

        private static DynamicInstanceManager Instance
        {
            get
            {
                _instance ??= new();

                return _instance;
            }
        }

        internal static void Regist(IDynamicInstance instance)
        {
            if (Instance._list.Exists(x => x == instance))
            {
#if UNITY_EDITOR
                throw new Exception($"{instance} is duplicated");
#endif
            }
            Instance._list.Add(instance);
        }

        internal static void Remove(IDynamicInstance instance)
            => Instance._list.Remove(instance);

        internal static T Find<T>() where T : class
        {
            for (int i = 0; i < Instance._list.Count; ++i)
            {
                if (Instance._list[i] is T value)
                    return value;
            }

            return null;
        }

        public static void Release()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public void Dispose()
        {
            while (_list.Count != 0)
            {
                IDynamicInstance instance = _list[0];
                instance.Destroy();
            }

            _list.Clear();
        }
    }
}
