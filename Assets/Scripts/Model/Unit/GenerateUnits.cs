using UnityEngine;
/// <summary>
/// when the player generates a unit this classe sees what unit it recieves 
/// and generates a unit randomly on the map in a open tile.
/// </summary>
public class GenerateUnits : MonoBehaviour
{
    [SerializeField]
    //gets the gameobject with is parent of all tiles
    private GameObject ParentOfTiles;

    // Method to place a unit randomly on an empty tile
    public void PlaceUnit(Unit unit)
    {
        // Bool to keep the loop running until a valid placement is found
        bool isRunning = true;
        
        // Start a loop that will keep running until a unit is placed
        while (isRunning)
        {
            // Reset the counter for child tiles
            int n = 0;
            // Generate a random number between 1 and the number of child tiles
            int rnd = Random.Range(1, ParentOfTiles.transform.childCount + 1);

            // Loop through each child tile under the ParentOfTiles object
            foreach (Transform child in ParentOfTiles.transform)
            {
                // Increment the counter for each tile
                n += 1;

                // Check if the current tile does not already contain a unit
                if (child.GetComponentInChildren<Units>() == null)
                {
                    // If this is the randomly selected tile, place the unit
                    if (n == rnd)
                    {
                        // Instantiate the unit's object at a specified position
                        GameObject Unitobject;
                        Unitobject = Instantiate(unit.UnitImage, 
                        new Vector3(0.3f, 0.2f, 0.7f), Quaternion.identity);
                        
                        // Set the parent of the unit ~
                        //to the selected tile's transform
                        Unitobject.transform.SetParent(child.transform, false);
                        
                        // Disable further iteration 
                        //(unit is placed successfully)
                        isRunning = false;

                        // Get the Units component of the instantiated unit
                        Units unitScript = Unitobject.GetComponent<Units>();
                        // Assign the unit to the unitToPlay variable
                        unitScript.unitToPlay = unit;
                    }
                }
            }

            // Exit the loop after one full iteration 
            //(as isRunning is set to false)
            isRunning = false;
        }
    }

}
