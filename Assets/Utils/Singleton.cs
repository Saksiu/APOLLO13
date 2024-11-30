using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;
    //public bool m_DontDestroyOnLoad = false;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            //if(m_DontDestroyOnLoad)
            //    DontDestroyOnLoad(this);
        }
        else{Destroy(this);}
    }

    protected bool IsTheOne()=>Instance == this;
}