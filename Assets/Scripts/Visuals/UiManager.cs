using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    // Variable to Set what turn it is
    private int gameTurn = 1;

    // Creates and empty list of resources 
    private List<Resources> allResources = new List<Resources> ();
    // Gets the text that shows the turn
    [SerializeField]
    private TextMeshProUGUI turn;
    // Gets the text that shows all resources
    [SerializeField]
    private TextMeshProUGUI showResources;
    // Gets the parent of all tiles
    [SerializeField]
    private GameObject parentOfTiles;
    // Gets the selected text
    [SerializeField]
    private TextMeshProUGUI Selected;
    // Gets the unitselectmanager
    [SerializeField]
    private UnitSelectManager unitSelectManager;
    // Gets the UnitsSelected
    [SerializeField]
    private GameObject unitsSelected;
    // Gets all unique Resources
    [SerializeField]
    private List<Resources> allUniqueResources;

    // Update method called every frame to update the UI with game information
    private void Update()
    {
        // Display the current turn number
        turn.text = "turn: " + gameTurn;
        // Update selected unit info
        UnitInfoSlected();
        // Update selected units info
        UnitsSelected();
        // Check and display all resources available in the game
        checkAllResourcesToGet();
    }

    // Method to increase the game turn by 1 when called
    public void Play()
    {
        gameTurn = gameTurn + 1;
    }

    // Method to display info about the selected units
    private void UnitInfoSlected()
    {
        // If exactly one unit is selected
        if (unitSelectManager.selectedUnits.Count == 1)
        {
            // Show the unit's name and resources it can harvest
            Selected.text = "Unit: " + unitSelectManager.selectedUnits.First().unitToPlay.UnitName + "\n" + " Total resources Harvested: " + "\n";
            // Loop through the resources that the selected unit can harvest
            foreach (Resources resource in unitSelectManager.selectedUnits.First().unitToPlay.ResourcesToHarvest)
            {
                // Add the resource name to the display
                Selected.text += resource.resourceName;            
                // Count how many times the resource was harvested by the selected unit
                int i = 0;
                foreach (Resources resources in unitSelectManager.selectedUnits.First().resourcesHarvested)
                {
                    i += 1; // Increase count for each harvested resource
                }
                // Display the number of resources harvested
                Selected.text += ": " + i + "\n";
            }
        }
        else
        {
            // If more than one unit is selected, display a summary of all selected units and resources harvested
            Selected.text = "Units Selected: " + unitSelectManager.selectedUnits.Count() + "\n" + " Total resources Harvested: " + "\n";       
            // Loop through all unique resources and calculate how many times each one has been harvested
            foreach (Resources resources in allUniqueResources)
            {
                int i = 0;
                foreach (Units unit in unitSelectManager.selectedUnits)
                {
                    // Count how many times each resource is harvested by any of the selected units
                    foreach (Resources resource in unit.resourcesHarvested)
                    {
                        i += 1; // Increase count for each harvested resource
                    }
                }
                // Display the resource count for each resource
                Selected.text += resources.resourceName + " x" + i + "\n";
            }
        }
    }

    // Method to check and display the total number of resources available in the game
    private void checkAllResourcesToGet()
    {
        // Display the header for the resources
        showResources.text = "Total Resources: \n";
        // Initialize a list to store all the resources in the game
        allResources = new List<Resources>();
        // Loop through all tiles and collect resources from them
        foreach (Transform child in parentOfTiles.transform)
        {
            TileInfo tilecode = child.GetComponent<TileInfo>();
            // Add each resource in the tile to the allResources list
            foreach (Resources resource in tilecode.GetResources())
            {
                allResources.Add(resource);
            }
        }
        // Loop through each unique resource and count how many times it appears in the game
        foreach (Resources resource in allUniqueResources)
        {
            int i = 0;
            // Count how many times the resource is present in allResources
            foreach (Resources resources in allResources)
            {
                if (resource == resources)
                {
                    // Increase count if the resource matches
                    i += 1; 
                }
            }
            // Display the count of each unique resource
            showResources.text = showResources.text + resource.resourceName + " x" + i + "\n";
        }
    }

    // Method to check if there are any selected units and show or hide the units Selected accordingly
    private void UnitsSelected()
    {
        // If no units are selected hide the units Selected
        if (unitSelectManager.selectedUnits.Count() == 0)
        {
            unitsSelected.SetActive(false);
        }
        else
        {
            // If units are selected, show the units Selected
            unitsSelected.SetActive(true);
        }
    }

}
