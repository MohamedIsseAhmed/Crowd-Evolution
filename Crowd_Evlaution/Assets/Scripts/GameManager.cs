using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "GameManager")]
public class GameManager : ScriptableObject
{
    public event EventHandler OnLevelComplated;
    public event EventHandler OnFailed;
    public event EventHandler CheckPlayerUnitNumber;

    public void RaiseLevelComplatedEvent(object s,EventArgs e)
    {
        OnLevelComplated?.Invoke(s, e);
    }
    public void RaiseFailedEvent(object s, EventArgs e)
    {
        OnFailed?.Invoke(s, e);
    }
    public void RaiseCheckPlayerUnitNumberEvent(object s, EventArgs e)
    {
        CheckPlayerUnitNumber?.Invoke(s, e);
    }
}
