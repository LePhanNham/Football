using UnityEngine;

namespace Base.Singleton
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T foundInstance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                    if (foundInstance != null)
                    {
                        return foundInstance;
                    }

                    Debug.Log("Create new singleton: " + typeof(T).Name + " in scene");
                    var go = new GameObject { name = typeof(T).Name };
                    _instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                OnAwake();
                return;
            }

            if (_instance == this)
            {
                return;
            }

            Debug.LogWarning("Destroy duplicate singleton: " + typeof(T).ToString());
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        protected abstract void OnAwake();
    }
}