using System.Linq;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Selected;
    [SerializeField]
    private UnitSelectManager unitSelectManager;

    void Update()
    {
        Selected.text = "Units Selected: " + unitSelectManager.selectedUnits.Count();
    }
}
