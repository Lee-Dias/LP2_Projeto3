using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    // Variable to Set what turn it is
    private int gameTurn = 1;

    // Creates and empty list of ResourcesGame 
    private List<ResourcesGame> allResourcesGame = new List<ResourcesGame> ();
    // Gets the text that shows the turn
    [SerializeField]
    private TextMeshProUGUI turn;
    // Gets the text that shows all ResourcesGame
    [SerializeField]
    private TextMeshProUGUI showResourcesGame;
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
    // Gets all unique ResourcesGame
    [SerializeField]
    private List<ResourcesGame> allUniqueResourcesGame;

    // Update method called every frame to update the UI with game information
    private void Update()
    {
        // Display the current turn number
        turn.text = "turn: " + gameTurn;
        // Update selected unit info
        UnitInfoSlected();
        // Update selected units info
        UnitsSelected();
        // Check and display all ResourcesGame available in the game
        checkAllResourcesGameToGet();
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
            // Show the unit's name and ResourcesGame it can harvest
            Selected.text = "Unit: " + unitSelectManager.selectedUnits.First().unitToPlay.UnitName + "\n" + " Total ResourcesGame Harvested: " + "\n";
            // Loop through the ResourcesGame that the selected unit can harvest
            foreach (ResourcesGame resource in unitSelectManager.selectedUnits.First().unitToPlay.ResourcesToHarvest)
            {
                // Add the resource name to the display
                Selected.text += resource.ResourceName;            
                // Count how many times the resource was harvested by the selected unit
                int i = 0;
                foreach (ResourcesGame ResourcesGame in unitSelectManager.selectedUnits.First().ResourcesGameHarvested)
                {
                    i += 1; // Increase count for each harvested resource
                }
                // Display the number of ResourcesGame harvested
                Selected.text += ": " + i + "\n";
            }
        }
        else
        {
            // If more than one unit is selected, display a summary of all selected units and ResourcesGame harvested
            Selected.text = "Units Selected: " + unitSelectManager.selectedUnits.Count() + "\n" + " Total ResourcesGame Harvested: " + "\n";       
            // Loop through all unique ResourcesGame and calculate how many times each one has been harvested
            foreach (ResourcesGame ResourcesGame in allUniqueResourcesGame)
            {
                int i = 0;
                foreach (Units unit in unitSelectManager.selectedUnits)
                {
                    // Count how many times each resource is harvested by any of the selected units
                    foreach (ResourcesGame resource in unit.ResourcesGameHarvested)
                    {
                        i += 1; // Increase count for each harvested resource
                    }
                }
                // Display the resource count for each resource
                Selected.text += ResourcesGame.ResourceName + " x" + i + "\n";
            }
        }
    }

    // Method to check and display the total number of ResourcesGame available in the game
    private void checkAllResourcesGameToGet()
    {
        // Display the header for the ResourcesGame
        showResourcesGame.text = "Total ResourcesGame: \n";
        // Initialize a list to store all the ResourcesGame in the game
        allResourcesGame = new List<ResourcesGame>();
        // Loop through all tiles and collect ResourcesGame from them
        foreach (Transform child in parentOfTiles.transform)
        {
            TileInfo tilecode = child.GetComponent<TileInfo>();
            // Add each resource in the tile to the allResourcesGame list
            foreach (ResourcesGame resource in tilecode.GetResources())
            {
                allResourcesGame.Add(resource);
            }
        }
        // Loop through each unique resource and count how many times it appears in the game
        foreach (ResourcesGame resource in allUniqueResourcesGame)
        {
            int i = 0;
            // Count how many times the resource is present in allResourcesGame
            foreach (ResourcesGame ResourcesGame in allResourcesGame)
            {
                if (resource == ResourcesGame)
                {
                    // Increase count if the resource matches
                    i += 1; 
                }
            }
            // Display the count of each unique resource
            showResourcesGame.text = showResourcesGame.text + resource.ResourceName + " x" + i + "\n";
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
