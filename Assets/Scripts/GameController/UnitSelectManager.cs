using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// This classe handles the selected unit 
/// sets a unit as selected and deselects a unit if it is clicked again and 
/// deselects a tile if another tile is selected or selects another unit if 
/// the player is clicking ctrl.
/// </summary>
public class UnitSelectManager : MonoBehaviour
{
    
    // List to store the currently selected units. 
    public List<Units> selectedUnits {get; private set;} = new List<Units>();
    // Reference to the TileSelectManager,
    // used to manage the selected tile in the game
    [SerializeField]
    private TileSelectManager tileSelected;  

    // UI Text element to display error messages
    [SerializeField]
    private TextMeshProUGUI errorText;  


    // Method to select a unit.
    public void SelectUnit(Units unit)
    {
        if (!selectedUnits.Contains(unit))
        {
            // Add the unit to the selected units list
            selectedUnits.Add(unit);  
            // Call the unit's selection logic
            unit.OnSelected();  
        }
    }

    // Method to deselect a unit. 
    public void DeselectUnit(Units unit)
    {
        if (selectedUnits.Contains(unit))
        {
            // Remove the unit from the selected units list
            selectedUnits.Remove(unit);  
            // Call the unit's deselection logi
            unit.OnDeselected();  
        }
    }

    // Method to move all selected units to the currently selected tile. 
    public void MoveAllUnits()
    {
        // Get the currently selected tile
        TileInfo tile = tileSelected.selectedTile;  
        // If a tile is selected
        if (tile != null) 
        {
            // Clear any previous error message
            errorText.text = "";  
            // Iterate over each selected unit
            foreach (Units unit in selectedUnits)  
            {
                // Move the unit to the selected tile
                unit.MoveUnit(tile);  
                // Trigger any additional movement logic for the unit
                unit.EachMove();  
            }
        }
        else
        {
            // Show an error message if no tile is selected
            errorText.text = "Please Select A tile";  
        }
    }

    // Method to deselect all units. 
    public void DeselectAllUnits()
    {
        // Iterate over all selected units
        foreach (Units unit in selectedUnits)  
        {
            // Deselect each unit
            unit.OnDeselected(); 
        }
        // Clear the list of selected units
        selectedUnits.Clear();  
    }

    // Method to remove all selected units from the game. 
    public void RemoveAllUnits()
    {
        // Iterate over each selected unit
        foreach (Units unit in selectedUnits)  
        {
            // Trigger the unit's self-remove logic
            unit.UnitRemoveSelf();  
        }
        // Clear the list of selected units
        selectedUnits.Clear();  
    }

    // Method to harvest resources for all selected units. 
    public void HarvestSelectedUnits()
    {
        // Iterate over each selected unit
        foreach (Units unit in selectedUnits)  
        {
            // Trigger the harvest logic for each unit
            unit.UnitHarvest();  
        }
    }

    // Method to get the list of currently selected units.
    public List<Units> GetSelectedUnits()
    {
        // Return the list of selected units
        return selectedUnits;  
    }

}
