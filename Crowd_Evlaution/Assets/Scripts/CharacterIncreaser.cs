using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterIncreaser : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterCounText;
   
    [SerializeField] private int characterCount;
    private void Start()
    {
        characterCounText.text = "  +"+ characterCount.ToString()+"\n   " + "PEOPLE";
    }
    private void OnTriggerEnter(Collider other)
    {
        UnitController unitController = other.gameObject.GetComponent<UnitController>();
        if (unitController != null)
        {
            unitController.IncreaseCharacter(characterCount);
        }
    }
}
