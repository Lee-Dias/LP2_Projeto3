using UnityEngine;


[CreateAssetMenu(fileName = "Land", menuName = "Scriptable Objects/Land")]
public class Land : ScriptableObject
{
    // Name of the land type 
    [SerializeField]
    private string landName;
    // land name in the code
    [SerializeField]
    private string landNameCode;
    // The base amount of coins 
    [SerializeField]
    private int baseCoin;
    // The base amount of food
    [SerializeField] 
    private int baseFood;
    // The GameObject representing the visual appearance of this land 
    [SerializeField]
    private GameObject landObject;

    // Name of the land type 
    public string LandName => landName;
    // land name in the code
    public string LandNameCode => landNameCode;
    // land name in the code
    public int BaseCoin => baseCoin;
    // The base amount of food 
    public int BaseFood => baseFood;
    // The base amount of food 
    public GameObject LandObject => landObject;
}

