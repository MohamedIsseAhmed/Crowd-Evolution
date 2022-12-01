using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class YearIncreaser : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private int year;
    private void Start()
    {
        yearText.text ="   +\n   " +year.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        UnitController unitController=other.gameObject.GetComponent<UnitController>();
        if(unitController != null)
        {
            
            unitController.IncreaseOrDecreaseYear(year);
        }
    }
}
