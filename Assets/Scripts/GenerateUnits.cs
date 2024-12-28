using JetBrains.Annotations;
using UnityEngine;

public class GenerateUnits : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentOfTiles;

    public void PlaceUnit(Unit unit){
        bool isRunning = true;
        while(isRunning){
            int n = 0;
            int rnd = Random.Range(1, ParentOfTiles.transform.childCount + 1);

            foreach (Transform child in ParentOfTiles.transform)
            {
                n += 1;
                if (child.GetComponentInChildren<Units>() == null){
                    if (n == rnd){
                        GameObject Unitobject;
                        Unitobject = Instantiate(unit.UnitImage , new Vector3(0.3f, 0.2f, 0.7f), Quaternion.identity);
                        // Set the parent of the tile to organize them in the scene
                        Unitobject.transform.SetParent(child.transform, false);
                        //Unitobject.transform.position = new Vector3(0, 0, 0.7f);
                        isRunning = false;
                        Units unitScript = Unitobject.GetComponent<Units>();
                        unitScript.unitToPlay = unit;   
                    }
                }
            }
            isRunning = false;
        }


    }
}
