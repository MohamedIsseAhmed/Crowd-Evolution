using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterData")]
public class CharacterData : ScriptableObject
{
    public float shootTimer = 0;
    public float shootTimerMax = 1;
    public WeaponData weaponData;

    private void Awake()
    {
        shootTimer = shootTimerMax;
    }
}
