using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FrontLinePoints : MonoBehaviour
{

    [SerializeField] private List<Transform> frontlinePoints;
    public List<Transform> FrontLinePointsList => frontlinePoints;
    public static FrontLinePoints instance;
    public event EventHandler<List<Transform>> OnFrontLİneTakeActionEvent;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnFrontLİneTakeActionEvent?.Invoke(this, frontlinePoints);
        }
    }
}
