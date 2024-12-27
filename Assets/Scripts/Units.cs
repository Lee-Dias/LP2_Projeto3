using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Units : MonoBehaviour
{
    [SerializeField]
    private string unitName; 

    [SerializeField]
    private List<Resources> resourcesToHarvest;

    [SerializeField]
    private bool canAddResources = false;

    [SerializeField, ShowIf(nameof(canAddResources))]
    private List<Resources> resourcesToGenerate;

    private TileVisuals currentTile;

    private List<Resources> resourcesInTile;

    private void ChangedTurn(){
        currentTile = this.GetComponentInParent<TileVisuals>();
        resourcesInTile = currentTile.GetResources();
    }

    private void unitHarvest(){
        foreach(Resources resource in resourcesInTile){
            foreach(Resources resourceHarvest in resourcesToHarvest){
                if(resource == resourceHarvest){
                    currentTile.RemoveResources(resourceHarvest);
                }
            }
        }
        if (canAddResources == true){
            foreach (Resources resource in resourcesToGenerate){
                currentTile.AddResources(resource);
            }
        }
    }

}
