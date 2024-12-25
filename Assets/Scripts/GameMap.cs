using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    private string[] allInMap;

    private int mapXSize;
    private int mapYSize;
    [SerializeField]
    private List<Resources> allResources;
    [SerializeField]
    private List<Lands> allLands;

    [SerializeField]
    private GameObject tilesParent;

    public void GameStart(string mapPath)
    {
        allInMap = File.ReadAllLines(mapPath);
        GenerateMap(allInMap);
    }

    // Update is called once per frame
    private void GenerateMap(string[] mapInfo)
    {
        string[] mapDimensions = mapInfo[0].Split();
        mapXSize = Convert.ToInt32(mapDimensions[0]);
        mapYSize = Convert.ToInt32(mapDimensions[1]);
        CreateMap();
    }
    private void CreateMap(){
        string[] mapInfo = allInMap;
        string line;
        string[] parts;
        int x = 1; 
        int y = 1; 
        for (int i = 1; i < mapInfo.Length; i++){
            line = mapInfo[i];
            parts = mapInfo[i].Split();
            for (int k = 0; k < parts.Length; k++){
                if (parts[k]== "#"){
                    break;
                }
                foreach (Lands land in allLands){
                    if (parts[k] == land.landNameCode)
                    {
                        // Instantiate the land object at the correct position
                        GameObject tileObject = Instantiate(land.landObject, new Vector3(x, y, 0), Quaternion.identity);

                        // Set the parent of the tile to organize them in the scene
                        tileObject.transform.SetParent(tilesParent.transform);

                        // Optionally, store the position in the tile (if you need to reference it later)
                        TileVisuals tilePosition = tileObject.AddComponent<TileVisuals>();
                        tilePosition.x = x;
                        tilePosition.y = y;

                        // Optionally, you can set the name of the tile based on its position for easy identification
                        tileObject.name = $"{land.landName} ({x}, {y})";

                        if(x == mapXSize){
                            x = 1;
                            y += 1;
                        }else{
                            x += 1;
                        }
                    }
                }
            }     
        }
    }
}
