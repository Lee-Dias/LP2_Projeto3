using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileSelectManager : MonoBehaviour
{
    // Public property that holds the reference to the currently selected tile
    public TileInfo selectedTile { get; private set; } 

    [SerializeField]
    // Displays information like coin and food counts for the selected tile
    private TextMeshProUGUI tileTextInfo; 

    // Method to select a tile
    public void SelectTile(TileInfo tile)
    {
        // If there is already a selected tile, deselect it first
        if (selectedTile != null)
            selectedTile.OnDeselected();
        // Call the OnSelected method of the tile to update its state to selected
        tile.OnSelected(); 
        // Update the selectedTile property to the newly selected tile
        selectedTile = tile;
        // Update the tile information text on the UI with the selected tiles details
        tileTextInfo.text = selectedTile.GetTileNameAndResources() + " coins: " + selectedTile.GetTotalCoins() + " Food: " + selectedTile.GetTotalFood();
    }
    // Method to deselect the currently selected tile
    public void DeselectTile()
    {
        // Call the OnDeselected method of the currently selected tile to reset its state
        selectedTile.OnDeselected();
        // Clear the selectedTile property
        selectedTile = null;
        // Update the UI text to reflect that no tile is currently selected
        tileTextInfo.text = "No tile selected";
    }

    // Method to get the currently selected tile
    public TileInfo GetSelectedUnit()
    {
        // Returns the reference to the currently selected tile
        return selectedTile; 
    }

}
