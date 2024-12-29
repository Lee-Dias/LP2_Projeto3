using UnityEngine;

public class TIlesChecker : MonoBehaviour
{

    [SerializeField] 
    private GameObject tilesparent; // The parent GameObject that holds all the tiles

    // Method to check if each tile in the parent GameObject has any children
    public void checkchilds()
    {
        // Loop through each child of the 'tilesparent' GameObject
        foreach (Transform child in tilesparent.transform)
        {
            // Get the TileInfo component attached to the current child GameObject
            TileInfo tilecode = child.GetComponent<TileInfo>();

            // Call the checkChild method from the TileInfo component to check if the tile has children
            tilecode.checkChild();
        }
    }

}
