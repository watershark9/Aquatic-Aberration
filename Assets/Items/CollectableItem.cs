using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    public readonly UnityEvent Interacted = new();

    public void Awake()
    {
        Interacted?.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
