using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UnitSelectManager : MonoBehaviour
{
    
    public List<Units> selectedUnits {get; private set;} = new List<Units>() ; // List of selected units

    [SerializeField]
    private TIlesChecker tilesChecker;
    [SerializeField]
    private TileSelectManager tileSelected;
    [SerializeField]
    private TextMeshProUGUI errorText;

    public void SelectUnit(Units unit)
    {
        if (!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            unit.OnSelected(); // Call the unit's selection logic
        }
    }

    public void DeselectUnit(Units unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Remove(unit);
            unit.OnDeselected(); // Call the unit's deselection logic
        }
    }

    public void MoveAllUnits()
    {
        TileInfo tile = tileSelected.selectedTile;
        if (tile != null){
            errorText.text = "";
            foreach (Units unit in selectedUnits)
            {
                unit.MoveUnit(tile); 
                unit.EachMove();
                tilesChecker.checkchilds();
            }
        }else{
            errorText.text = "Please Select A tile";
        }

    }

    public void DeselectAllUnits()
    {
        foreach (Units unit in selectedUnits)
        {
            unit.OnDeselected(); // Deselect each unit
        }
        selectedUnits.Clear(); // Clear the list
    }

    public void RemoveAllUnits()
    {
        foreach (Units unit in selectedUnits)
        {
            unit.UnitRemoveSelf(); // Deselect each unit
        }
        selectedUnits.Clear(); // Clear the list
    }

    public void HarvestSelectedUnits()
    {
        foreach (Units unit in selectedUnits)
        {
            unit.UnitHarvest(); // Trigger the harvest logic for each unit
            
        }
    }

    public List<Units> GetSelectedUnits()
    {
        return selectedUnits; // Returns the list of selected units if needed
    }
}
