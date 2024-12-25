using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Scriptable Objects/Resources")]
public class Resources : ScriptableObject
{

    public string resourceName;

    public string resourceNameCode;

    public int coinModifier;

    public int foodModifier;
}
