using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Scriptable Objects/Resources")]
public class Resources : ScriptableObject
{
    // Name of the resource
    public string resourceName;

    // Resource name in the code
    public string resourceNameCode;

    // Modifier to change the amount of coins on a land 
    public int coinModifier;

    // Modifier to change the amount of food on a land
    public int foodModifier;
}

