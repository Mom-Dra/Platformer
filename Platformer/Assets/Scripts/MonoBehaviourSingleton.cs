using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> :  MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        { 
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<T>();
                    gameObject.name = typeof(T).Name;

                    DontDestroyOnLoad(gameObject);

                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;

            DontDestroyOnLoad (gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
