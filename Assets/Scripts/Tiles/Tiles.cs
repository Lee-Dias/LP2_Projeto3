using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private Lands land; 
    // List to store resources present on the tile
    public List<Resources> resources { get; private set; } 

    // Constructor to initialize the Tile with a specific land type
    public Tile(Lands land)
    {
        // Set the land type for this tile
        this.land = land; 
        // Initialize the list of resources as empty
        this.resources = new List<Resources>(); 
    }

    // Method to calculate the total number of coins available on the tile
    public int CalculateCoins()
    {
        // Start with the base number of coins defined by the land
        int totalCoin = land.baseCoin;
        // Iterate through all resources on the tile and add their coin modifier
        foreach (var resource in resources)
        {
            totalCoin += resource.coinModifier;
        }
        // Return the total number of coins available on the tile
        return totalCoin;
    }

    // Method to calculate the total amount of food available on the tile
    public int CalculateFood()
    {
        // Start with the base amount of food defined by the land
        int totalFood = land.baseFood;
        // Iterate through all resources on the tile and add their food modifier
        foreach (var resource in resources)
        {
            totalFood += resource.foodModifier;
        }
        // Return the total amount of food available on the tile
        return totalFood;
    }

    // Method to add a resource to the tile if it is not already present
    public void AddResource(Resources resourceToAdd)
    {
        // Check if the resource is already present in the resources list
        foreach (Resources resource in resources)
        {
            if (resourceToAdd == resource)
            {
                // If the resource is already on the tile, exit the method without adding it
                return;
            }
        }
        // If the resource is not already on the tile, add it to the resources list
        resources.Add(resourceToAdd);
    }

    // Method to remove a resource from the tile
    public void RemoveResource(Resources resourceToRemove)
    {
        // Remove the specified resource from the resources list
        resources.Remove(resourceToRemove);
    }

    // Method to get the name of the land and a list of resources on the tile as a formatted string
    public string GetTileNameAndResources()
    {
        // Start the name with the land name
        string name = "Land: " + land.landName;
        // If there are resources on the tile, append their names to the tile's name
        if (resources.Count > 0)
        {
            // Convert each resource to its name and sort them alphabetically
            var resource = resources.ConvertAll(r => r.resourceName);
            resource.Sort();
            // Join the sorted resource names into a comma-separated string
            name += " Resources: " + string.Join(", ", resource);
        }
        // Return the formatted name of the tile with resources
        return name;
    }

}
