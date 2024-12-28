using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Selected;
    [SerializeField]
    private UnitSelectManager unitSelectManager;
    [SerializeField]
    private GameObject unitsArea ;
    [SerializeField]
    private List<Resources> allUniqueResources;

    private void Update()
    {
        UnitInfoSlected();
        UnitsSelected();
    }
    private void UnitInfoSlected() {
        if (unitSelectManager.selectedUnits.Count == 1){
            Selected.text = "Unit: " + unitSelectManager.selectedUnits.First().unitToPlay.UnitName + "\n" + " Total resources Harvested: " + "\n";
            foreach(Resources resource in unitSelectManager.selectedUnits.First().unitToPlay.ResourcesToHarvest){
                Selected.text += resource.resourceName;
                int i = 0;
                foreach (Resources resources in unitSelectManager.selectedUnits.First().resourcesHarvested){
                    i += 1;
                }
                Selected.text += ": " + i + "\n";
            }
        }else{
            Selected.text = "Units Selected: " + unitSelectManager.selectedUnits.Count() + "\n" + " Total resources Harvested: " + "\n";
            foreach(Resources resources in allUniqueResources){
                int i = 0;
                foreach(Units unit in unitSelectManager.selectedUnits){
                    foreach(Resources resource in unit.resourcesHarvested){
                        i += 1;

                    }
                }
                Selected.text += resources.resourceName + " x"+ i +"\n";
            }   
        }
    }

    private void UnitsSelected(){
        if(unitSelectManager.selectedUnits.Count() == 0){
            unitsArea.SetActive(false);
        }else{
            unitsArea.SetActive(true);
        } 
    }
}
