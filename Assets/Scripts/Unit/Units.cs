using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;
using NUnit.Framework;

public class Units : MonoBehaviour
{
    //Variable for unit to know if he is a miner, butcher etc...
    public Unit unitToPlay;
    //Variable for currenttile that the unit is in
    private TileInfo currentTile;
    //Variable for the ResourcesGame in the tile that the unit is in
    private List<ResourcesGame> ResourcesGameInTile;
    //Variable for the ResourcesGame that the unit has harvested
    public List<ResourcesGame> ResourcesGameHarvested {get; private set;} = new List<ResourcesGame>();
    //Variable for the UnitSelectManager
    private UnitSelectManager unitSelectManager;
    // For visual feedback on the blink
    private Renderer unitRenderer; 
    // Stores the original color of the unit
    private Color originalColor; 
    // Tracks whether the unit is blinking
    private GameObject parentOfTiles; 
    // Tracks whether the unit is blinking
    private bool isBlinking = false; 

    // Does this code at when the script is created in the gameobject
    void Start()
    {
        parentOfTiles = GameObject.Find("TilesParent");
        //sets the variable unitRenderer to the renderer that the object
        unitRenderer = GetComponent<Renderer>();
        //gets the renderers original color
        originalColor = unitRenderer.material.color;
        //does the method EachMoveSSS
        EachMove();
    }

    //A method so the currentTile variable always has the correct parent
    public void EachMove(){
        //gets the parent of the object
        currentTile = this.GetComponentInParent<TileInfo>();
        //gets the ResourcesGame of the parent
        ResourcesGameInTile = currentTile.GetResources();
    }
    //checks if the player left clicks the mouse
    private void OnMouseDown()
    {
        // gets the unitSelectManager
        unitSelectManager = FindFirstObjectByType<UnitSelectManager>();
        // Check if the unit is already selected
        if (unitSelectManager.GetSelectedUnits().Contains(this))
        {
            //checks if the player is holding ctrl
            if(Input.GetKey(KeyCode.LeftControl)){
                //deselects this unit
                unitSelectManager.DeselectUnit(this);
            //does this if the player isnt holding ctrl
            }else{
                //deselects all units
                unitSelectManager.DeselectAllUnits();
                //Selects this unit
                unitSelectManager.SelectUnit(this);
            }
            
        }
        //does this if this unit isnt selected
        else
        {
            //checks if the player is holding ctrl
            if(Input.GetKey(KeyCode.LeftControl)){
                //Selects this unit
                unitSelectManager.SelectUnit(this);
            //does this if the player isnt holding ctrl
            }else{
                //deselects all units
                unitSelectManager.DeselectAllUnits();
                //Selects this unit
                unitSelectManager.SelectUnit(this);
            }
            
        }
    }

    // Method for the unit to harvest ResourcesGame
    public void UnitHarvest()
    {
        // Check if there are ResourcesGame in the current tile
        if (ResourcesGameInTile != null)
        {
            // Creates a list to store ResourcesGame that need to be removed after harvesting
            List<ResourcesGame> ResourcesGameToRemove = new List<ResourcesGame>();

            // Loops through each resource in the tile
            foreach (ResourcesGame resource in ResourcesGameInTile)
            {
                // Loops through each resource the unit is capable of harvesting
                foreach (ResourcesGame resourceHarvest in unitToPlay.ResourcesToHarvest)
                {
                    // Check if the resource matches the unit's harvestable ResourcesGame
                    if (resource == resourceHarvest)
                    {
                        // Add the resource to the removal list to process it later
                        ResourcesGameToRemove.Add(resourceHarvest);

                        // Add the resource to the list of harvested ResourcesGame
                        ResourcesGameHarvested.Add(resourceHarvest);
                    }
                }
            }

            // Check if the unit is allowed to add ResourcesGame to the tile
            if (unitToPlay.CanAddResources == true)
            {
                // Loop through the ResourcesGame the unit is capable of generating
                foreach (ResourcesGame resource in unitToPlay.ResourcesToGenerate)
                {
                    // Add the generated resource to the current tile
                    currentTile.AddResources(resource);
                }
            }

            // Process the ResourcesGame that need to be removed after harvesting
            foreach (ResourcesGame resourceToRemove in ResourcesGameToRemove)
            {
                // Remove the resource from the current tile
                currentTile.RemoveResources(resourceToRemove);
            }
        }
        else
        {
            // Log a message if there are no ResourcesGame to harvest in the current tile
            Debug.Log("nothing to harvest here");
        }
    }
    // Method to move the unit to a specified tile
    public void MoveUnit(TileInfo destination)
    {
        // Gets the current tile that the unit is in
        TileInfo currentTile = this.GetComponentInParent<TileInfo>();
        // Gets the Vector3 position of the current tile
        Vector3 currentPos = new Vector3(currentTile.x, currentTile.y, 0.7f);
        // Gets the Vector3 position of the destination tile
        Vector3 destPos = new Vector3(destination.x, destination.y, 0.7f);
        // Calculate the distance from the current position to the destination
        float currentDistance = Vector3.Distance(currentPos, destPos);
        // Check if the unit is already at the destination
        if (currentPos == destPos)
        {
            // Log a message if the unit is already at the destination
            Debug.Log($"{unitToPlay.UnitName} is already at the destination.");
            return;
        }

        // Create a list to store possible movement directions
        List<Vector3> directions = new List<Vector3>();
        // Check if the unit has Von Neumann movement (cardinal directions only)
        if (unitToPlay.Movement == Unit.move.Vonneumann)
        {
            // Add cardinal movement directions to the list
            directions.AddRange(new[]
            {
                // Up
                new Vector3(0, 1, 0),  
                // Down
                new Vector3(0, -1, 0), 
                // Left
                new Vector3(-1, 0, 0), 
                // Right
                new Vector3(1, 0, 0)   
            });
        }
        // Check if the unit has Moore movement (cardinal and diagonal directions)
        else if (unitToPlay.Movement == Unit.move.Moore)
        {
            // Add cardinal and diagonal movement directions to the list
            directions.AddRange(new[]
            {
                // Up
                new Vector3(0, 1, 0),   
                // Down
                new Vector3(0, -1, 0),  
                // Left
                new Vector3(-1, 0, 0),  
                // Right
                new Vector3(1, 0, 0),   
                // Top Left
                new Vector3(-1, 1, 0),  
                // Top Right
                new Vector3(1, 1, 0),   
                // Bottom Left
                new Vector3(-1, -1, 0), 
                // Bottom Right
                new Vector3(1, -1, 0)   
            });
        }

        // Sort the possible directions by their proximity to the destination
        directions.Sort((a, b) =>
        {
            // Calculate the position for the current direction (a)
            Vector3 nextA = currentPos + a;
            // Calculate the position for the other direction (b)
            Vector3 nextB = currentPos + b;
            // Calculate the distance from the destination for direction (a)
            float distA = Vector3.Distance(nextA, destPos);
            // Calculate the distance from the destination for direction (b)
            float distB = Vector3.Distance(nextB, destPos);
            // Compare the distances to prioritize closer directions
            return distA.CompareTo(distB);
        });

        // Attempt to move the unit to the closest unoccupied tile
        foreach (Vector3 dir in directions)
        {
            // Calculate the new position for this direction
            Vector3 newPos = currentPos + dir;
            // Find the tile information for the new position
            TileInfo targetTileInfo = FindTileInfoAt(newPos);

            // Check if the tile is valid and unoccupied
            if (targetTileInfo != null && destination.hasChild == false)
            {
                // Calculate the distance from the new position to the destination
                float newDistance = Vector3.Distance(newPos, destPos);
                // Check if the new position is closer to the destination
                if (newDistance < currentDistance)
                {
                    // Set the unit's parent to the new tile
                    transform.SetParent(targetTileInfo.transform, false);

                    // Update the unit's world position for visual feedback
                    transform.position = new Vector3(newPos.x + 0.3f, newPos.y + 0.3f, 0.7f);

                    // Log the new position of the unit
                    Debug.Log($"{unitToPlay.UnitName} moved to ({newPos.x}, {newPos.y}).");
                    return;
                }
            }
        }

        // Log a message if the unit couldn't move and stayed in place
        Debug.Log($"{unitToPlay.UnitName} couldn't move and stayed at ({currentPos.x}, {currentPos.y}).");
    }

    private TileInfo FindTileInfoAt(Vector3 position)
    {
    // Loop through all tiles to find the one matching the position
    foreach (Transform child in parentOfTiles.transform)
    {
        TileInfo tileVisual = child.GetComponent<TileInfo>();
        if (tileVisual.x == (int)position.x && tileVisual.y == (int)position.y)
        {
            return tileVisual;
        }
    }
    return null;
    }

    public void UnitRemoveSelf(){
        Destroy(this.gameObject); 
    }

    //does this if the unit is selected
    public void OnSelected()
    {
        //sets isblinking to true
        isBlinking = true;
        //starts the blinking effect
        StartCoroutine(BlinkEffect());

    }

    //does this if the unit is deselected
    public void OnDeselected()
    {
        //stops the blinking effect
        StopCoroutine(BlinkEffect());
        //sets isblinking to false
        isBlinking = false;
        //sets the color of the object to is original color
        unitRenderer.material.color = originalColor;
    }

    //does a blink effect to the game object
    private IEnumerator BlinkEffect()
    {
        //checks if the tile is supposed to be blinking
        isBlinking = true;
        // Adjust blinking speed
        float blinkSpeed = 2f; 

        //does this while blinking is true
        while (isBlinking)
        {
            // Transitions to white
            float t = 0f;
            //does this while t is lower than one
            while (t < 1f)
            {
                //sees how fast should the t go to 1
                t += Time.deltaTime * blinkSpeed;
                unitRenderer.material.color = Color.Lerp(originalColor, Color.white, t);
                yield return null;
            }

            // Transition back to the original color
            t = 0f;
            while (t < 1f)
            {
                //sees how fast should the t go to 1
                t += Time.deltaTime * blinkSpeed;
                unitRenderer.material.color = Color.Lerp(Color.white, originalColor, t);
                yield return null;
            }
        }
    }

}
