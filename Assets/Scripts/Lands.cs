using UnityEngine;

[CreateAssetMenu(fileName = "Lands", menuName = "Scriptable Objects/Lands")]
public class Lands : ScriptableObject
{

    public string landName;
    public string landNameCode;

    public int baseCoin;

    public int baseFood;

    public GameObject landObject;
}
