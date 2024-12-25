using UnityEngine;

public class TileVisuals : MonoBehaviour
{
    private Tile tile; // The logical data for this tile

    public int x;
    public int y;  

    /// <summary>
    /// Initializes the TileVisuals with terrain and resources.
    /// </summary>
    /// <param name="terrain">The terrain data for this tile.</param>
    /// <param name="resources">The resources on this tile.</param>
    public void Initialize(Lands land, Resources[] resources)
    {
        // Create the Tile object
        tile = new Tile(land);

        // Add resources to the tile
        foreach (var resource in resources)
        {
            tile.AddResource(resource);
        }

    }

    /// <summary>
    /// Gets the total Coins generated by this tile.
    /// </summary>
    /// <returns>Total Coins.</returns>
    private int GetTotalCoins()
    {
        return tile.CalculateCoins();
    }

    /// <summary>
    /// Gets the total Food generated by this tile.
    /// </summary>
    /// <returns>Total Food.</returns>
    private int GetTotalFood()
    {
        return tile.CalculateFood();
    }

    /// <summary>
    /// Gets the unique code for this tile.
    /// </summary>
    /// <returns>The tile's unique code.</returns>
    private string GetTileNameCode()
    {
        return tile.GetTileNameCode();
    }
}