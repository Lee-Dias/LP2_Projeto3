using UnityEngine;
using UnityEditor;

public class Start : MonoBehaviour
{
    // Property to hold the file path selected by the user
    private string path { get; set; }

    // Variable to store the path for a specific .map4x file
    private string Map4xPath;

    // Reference to the GameMap ScriptableObject to start the game
    [SerializeField]
    private GameMap gameMapFile;
    [SerializeField]
    private CameraScript cameraScript;

    // Method to start the game by selecting a .map4x file
    public void StartGame()
    {
        // Open the file explorer and get the path of the selected file
        Map4xPath = OpenExplorer();
        
        // Proceed only if a valid file path was selected
        if (!string.IsNullOrEmpty(Map4xPath))
        {
            // Check if the selected file has the correct .map4x extension
            if (Map4xPath.EndsWith(".map4x"))
            {
                // Hide the current game object after a valid file is selected
                this.gameObject.SetActive(false);
                // Call the GameStart method from GameMap and pass the selected file path
                gameMapFile.GameStart(Map4xPath);
                cameraScript.CameraPosition();

            }
            else
            {
                // Handle if the selected file isn't a .map4x file
                Debug.LogError("Invalid file type selected. Only .map4x files are allowed.");
                // Tell the user to select a valid file
                Debug.Log("Please select a valid .map4x file.");
            }
        }
        else
        {
            // Handle if no file was selected
            Debug.LogWarning("No file selected.");
        }
    }

    // Opens the file explorer and returns the selected files path
    public string OpenExplorer()
    {
        // Open the file explorer to select a .map4x file and return the path
        path = EditorUtility.OpenFilePanel("Select Map File", "", "map4x");
        return path;
    }
}

