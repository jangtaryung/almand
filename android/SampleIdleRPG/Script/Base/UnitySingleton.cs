using UnityEngine;


public class UnitySingleton<T> : BaseMonoBehaviour
        where T : Component
{
    private static T _instance;
    public static bool isDestroy;

    public static T Instance
    {
        get
        {
            if (isDestroy)
            {
                return null;
            }
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
//                    obj.hideFlags = HideFlags.HideInHierarchy;
                    _instance = (T)obj.AddComponent(typeof(T));
//                    print("obj.name ->" + obj.name);
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
//        Debug.Log("Destroy " + _instance);
        if (_instance != null)
        {
            DestroyImmediate(_instance);
            _instance = null;
            isDestroy = true;
        }
    }
    void OnDestroy()
    {
        if (isDestroy)
        {
            return;
        }
        if (_instance != null)
        {
            Destroy(_instance);
            _instance = null;
        }
    } 
}
