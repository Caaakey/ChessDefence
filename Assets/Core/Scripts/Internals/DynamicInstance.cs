using YMSoft.Core.Internal;

namespace YMSoft.Core
{
    public abstract class DynamicInstance<T> : IDynamicInstance
        where T : class, new()
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                _instance = DynamicInstanceManager.Find<T>();
                if (_instance == null)
                {
                    _instance = new();
                    DynamicInstanceManager.Regist(_instance as IDynamicInstance);
                }

                return _instance;
            }
        }

        void IDynamicInstance.Destroy()
        {
            OnDestroy();
            DynamicInstanceManager.Remove(this);

            _instance = null;
        }

        public virtual void OnDestroy() { }
    }
}
