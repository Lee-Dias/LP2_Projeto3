using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour
{
    private int gameTurn = 1;

    private List<Resources> allResources = new List<Resources> ();
    [SerializeField]
    private List<Resources> allUniqueResources;
    [SerializeField]
    private TextMeshProUGUI turn;
    [SerializeField]
    private TextMeshProUGUI showResources;

    [SerializeField]
    private GameObject parentOfTiles;

    void Update(){
        turn.text = "turn: " + gameTurn;
        checkAllResourcesToGet();
    }

    public void Play(){
        gameTurn = gameTurn +1;
    }

    private void checkAllResourcesToGet(){
        showResources.text = "Total Resources: \n";
        allResources = new List<Resources> ();
        foreach(Transform child in parentOfTiles.transform){
            TileInfo tilecode = child.GetComponent<TileInfo>();
            foreach (Resources resource in tilecode.GetResources()){
                allResources.Add(resource);
            }
        }
        foreach(Resources resource in allUniqueResources){
            int i = 0;
            foreach(Resources resources in allResources){
                if(resource == resources){
                    i+=1;
                }
            }
            showResources.text = showResources.text + resource.resourceName + " x" + i + "\n"; 
        }
    }
}
