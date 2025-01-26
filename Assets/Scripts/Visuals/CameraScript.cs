using UnityEngine;
/// <summary>
/// This class handles the camera as its movement, zoom and its position to be 
/// centred on the map,
/// and what the initial size will be depending on the size of the map
/// </summary>
public class CameraScript : MonoBehaviour
{
// Reference to the game map for positioning the camera
    [SerializeField]
    private GameMap gameMap;

    // Speed of camera movement and zooming
    [SerializeField]
    private float moveSpeed = 10f;    

    // Zoom speed and boundaries
    [SerializeField]     
    private float zoomSpeed = 2f;   
       
    private float minZoom = 2f;        
    private float maxZoom = 50f;           

    private Vector3 dragOrigin;  

    // Camera reference
    private Camera cam;                    

    void Start()
    {
        // Get the main camera
        cam = Camera.main;  
    }

    // Set the initial camera position based on the game map size
    public void CameraPosition()
    {
        // Camera positioned at a calculated point based on map size
        Vector3 newPos = new Vector3((gameMap.mapXSize / 4), (gameMap.mapYSize / 2), 15);
        //making the camera be the position created
        this.transform.position = newPos;
        //make its size so it will always fit the map
        cam.orthographicSize = gameMap.mapXSize/2;
        maxZoom = gameMap.mapYSize;
    }

    void Update()
    {
        // Call zoom handling function
        HandleZoom();  
        // Call drag handling function
        HandleRightClickDrag();  
    }

    // Function to handle zoom using mouse scroll wheel
    void HandleZoom()
    {
        // Get scroll input from the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Change camera zoom based on scroll input, multiplied by zoom speed
            cam.orthographicSize -= scroll * zoomSpeed;

            // Clamp the zoom level to make sure it stays within min and max zoom bounds
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    // Function to move the camera using right mouse drag
    private void HandleRightClickDrag()
    {
        // Detect when the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))   
        {
            dragOrigin = Input.mousePosition;  // Store the current mouse position as the drag origin
        }

        // Detect if the right mouse button is being held down (dragging)
        if (Input.GetMouseButton(1))     
        {
            // Calculate the difference between the current mouse position and the drag origin
            Vector3 delta = Input.mousePosition - dragOrigin;

            // Create a movement vector where both X and Y axes are affected (horizontal and vertical movement)
            Vector3 move = new Vector3(delta.x, delta.y, 0) * moveSpeed * Time.deltaTime;

            // Move the camera based on the delta movement
            transform.Translate(-move, Space.World);  

            // Update the drag origin for the next frame
            dragOrigin = Input.mousePosition;  
        }
    }
}
