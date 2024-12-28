using UnityEngine;

public class TIlesChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject tilesparent;

    public void checkchilds(){
        foreach(Transform child in tilesparent.transform){
            TileInfo tilecode = child.GetComponent<TileInfo>();
            tilecode.checkChild();
        }
    }
}
