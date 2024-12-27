using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private Lands land;
    public List<Resources> resources{get; private set;}

    public Tile(Lands land)
    {
        this.land = land;
        this.resources = new List<Resources>();
    }

    public int CalculateCoins()
    {
        int totalCoin = land.baseCoin;
        foreach (var resource in resources)
        {
            totalCoin += resource.coinModifier;
        }
        return totalCoin;
    }

    public int CalculateFood()
    {
        int totalFood = land.baseFood;
        foreach (var resource in resources)
        {
            totalFood += resource.foodModifier;
        }
        return totalFood;
    }

    public void AddResource(Resources resourceToAdd)
    {
        foreach(Resources resource in resources){
            if(resourceToAdd == resource){
                return;
            }
        }
        resources.Add(resourceToAdd);
    }

    public void RemoveResource(Resources resourceToRemove)
    {
        resources.Remove(resourceToRemove);
    }

    public string GetTileNameAndResources()
    {
        string name = "Land: " + land.landName;

        if (resources.Count > 0)
        {
            var resource = resources.ConvertAll(r => r.resourceName);
            resource.Sort();
            name += " Resources: " + string.Join(", ", resource);
        }

        return name;
    }
}
