using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileSelectManager : MonoBehaviour
{
    public TileInfo selectedTile {get; private set;} // List of selected units
    [SerializeField]
    private TextMeshProUGUI tileTextInfo; // List of selected units

    public void SelectTile(TileInfo tile)
    {
        if (selectedTile != null)
            selectedTile.OnDeselected();
        tile.OnSelected(); 
        selectedTile = tile;
        tileTextInfo.text = selectedTile.GetTileNameAndResources() + " coins: " + selectedTile.GetTotalCoins() + " Food: " + selectedTile.GetTotalFood();

    }

    public void DeselectTile(TileInfo tile)
    {
        selectedTile.OnDeselected();
        selectedTile = null;
        tileTextInfo.text = "No tile selected";

    }
    public TileInfo GetSelectedUnit()
    {
        return selectedTile; // Returns the list of selected units if needed
    }
}
