using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSPeed;
    [SerializeField]
    private float panBorderThickness;

    Vector3 cameraPos;
    
    private GameObject LevelManager;

    private float tileSize;
    private float mapSizeY;

    private Vector2 borderBoundaryPositionX;
    private Vector2 borderBoundaryPositionY;

    [SerializeField]
    private Vector2 panLimit;

    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
        tileSize=LevelManager.GetComponent<LevelManager>().TileSize;
        mapSizeY= LevelManager.GetComponent<LevelManager>().mapYSize;

        Vector2 borderBoundaryVector = new Vector2(-tileSize*2 - tileSize/2,0);
        Vector2 mapYSizeVector = new Vector2(0, mapSizeY);
        Vector2 borderBoundaryYVector = Quaternion.Euler(0, 0, -90) * borderBoundaryVector;

        borderBoundaryPositionX = Vector2.zero + borderBoundaryVector;
        borderBoundaryPositionY = mapYSizeVector + borderBoundaryYVector;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = transform.position;
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            cameraPos.y += cameraSPeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.height - panBorderThickness)
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

        cameraPos.x = Mathf.Clamp(cameraPos.x,-panLimit.x,panLimit.x);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -panLimit.y, panLimit.y);
        transform.position = cameraPos;

        //if(Input.mousePosition.x)
    }
}
