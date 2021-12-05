using UnityEngine;

namespace SLGame.Modules
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                GameObject obj = new GameObject();
                _instance = obj.AddComponent<T>();
                obj.name = typeof(T).ToString();

                return _instance;
            }

            set
            {
                if (_instance == null)
                {
                    _instance = value;
                    return;
                }
                else
                {
                    Debug.LogError("Instance already set, u can't change it");
                }
            }
        }

        public static void Initialization()
        {
            GameObject obj = new GameObject();
            _instance = obj.AddComponent<T>();
            obj.name = typeof(T).ToString();
        }
    }
}
