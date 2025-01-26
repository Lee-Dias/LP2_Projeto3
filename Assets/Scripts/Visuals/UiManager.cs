using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
/// <summary>
/// Handles all the UI in the game getting
/// all the information that the player needs and showing it to him
/// </summary>
public class UiManager : MonoBehaviour
{
    // Variable to Set what turn it is
    private int gameTurn = 1;

    // Creates and empty list of ResourceGame 
    private List<ResourceGame> allResourceGame = new List<ResourceGame> ();
    // Gets the text that shows the turn
    [SerializeField]
    private TextMeshProUGUI turn;
    // Gets the text that shows all ResourceGame
    [SerializeField]
    private TextMeshProUGUI showResourceGame;
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
    // Gets all unique ResourceGame
    [SerializeField]
    private List<ResourceGame> allUniqueResourceGame;
    [SerializeField]
    private TileSelectManager TileManager;
    [SerializeField]
    // Displays information like coin and food counts for the selected tile
    private TextMeshProUGUI tileTextInfo; 

    private TileInfo selectedTile;

    void Start (){
        
    }
    // Update method called every frame to update the UI with game information
    private void Update()
    {
        selectedTile = TileManager.GetSelectedUnit();
        // Display the current turn number
        turn.text = "turn: " + gameTurn;
        // Update selected unit info
        UnitInfoSlected();
        // Update selected units info
        UnitsSelected();
        // Check and display all ResourceGame available in the game
        CheckAllResourceGameToGet();
        // Update the tile information text on the UI 
        //with the selected tiles details
        if (selectedTile != null){
            tileTextInfo.text = selectedTile.GetTileNameAndResources() +
            " coins: " + selectedTile.GetTotalCoins() 
            + " Food: " + selectedTile.GetTotalFood();
        }else{
            tileTextInfo.text = "No selected tile";
        }
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
            // Show the unit's name and ResourceGame it can harvest
            Selected.text = "Unit: " + unitSelectManager.selectedUnits.First().unitToPlay.UnitName + "\n" + " Total ResourceGame Harvested: " + "\n";
            // Loop through the ResourceGame that the selected unit can harvest
            foreach (ResourceGame resource in unitSelectManager.selectedUnits.First().unitToPlay.ResourcesToHarvest)
            {
                // Add the resource name to the display
                Selected.text += resource.ResourceName;            
                // Count how many times the resource was harvested by the selected unit
                int i = 0;
                foreach (ResourceGame ResourceGame in unitSelectManager.selectedUnits.First().ResourceGameHarvested)
                {
                    i += 1; // Increase count for each harvested resource
                }
                // Display the number of ResourceGame harvested
                Selected.text += ": " + i + "\n";
            }
        }
        else
        {
            // If more than one unit is selected, display a summary of all selected units and ResourceGame harvested
            Selected.text = "Units Selected: " + unitSelectManager.selectedUnits.Count() + "\n" + " Total ResourceGame Harvested: " + "\n";       
            // Loop through all unique ResourceGame and calculate how many times each one has been harvested
            foreach (ResourceGame ResourceGame in allUniqueResourceGame)
            {
                int i = 0;
                foreach (Units unit in unitSelectManager.selectedUnits)
                {
                    // Count how many times each resource is harvested by any of the selected units
                    foreach (ResourceGame resource in unit.ResourceGameHarvested)
                    {
                        i += 1; // Increase count for each harvested resource
                    }
                }
                // Display the resource count for each resource
                Selected.text += ResourceGame.ResourceName + " x" + i + "\n";
            }
        }
    }

    // Method to check and display the total number of ResourceGame available in the game
    private void CheckAllResourceGameToGet()
    {
        // Display the header for the ResourceGame
        showResourceGame.text = "Total ResourceGame: \n";
        // Initialize a list to store all the ResourceGame in the game
        allResourceGame = new List<ResourceGame>();
        // Loop through all tiles and collect ResourceGame from them
        foreach (Transform child in parentOfTiles.transform)
        {
            TileInfo tilecode = child.GetComponent<TileInfo>();
            // Add each resource in the tile to the allResourceGame list
            foreach (ResourceGame resource in tilecode.GetResources())
            {
                allResourceGame.Add(resource);
            }
        }
        // Loop through each unique resource and count how many times it appears in the game
        foreach (ResourceGame resource in allUniqueResourceGame)
        {
            int i = 0;
            // Count how many times the resource is present in allResourceGame
            foreach (ResourceGame ResourceGame in allResourceGame)
            {
                if (resource == ResourceGame)
                {
                    // Increase count if the resource matches
                    i += 1; 
                }
            }
            // Display the count of each unique resource
            showResourceGame.text = showResourceGame.text + resource.ResourceName + " x" + i + "\n";
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
