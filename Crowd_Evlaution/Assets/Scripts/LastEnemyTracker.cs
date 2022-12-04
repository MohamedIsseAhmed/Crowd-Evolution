using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LastEnemyTracker : MonoBehaviour
{
    public static LastEnemyTracker instance;
    [SerializeField] private List<GameObject> lastenemiesOnFrontLine;
    public event EventHandler OnAllEnemiesDied;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("LastEnemy");

        foreach (GameObject enemy in enemies)
        {
            lastenemiesOnFrontLine.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        if (lastenemiesOnFrontLine.Contains(enemyToRemove))
        {
            lastenemiesOnFrontLine.Remove(enemyToRemove);
        }
        if (lastenemiesOnFrontLine.Count == 0)
        {
            OnAllEnemiesDied?.Invoke(this, EventArgs.Empty);
        }
    }
}
