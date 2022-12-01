using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharacterCreator")]
public class ObjectCreater : ScriptableObject
{
    public Transform characterUnder500Year;
    public Transform characterUpper500Year;
    public Transform character1500Year;
    public Transform characterUpper1500Year;
    public int currentYearMax;
    private void OnEnable()
    {
        currentYearMax = 0;
    }
    public  Transform CreateObjec(int year)
    {
        if (year <= 750)
        {
            currentYearMax = 750;
            return characterUnder500Year;
        }
        else if (year <= 1000 && year>750)
        {
            currentYearMax = 1000;
            return characterUpper500Year;
        }
        else if (year >=1000 && year<=1500  )
        {
            currentYearMax = 1500;
            return character1500Year;
        }
        else if (year > 1500)
        {
            currentYearMax = 1501;
            return characterUpper1500Year;
        }
        return null;
    }
    
}
