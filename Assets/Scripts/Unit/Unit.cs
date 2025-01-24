using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    // Enum to define movement types for the unit
    public enum move { Vonneumann, Moore }

    // Name of the unit 
    [SerializeField] 
    private string unitName;  
    // Name of the unit in the code 
    [SerializeField]
    private string unitNameCode;  
    // List of resources the unit can harvest (e.g., food, wood)
    [SerializeField]
    private List<ResourcesGame> resourcesToHarvest;  
    // Bool to determine if the unit can add resources to the game world 
    [SerializeField]
    private bool canAddResources = false;  
    // List of resources the unit can generate 
    [SerializeField, ShowIf(nameof(canAddResources))] 
    private List<ResourcesGame> resourcesToGenerate;  
    // Movement pattern for the unit
    [SerializeField] 
    private move movement;  
    // GameObject representing the unit visually in the game
    [SerializeField] 
    private GameObject unitImage;  

    // Read-only properties to expose the private fields to other classes or scripts

    // Returns the units name
    public string UnitName => unitName;  
    // Returns the units name in the code
    public string UnitNameCode => unitNameCode;  
    // Returns the list of resources the unit can harvest
    public List<ResourcesGame> ResourcesToHarvest => resourcesToHarvest;  
    // Returns whether the unit can generate resources
    public bool CanAddResources => canAddResources;  
    // Returns the list of resources the unit can generate
    public List<ResourcesGame> ResourcesToGenerate => resourcesToGenerate;  
    // Returns the movement type of the unit
    public move Movement => movement;  
    // Returns the visual representation of the unit
    public GameObject UnitImage => unitImage;  

}
