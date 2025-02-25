using UnityEngine;

/// <summary>
/// this class handles each resource info as the name, the amount of coins it 
/// haves, the amount of food it haves and the image of the resource.
/// </summary>
[CreateAssetMenu(fileName = "ResourceGame",
 menuName = "Scriptable Objects/ResourceGame")]
public class ResourceGame : ScriptableObject
{
    // Name of the resource
    [SerializeField]
    private string resourceName;
    // Resource name in the code
    [SerializeField]
    private string resourceNameCode;
    // Modifier to change the amount of coins on a land 
    [SerializeField]
    private int coinModifier;
    // Modifier to change the amount of food on a land
    [SerializeField]
    private int foodModifier;
    // Image of the Resource;
    [SerializeField]
    private GameObject img;

    public string ResourceName => resourceName;
    public string ResourceNameCode => resourceNameCode;
    public int CoinModifier => coinModifier;
    public int FoodModifier => foodModifier;
    public GameObject Img => img;

    
}
