using UnityEngine;
using UnityEngine.UI;
using System;

public class WallGenerator : MonoBehaviour {

    #region Variables
    public int width;
    public int height;

    public GameObject wallUnit;
    public float XDimensionOfWallUnit = 1f;
    public float YDimensionOfWallUnit = 1f;
    public float ZDimensionOfWallUnit = 1f;

    public int[,] map; // Creates int array that can take two inputs as dimensions

    public bool wallExists;
    public WallUnitUIController wallUnitUIControllerScript;
    #endregion

    #region Unity Methods

    void Awake()
    {
        wallExists = false;
    }

    private void Start()
    {
        width = wallUnitUIControllerScript.width;
        height = wallUnitUIControllerScript.height;

        map = new int[width, height];

        FillWallArray();
    }

    // Fills wall value array with values from UI Controller.
    void FillWallArray()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = wallUnitUIControllerScript.grid[x, y];
            }
        }
    }

    // This just checks if a wall already exists or not, then just creates the wall if none already exists, or deletes the existing wall to 
    // create the new one if one already exists.
    public void CreateNewWallOrReplaceOldWall()
    {
        if (wallExists != true)
        {
            CreateWall();
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            wallExists = false;
            CreateWall();
        }

    }

    // Builds wall object out of WallUnitObjects by placing the WallUnitObjects in the designated values found in the array
    public void CreateWall()
    {
        Vector3 currentPosition;
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 0)
                    {
                        currentPosition = new Vector3(XDimensionOfWallUnit * (-width / 2 + x + 0.5f), 0, ZDimensionOfWallUnit * (-height / 2 + y + 0.5f));
                        GameObject newWallElement = Instantiate(wallUnit, currentPosition, Quaternion.identity);
                        newWallElement.transform.SetParent(gameObject.transform, false);
                    }
                }
            }
        }

        wallExists = true;
    }

    #endregion
}
