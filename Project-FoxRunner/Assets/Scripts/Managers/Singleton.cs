using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    private static T instance;

    public static T Instance 
    {
        get {
            if(instance == null)
            {
                var objects = FindObjectsOfType(typeof(T)) as T[];
                if (objects != null) 
                {
                    instance = objects[0];
                }
                if(objects.Length > 1)
                {
                    Debug.Log("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<T>();
                }    
            }
            return instance;
        }
    }
}

public class SingletonPersistent<T> : MonoBehaviour
    where T : Component
{
    public static T instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
