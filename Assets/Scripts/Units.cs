using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Units : MonoBehaviour
{
    private Unit unitToPay;

    private TileVisuals currentTile;

    private List<Resources> resourcesInTile;

    private List<Resources> resourcesHarvested;

    private void EachTurn(){
        currentTile = this.GetComponentInParent<TileVisuals>();
        resourcesInTile = currentTile.GetResources();
    }

    private void UnitHarvest(){
        foreach(Resources resource in resourcesInTile){
            foreach(Resources resourceHarvest in unitToPay.ResourcesToHarvest){
                if(resource == resourceHarvest){
                    currentTile.RemoveResources(resourceHarvest);
                    resourcesHarvested.Add(resourceHarvest);
                }
            }
            }
        if (unitToPay.CanAddResources == true){
            foreach (Resources resource in unitToPay.ResourcesToGenerate){
                currentTile.AddResources(resource);
            }
        }
    }
    private void MoveUnit(Tile destination){


    }

}
