using UnityEngine;
using YMSoft.Core.Internal;

namespace YMSoft.Core
{
    public abstract class DynamicMonoInstance<T> : MonoBehaviour, IDynamicInstance
        where T : MonoBehaviour
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DynamicInstanceManager.Find<T>();

                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();

                        if (_instance != null)
                        {
                            DynamicInstanceManager.Regist(_instance as IDynamicInstance);
                            return _instance;
                        }
                    }
                    else
                        return _instance;

                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    DynamicInstanceManager.Regist(_instance as IDynamicInstance);
                }

                return _instance;
            }
        }

        public static void SetInstance(T instance)
            => _instance = instance;

        void IDynamicInstance.Destroy()
        {
            OnDestroy();
            DynamicInstanceManager.Remove(this);

            _instance = null;
        }

        public virtual void OnDestroy() { }
    }
}
