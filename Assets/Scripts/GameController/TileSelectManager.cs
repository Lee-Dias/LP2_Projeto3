using TMPro;
using UnityEngine;
/// <summary>
/// This classe handles the selected tile 
/// sets a tile as selected and deselects a tile if it is clicked again and 
/// deselects a tile if another tile is selected
/// </summary>
public class TileSelectManager : MonoBehaviour
{
    // Public property that holds the reference to the currently selected tile
    public TileInfo selectedTile { get; private set; } 


    // Method to select a tile
    public void SelectTile(TileInfo tile)
    {
        // If there is already a selected tile, deselect it first
        if (selectedTile != null)
            selectedTile.OnDeselected();
        // Call the OnSelected method of the tile to update 
        //its state to selected
        tile.OnSelected(); 
        // Update the selectedTile property to the newly selected tile
        selectedTile = tile;

    }
    // Method to deselect the currently selected tile
    public void DeselectTile()
    {
        // Call the OnDeselected method of the currently 
        //selected tile to reset its state
        selectedTile.OnDeselected();
        // Clear the selectedTile property
        selectedTile = null;
        
    }

    // Method to get the currently selected tile
    public TileInfo GetSelectedUnit()
    {
        // Returns the reference to the currently selected tile
        return selectedTile; 
    }

}
