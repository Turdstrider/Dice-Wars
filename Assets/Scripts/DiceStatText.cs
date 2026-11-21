using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceStatText : TextUpdater
{
    private ObjectSelector objectSelector;

    protected override void Awake()
    {
        base.Awake(); //execute base class' Awake function.

        objectSelector = Camera.main.GetComponent<ObjectSelector>();
    }
    //overrides previous behaviour of TextUpdater (which did nothing before but I digress).
    public override void ChangeText() //Is called by selecting/deselecting a Dice..
    {
        if(objectSelector.currentSelectedDie != null)
        {
            textMesh.text = "Dice: " + objectSelector.currentSelectedDie.name + "\nBasic Rolls: " + objectSelector.currentSelectionManager.basicRollCount;
        }
        else
        {
            textMesh.text = "No Dice Selected.";
        }
    }
}