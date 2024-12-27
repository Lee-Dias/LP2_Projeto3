using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public enum move{Vonneumann , Moore }

    [SerializeField]
    private string unitName;

    [SerializeField]
    private string unitNameCode; 

    [SerializeField]
    private List<Resources> resourcesToHarvest;

    [SerializeField]
    private bool canAddResources = false;   

    [SerializeField, ShowIf(nameof(canAddResources))]
    private List<Resources> resourcesToGenerate;

    [SerializeField]
    private move movement;

    [SerializeField]
    private GameObject unitImage;

    public string UnitName => unitName;
    public string UnitNameCode => unitNameCode;
    public List<Resources> ResourcesToHarvest => resourcesToHarvest;
    public bool CanAddResources => canAddResources;
    public List<Resources> ResourcesToGenerate => resourcesToGenerate;
    public move Movement => movement;
    public GameObject UnitImage => unitImage;
}
