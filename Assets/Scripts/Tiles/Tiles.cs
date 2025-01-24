using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile
{
    private Lands land; 
    // List to store resources present on the tile
    public List<ResourcesGame> resources { get; private set; } 

    // Constructor to initialize the Tile with a specific land type
    public Tile(Lands land)
    {
        // Set the land type for this tile
        this.land = land; 
        // Initialize the list of resources as empty
        this.resources = new List<ResourcesGame>(); 
    }

    // Method to calculate the total number of coins available on the tile
    public int CalculateCoins()
    {
        // Start with the base number of coins defined by the land
        int totalCoin = land.BaseCoin;
        // Iterate through all resources on the tile and add their coin modifier
        foreach (var resource in resources)
        {
            totalCoin += resource.CoinModifier;
        }
        // Return the total number of coins available on the tile
        return totalCoin;
    }

    // Method to calculate the total amount of food available on the tile
    public int CalculateFood()
    {
        // Start with the base amount of food defined by the land
        int totalFood = land.BaseFood;
        // Iterate through all resources on the tile and add their food modifier
        foreach (var resource in resources)
        {
            totalFood += resource.FoodModifier;
        }
        // Return the total amount of food available on the tile
        return totalFood;
    }

    // Method to add a resource to the tile if it is not already present
    public void AddResource(ResourcesGame resourceToAdd, GameObject crr)
    { 
        // Check if the resource is already present in the list
        // Check if the resource is already present in the resources list
        foreach (ResourcesGame resource in resources)
        {
            if (resourceToAdd == resource)
            {
                // If the resource is already on the tile, exit the method without adding it
                return;
            }
        }
        // If the resource is not already on the tile, add it to the resources list
        resources.Add(resourceToAdd);
        MakeResourcesImages(crr);


    }

    private void MakeResourcesImages(GameObject current) 
    {
        foreach (Transform child in current.transform)
        {
            if(child.gameObject.layer == 3)
                UnityEngine.Object.Destroy(child.gameObject);
        }
        float offsetX = -0.35f; // Horizontal spacing between resource images
        float offsetY = -0.35f; // Vertical offset
        float positionx = 0;
        int count = 0;
        foreach (ResourcesGame resource in resources)
        {
            // Instantiate the resource's GameObject
            GameObject resourceImg = UnityEngine.Object.Instantiate(resource.Img, new Vector3(0, 0, 0.51f), Quaternion.identity);
            resourceImg.layer = 3;
            resourceImg.transform.SetParent(current.transform);

            Vector3 localPosition = new Vector3( positionx + offsetX, offsetY, 1);

            // Set the position relative to the tile
            resourceImg.transform.localPosition = localPosition;
            positionx += 0.2f;
            count += 1;
            if(count > 5 ){
                count = 0;
                positionx = 0;
                offsetY = 0.2f;
            }
        }

    }

    // Method to remove a resource from the tile
    public void RemoveResource(ResourcesGame resourceToRemove, GameObject crr)
    {
        // Remove the specified resource from the resources list
        resources.Remove(resourceToRemove);
        MakeResourcesImages(crr);
    }

    // Method to get the name of the land and a list of resources on the tile as a formatted string
    public string GetTileNameAndResources()
    {
        // Start the name with the land name
        string name = "Land: " + land.LandName;
        // If there are resources on the tile, append their names to the tile's name
        if (resources.Count > 0)
        {
            // Convert each resource to its name and sort them alphabetically
            var resource = resources.ConvertAll(r => r.ResourceName);
            resource.Sort();
            // Join the sorted resource names into a comma-separated string
            name += " Resources: " + string.Join(", ", resource);
        }
        // Return the formatted name of the tile with resources
        return name;
    }

}
