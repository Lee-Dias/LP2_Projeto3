using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TileInfo : MonoBehaviour
{
    private Tile tile; // The logical data for this tile

    //tiles x
    public int x;
    //tiles y
    public int y;  

    //checks if tile has child
    public bool hasChild{get; private set;} = false;
    // For visual feedback on the blink
    private Renderer unitRenderer; 
    // Stores the original color of the unit
    private Color originalColor; 
    // Tracks whether the unit is blinking
    private bool isBlinking = false;


    // Method to initialize the tile with a specified land type
    public void Initialize(Lands land)
    {
        // Create the Tile object using the provided land
        tile = new Tile(land);
        // Get the Renderer component of the current GameObject (likely the tile's visual representation)
        unitRenderer = GetComponent<Renderer>();
        // Store the original color of the unit's material for future reference (e.g., for the blink effect)
        originalColor = unitRenderer.material.color;
    }

    // Method to check if the tile has any child objects (e.g., a unit or other objects)
    public void checkChild()
    {
        // If the tile has one or more child objects, set the hasChild flag to true
        if (this.transform.childCount > 0)
        {
            hasChild = true;
        }
        else
        {
            // Otherwise, set the hasChild flag to false
            hasChild = false;
        }
    }

    // Method to retrieve the list of resources present on the tile
    public List<ResourcesGame> GetResources()
    {
        // Return the list of resources stored in the tile object
        return tile.resources;
    }
 

    // Method to add resources to the tile
    public void AddResources(ResourcesGame resources)
    {
        // Call the AddResource method on the tile to add the specified resource
        tile.AddResource(resources, this.gameObject);
    }

    // Method to remove resources from the tile
    public void RemoveResources(ResourcesGame resources)
    {
        // Call the RemoveResource method on the tile to remove the resource
        tile.RemoveResource(resources);
    }

    // Method to calculate the total amount of coins available on the tile
    public int GetTotalCoins()
    {
        // Call the CalculateCoins method on the tile and return the total number of coins
        return tile.CalculateCoins();
    }

    // Method to calculate the total amount of food available on the tile
    public int GetTotalFood()
    {
        // Call the CalculateFood method on the tile and return the total amount of food
        return tile.CalculateFood();
    }

    // Method to get the name of the tile and its resources in a string
    public string GetTileNameAndResources()
    {
        // Return the name of the tile and the resources it contains
        return tile.GetTileNameAndResources();
    }

    // Method to handle mouse click on the tile
    private void OnMouseDown()
    {
        // Find the first instance of UnitSelectManager in the scene
        UnitSelectManager unitSelectManager = FindFirstObjectByType<UnitSelectManager>();
        // Find the first instance of TileSelectManager in the scene
        TileSelectManager tileSelectManager = FindFirstObjectByType<TileSelectManager>();
        // Check if the tile is already selected in the TileSelectManager
        if (tileSelectManager.selectedTile == this)
        {
            // If selected, deselect the tile
            tileSelectManager.DeselectTile();
        }
        else
        {
            // If not selected, select the current tile
            tileSelectManager.SelectTile(this);
        }
    }


    // Method to handle the unit selection, triggering the blinking effect
    public void OnSelected()
    {
        // Set the isBlinking to true, indicating the unit is selected and should blink
        isBlinking = true;
        // Start the blinking effect coroutine to make the unit blink
        StartCoroutine(BlinkEffect());
    }

    // Method to handle the unit deselection, stopping the blinking effect
    public void OnDeselected()
    {
        // Stop the blinking effect coroutine when the unit is deselected
        StopCoroutine(BlinkEffect());
        // Set the isBlinking to false, indicating the unit is no longer blinking
        isBlinking = false;
        // Reset the units material color back to the original color
        unitRenderer.material.color = originalColor;
    }

    // Coroutine to create a blinking effect on the unit
    private IEnumerator BlinkEffect()
    {
        // Set the flag indicating the unit is blinking to true
        isBlinking = true;

        // Define the speed at which the unit will blink
        float blinkSpeed = 2f;

        // Continue blinking as long as isBlinking is true
        while (isBlinking)
        {
            // Transition from the original color to white
            float t = 0f;
            // Increment t over time and the blink speed to transition between the colors
            while (t < 1f)
            {
                // Increase t based on the time passed and blink speed
                t += Time.deltaTime * blinkSpeed;  
                // go between the original color and white based on t
                unitRenderer.material.color = Color.Lerp(originalColor, Color.white, t);
                // Wait until the next frame to continue
                yield return null;
            }

            // Reset t for the next transition
            t = 0f;  
            // transition back to the original color from white
            while (t < 1f)
            {
                t += Time.deltaTime * blinkSpeed;  // Increase t over time for the reverse transition
                // go between white and the original color based on t
                unitRenderer.material.color = Color.Lerp(Color.white, originalColor, t);
                // Wait until the next frame to continue
                yield return null;
            }
        }
    }

}