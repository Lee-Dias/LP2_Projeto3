using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;

public class Units : MonoBehaviour
{
    //Variable for unit to know if he is a miner, butcher etc...
    public Unit unitToPlay;
    //Variable for currenttile that the unit is in
    private TileInfo currentTile;
    //Variable for the resources in the tile that the unit is in
    private List<Resources> resourcesInTile;
    //Variable for the resources that the unit has harvested
    public List<Resources> resourcesHarvested {get; private set;} = new List<Resources>();
    //Variable for the UnitSelectManager
    private UnitSelectManager unitSelectManager;
    // For visual feedback on the blink
    private Renderer unitRenderer; 
    // Stores the original color of the unit
    private Color originalColor; 
    // Tracks whether the unit is blinking
    private bool isBlinking = false; 

    void Start()
    {
        unitRenderer = GetComponent<Renderer>();
        originalColor = unitRenderer.material.color;
        EachMove();
    }
    public void EachMove(){
        currentTile = this.GetComponentInParent<TileInfo>();
        resourcesInTile = currentTile.GetResources();
    }
    private void OnMouseDown()
    {
        // gets the unitSelectManager
        unitSelectManager = FindFirstObjectByType<UnitSelectManager>();
        // Check if the unit is already selected
        if (unitSelectManager.GetSelectedUnits().Contains(this))
        {
            unitSelectManager.DeselectUnit(this);
        }
        else
        {
            unitSelectManager.SelectUnit(this);
        }
    }

    public void UnitHarvest(){

        
        if(resourcesInTile != null){
            // Create a list to store resources to be removed
            List<Resources> resourcesToRemove = new List<Resources>();
            foreach(Resources resource in resourcesInTile){
                foreach(Resources resourceHarvest in unitToPlay.ResourcesToHarvest){
                    if(resource == resourceHarvest){
                        resourcesToRemove.Add(resourceHarvest);
                        resourcesHarvested.Add(resourceHarvest);
                    }
                }
                }
                if (unitToPlay.CanAddResources == true){
                    foreach (Resources resource in unitToPlay.ResourcesToGenerate){
                        currentTile.AddResources(resource);
                    }
                } 
                // gets the resources to remove after iteration
                foreach (Resources resourceToRemove in resourcesToRemove)
                {
                    // Removes the resources after iteration cause if its while c# doesnt allow it
                    currentTile.RemoveResources(resourceToRemove);
                }        
        }else{
            Debug.Log("nothing to harvest here");
        }

    }
    public void MoveUnit(TileInfo destination)
    {
        // gets the CurrentTile that unit is in
        TileInfo currentTile = this.GetComponentInParent<TileInfo>();
        // gets the vector 3 of the currentposition
        Vector3 currentPos = new Vector3(currentTile.x, currentTile.y, 0.7f);
        // gets the vector 3 of the destination
        Vector3 destPos = new Vector3(destination.x, destination.y, 0.7f);
        // Calculate the distance from the current tile to the destination
        float currentDistance = Vector3.Distance(currentPos, destPos);
        // If already at the closest position, do nothing
        if (currentPos == destPos)
        {
            Debug.Log($"{unitToPlay.UnitName} is already at the destination.");
            return;
        }

        // Determine possible movement directions
        List<Vector3> directions = new List<Vector3>();
        if (unitToPlay.Movement == Unit.move.Vonneumann)
        {
            directions.AddRange(new[]
            {
                new Vector3(0, 1, 0),  // Up
                new Vector3(0, -1, 0), // Down
                new Vector3(-1, 0, 0), // Left
                new Vector3(1, 0, 0)   // Right
            });
        }
        else if (unitToPlay.Movement == Unit.move.Moore)
        {
            directions.AddRange(new[]
            {
                new Vector3(0, 1, 0),   // Up
                new Vector3(0, -1, 0),  // Down
                new Vector3(-1, 0, 0),  // Left
                new Vector3(1, 0, 0),   // Right
                new Vector3(-1, 1, 0),  // Top Left
                new Vector3(1, 1, 0),   // Top Right
                new Vector3(-1, -1, 0), // Bottom Left
                new Vector3(1, -1, 0)   // Bottom Right
            });
        }

        // Sort directions by proximity to destination
        directions.Sort((a, b) =>
        {
            Vector3 nextA = currentPos + a;
            Vector3 nextB = currentPos + b;
            float distA = Vector3.Distance(nextA, destPos);
            float distB = Vector3.Distance(nextB, destPos);
            return distA.CompareTo(distB);
        });

        // Attempt to move to the closest unoccupied tile
        foreach (Vector3 dir in directions)
        {
            Vector3 newPos = currentPos + dir;
            TileInfo targetTileInfo = FindTileInfoAt(newPos);
            
            if (targetTileInfo != null && destination.hasChild == false)
            {
                float newDistance = Vector3.Distance(newPos, destPos);
                if (newDistance < currentDistance){
                    // Update parent to the new tile
                    transform.SetParent(targetTileInfo.transform, false);

                    // Update position in world space (optional, if using transforms)
                    transform.position = new Vector3(newPos.x + 0.3f,newPos.y + 0.3f, 0.7f); 

                    Debug.Log($"{unitToPlay.UnitName} moved to ({newPos.x}, {newPos.y}).");
                    return;
                }

            }
        }

        // If no valid moves, stay in place
        Debug.Log($"{unitToPlay.UnitName} couldn't move and stayed at ({currentPos.x}, {currentPos.y}).");
    }

    private TileInfo FindTileInfoAt(Vector3 position)
    {
    // Loop through all tiles to find the one matching the position
    foreach (TileInfo tileVisual in FindObjectsOfType<TileInfo>())
    {
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

    public void OnSelected()
    {
        isBlinking = true;
        StartCoroutine(BlinkEffect());

    }

    public void OnDeselected()
    {
        StopCoroutine(BlinkEffect());
        isBlinking = false;
        unitRenderer.material.color = originalColor;
    }

    private IEnumerator BlinkEffect()
    {
        //checks if the tile is supposed to be blinking
        isBlinking = true;
        // Adjust blinking speed
        float blinkSpeed = 2f; 


        while (isBlinking)
        {
            // Transition to white
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * blinkSpeed;
                unitRenderer.material.color = Color.Lerp(originalColor, Color.white, t);
                yield return null;
            }

            // Transition back to the original color
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * blinkSpeed;
                unitRenderer.material.color = Color.Lerp(Color.white, originalColor, t);
                yield return null;
            }
        }
    }

}
