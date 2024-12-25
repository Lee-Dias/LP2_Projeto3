using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Lands land;
    public List<Resources> resources;

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

    public void AddResource(Resources resource)
    {
        resources.Add(resource);
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
