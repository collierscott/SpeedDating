﻿using UnityEngine;

// ReSharper disable once CheckNamespace
public class Script : MonoBehaviour
{
    public T Find<T>() where T : MonoBehaviour
    {
        return FindObjectOfType<T>();
    }

    public T Get<T>() where T : MonoBehaviour
    {
        return GetComponent<T>();
    }
}