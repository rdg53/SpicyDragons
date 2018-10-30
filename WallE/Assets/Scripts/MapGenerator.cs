using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour {

    #region Variables
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map; // Creates int array that can take two inputs as dimensions
	#endregion
	
	#region Unity Methods

	void Start ()
	{
        GenerateMap();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        // Generate array with proper dimensions
        map = new int[width, height];
        RandomFillMap();

        // Calls SmoothMap method number of times as determined by for loop
        // Allows for general control of how much "smoothing" to apply
        // Runs after cells have been applied random initial values
        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

    }
	
    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            // Allows for random seed choice when this bool is selected
            seed = Time.time.ToString();
        }

        // Not sure exactly how this line works with System.Random
        System.Random psuedoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                // ? A : B --- if else --- if true, A --- else, B
                // This line lets the randomFillPercent determine the percent chance of randomly assigning a value of 1 to a map location on the initial random pass
                map[x, y] = (psuedoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
            }
        }
    }

    // Smoothing function to apply to control influenece of cells on neighboring cells
    // This is a core set of "rules" for determining how generation will work, can be edited for different results.
    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborWallTiles = GetSurroundingWallCount(x, y);

                if (neighborWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighborWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    // Determines how many walls are arround the current grid location
    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        // Looks at 3x3 grid surrounding location (looks at all adjacent cells)
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                // Makes sure cell is safely within map. Keeps from checking outside of map.
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    // Does not need to check itself
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        // map[,] values are initially only 0 or 1, so this basically adds 1 if cell is "active" and 0 if cell is "inactive"
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else
                {
                    wallCount++;
                }
                
            }
        }

        return wallCount;
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // If map location has a value of 1, set the color to black. Else, set the color to white.
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    // Position this way centers everything
                    Vector3 pos = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }

    #endregion
}
