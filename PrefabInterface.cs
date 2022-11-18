using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InterfaceRegister<T>:ISerializationCallbackReceiver where T:class
{
    [SerializeField] private GameObject go;
    [SerializeField] private string typeName;
    private T Instance { get; set; }

    public static implicit operator T(InterfaceRegister<T> obj) => obj.GetInterface();

    public T GetInterface()
    {
        if(Instance == null)
        {
            Instance = go.GetComponent<T>();
        }
        return Instance;
    }

    public void OnBeforeSerialize()
    {
        Check();
    }

    public void OnAfterDeserialize()
    {
        Check();
    }

    private void Check()
    {
        if (typeName != typeof(T).ToString())
        {
            go = null;
            typeName = typeof(T).ToString();
        }
    }
}
