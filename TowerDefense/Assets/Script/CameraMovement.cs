using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSPeed;
    [SerializeField]
    private float panBorderThickness;

    Vector3 cameraPos;
    Vector3 previousCameraPos;

    private GameObject LevelManager;
    [SerializeField]
    private GameObject BorderTiles;
    private Vector2 UpperLeftBorderLimit;
    private Vector2 BottomRightBorderLimit;

    private float tileSize;

    private bool checklimits;

    private float horizontalSize;
    private float verticalSize;

    private Vector2 cameraUpperLeftBorder;
    private Vector2 cameraBottomRightBorder;
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
        tileSize =LevelManager.GetComponent<LevelManager>().TileSize;

        checklimits = false;

        verticalSize = Camera.main.orthographicSize;
        horizontalSize = (verticalSize * Screen.width / Screen.height);

    }

    // Update is called once per frame
    void Update()
    {

        if (checklimits == false)
        {
            UpperLeftBorderLimit = BorderTiles.transform.GetChild(0).gameObject.transform.position;
            UpperLeftBorderLimit.x -= tileSize / 2;
            UpperLeftBorderLimit.y += tileSize / 2;

            BottomRightBorderLimit = BorderTiles.transform.GetChild(BorderTiles.transform.childCount - 1).gameObject.transform.position;
            BottomRightBorderLimit.x += tileSize / 2;
            BottomRightBorderLimit.y -= tileSize / 2;

            checklimits = true;
        }

        cameraPos = transform.position;
        previousCameraPos = cameraPos;
        
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            cameraPos.y += cameraSPeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            cameraPos.x += cameraSPeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            cameraPos.x -= cameraSPeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            cameraPos.y -= cameraSPeed * Time.deltaTime;
        }

        UpdateCameraBorderLimit();

        if (!CameraIsWithinBorderLimits())
        {
            cameraPos = previousCameraPos;
        }
           
        transform.position = cameraPos;
    }

    void UpdateCameraBorderLimit()
    {
        cameraUpperLeftBorder = cameraPos;
        cameraUpperLeftBorder.x -= horizontalSize;
        cameraUpperLeftBorder.y += verticalSize;

        cameraBottomRightBorder = cameraPos;
        cameraBottomRightBorder.x += horizontalSize;
        cameraBottomRightBorder.y -= verticalSize;
    }

    bool CameraIsWithinBorderLimits()
    {
        if (UpperLeftBorderLimit.x < cameraUpperLeftBorder.x && cameraBottomRightBorder.x < BottomRightBorderLimit.x)
        {
            if(BottomRightBorderLimit.y< cameraBottomRightBorder .y && cameraUpperLeftBorder.y< UpperLeftBorderLimit.y)
            {
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }
}
