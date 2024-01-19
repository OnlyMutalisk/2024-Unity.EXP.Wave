using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) // ���ӿ�����Ʈ ��ü�� ��ȯ
    {
        Transform transfrom = FindChild<Transform>(go, name, recursive);
        if (transfrom == null)
        {
            return null;
        }
        return transfrom.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object // ���� ������Ʈ�� �ڽĵ��� T�� ������Ʈ ��ȯ
    {
        if (go == null)
            return null;
        if(recursive == false) 
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }

        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }

            }
        }
        return null;
    }

}
