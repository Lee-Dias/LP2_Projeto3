using System.IO;
using UnityEngine;
using UnityEditor;

public class Start : MonoBehaviour
{
    private string path{ get; set; }
    private string Map4xPath;

    [SerializeField]
    private GameMap gameMapFile;

    public void StartGame(){
        Map4xPath = OpenExplorer();
        if (!string.IsNullOrEmpty(Map4xPath))
        {
            // Proceed only if the file has the correct extension
            if (Map4xPath.EndsWith(".map4x"))
            {
                this.gameObject.SetActive(false);
                gameMapFile.GameStart(Map4xPath);
            }
            else
            {
                // Handle the case where the file is not a .map4x file
                Debug.LogError("Invalid file type selected. Only .map4x files are allowed.");
                // Optionally, show a message to the player
                Debug.Log("Please select a valid .map4x file.");
            }
        }
        else
        {
            // Handle case where the user cancels the file selection
            Debug.LogWarning("No file selected.");
        }
    }

    public string OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Select Map File", "", "map4x");
        return path;
    }
}
