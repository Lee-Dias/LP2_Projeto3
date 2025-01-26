using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    // Array to store all the map data loaded from a file
    private string[] allInMap;
    // Reference to the TileInfo script used for managing individual tile behavior
    private TileInfo tileScript;
    // Integer to store the horizontal size of the map
    public int mapXSize{get; private set;}
    // Integer to store the vertical size of the map
    public int mapYSize{get; private set;}
    // List to store all resource types available in the game
    [SerializeField]
    private List<ResourceGame> allResources;
    // List to store all land types available in the game
    [SerializeField]
    private List<Land> allLands;
    // Reference to the GameObject that will act as the parent container for all tiles
    [SerializeField]
    private GameObject tilesParent;
    // Reference to the TextMeshProUGUI element used to display error messages
    [SerializeField]
    private TextMeshProUGUI error;


    // Method to start the game and initialize the map
    public void GameStart(string mapPath)
    {
        // Read all lines from the specified map file and store them in an array
        allInMap = File.ReadAllLines(mapPath);
        // Call the GenerateMap method to create the map using the data from the file
        GenerateMap(allInMap);
    }

    // Method to generate a map based on the provided map information
    private void GenerateMap(string[] mapInfo)
    {
        // Split the first element of the map information to get the dimensions
        string[] mapDimensions = mapInfo[0].Split();       
        // Convert the first value to an integer and assign it to the map's X size
        mapXSize = Convert.ToInt32(mapDimensions[0]);      
        // Convert the second value to an integer and assign it to the map's Y size
        mapYSize = Convert.ToInt32(mapDimensions[1]);  
        // Call the method to create the map based on the dimensions
        CreateMap();
    }

    // Method to create the map by instantiating tiles and assigning resources
    private void CreateMap()
    {
        // Store the map data loaded from the file
        string[] mapInfo = allInMap;
        // Declare variables for each line in the map and for parts of each line
        string line;
        //string to get all parts from the line
        string[] parts;
        // Initialize coordinates for the tiles (x and y)
        int x = 1;
        int y = 1;

        // Loop through each line in the map data
        for (int i = 1; i < mapInfo.Length; i++)
        {
            // Get the current line of the map data
            line = mapInfo[i];
            // Split the line into individual parts (tokens) based on spaces
            parts = mapInfo[i].Split();

            // Loop through each part in the current line
            for (int k = 0; k < parts.Length; k++)
            {
                // Check if something was found for the current part
                bool gotSomething = false;

                // If the current part is "#" break out of this line
                if (parts[k] == "#")
                {
                    break;
                }

                // Loop through all land types to see if the part matches any land name code
                foreach (Land land in allLands)
                {
                    // Check if the current part matches the land's name code
                    if (parts[k] == land.LandNameCode)
                    {
                        // Mark that we found a valid land type for this part
                        gotSomething = true;

                        // Instantiate the land object at the correct position (x, y)
                        GameObject tileObject = Instantiate
                        (land.LandObject, new Vector3(x, y, 0),
                        Quaternion.identity);

                        // Set the parent of the tile to organize them in the scene hierarchy
                        tileObject.transform.SetParent(tilesParent.transform);

                        // Get the TileInfo component from the instantiated tile object
                        tileScript = tileObject.GetComponent<TileInfo>();
                        // Initialize the tile with the corresponding land type
                        tileScript.Initialize(land);
                        // Set the tile's coordinates (x, y) for later reference

                        tileScript.Changexy(x,y);

                        // Optionally, set the name of the tile based on its position for easy debugging
                        tileObject.name = $"{land.LandName} ({x}, {y})";

                        // Update the coordinates: if we reach the end of the row, move to the next row
                        if (x == mapXSize)
                        {
                            x = 1;
                            y += 1;
                        }
                        else
                        {
                            x += 1;
                        }
                    }
                }

                // Loop through all resources to see if the part matches any resource code
                foreach (ResourceGame resources in allResources)
                {
                    // Check if the current part matches the resource's name code
                    if (parts[k] == resources.ResourceNameCode)
                    {
                        // Mark that we found a valid resource for this part
                        gotSomething = true;
                        // Add the resource to the current tile
                        tileScript.AddResources(resources);
                    }
                }

                // If nothing matched (neither land nor resource), display an error message
                if (gotSomething == false)
                {
                    error.text = parts[k] + " is unknown, please create it.";
                }
            }
        }
    }

}
