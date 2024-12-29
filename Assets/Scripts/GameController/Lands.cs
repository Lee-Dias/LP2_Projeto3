using UnityEngine;


[CreateAssetMenu(fileName = "Lands", menuName = "Scriptable Objects/Lands")]
public class Lands : ScriptableObject
{
    // Name of the land type 
    public string landName;

    // land name in the code
    public string landNameCode;

    // The base amount of coins 
    public int baseCoin;

    // The base amount of food 
    public int baseFood;

    // The GameObject representing the visual appearance of this land 
    public GameObject landObject;
}

