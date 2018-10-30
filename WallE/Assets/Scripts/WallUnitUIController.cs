using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallUnitUIController : MonoBehaviour {

    #region Variables

    public int width = 4;
    public int height = 4;

    public int[,] grid;

    public float maxWidthUIGrid = 2;
    public float maxHeightUIGrid = 2;
    public Image wallUnitUI;

    public Texture2D imageCutout;

    public WallGenerator wallGenerator;
    #endregion

    #region Unity Methods

    private void Start()
    {
        CreateWallUI();

        //ImagePixelReader();

    }

    // Initializes creation of the wall unit array based on the given dimensions and fills it with a determined starting value.
    void GenerateWallGridUI()
    {
        grid = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 1;
            }
        }
    }

    // Goes through the entire grid array and creates a wallUnitUI element as each position to fit it within the dimensional bounds designated for the particular UI element.
    // The bounds (maxWidthUIGrid and maxHeightUIGrid) create a scaling factor for each UI element so that the total area covered is constant regardless of how many elements are created.
    void CreateWallUI()
    {
        // Creates the array holding values for the locations to create 3D elements
        //GenerateWallGridUI();

        ImagePixelReader();

        Vector2 currentPosition;
        float xScaleWallUnitUI;
        float yScaleWallUnitUI;
        float unityUIScalingFactor = 100; // Unclear if this is a consistent value at all times, testing just showed that 100 created the correct size as of now.

        // Calculates scaling values to apply to individual UI Grid element so that they will all fit within the designated bounding dimensions expressed
        xScaleWallUnitUI = maxWidthUIGrid / width;
        yScaleWallUnitUI = maxHeightUIGrid / height;

        wallUnitUI.transform.localScale = new Vector2(xScaleWallUnitUI, yScaleWallUnitUI);

        // Creates the full UI grid, with each tile being a child of this gameObject. Each tile is also given GridIdentificationValues which help keep the identity of the tile
        // tied to its corresponding values found in other arrays. Its unitExistenceArrayValue is then initialized at whatever was set initially in the grid in this script.
        if (grid != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    currentPosition = new Vector2((unityUIScalingFactor * xScaleWallUnitUI) * (-width / 2 + x + 0.5f), (unityUIScalingFactor * yScaleWallUnitUI) * (-height / 2 + y + 0.5f));
                    Image createdWallUnitUI = Instantiate(wallUnitUI, currentPosition, Quaternion.identity);
                    createdWallUnitUI.transform.SetParent(gameObject.transform, false);

                    createdWallUnitUI.GetComponent<WallUnitUIScript>().xGridIdentificationValue = x;
                    createdWallUnitUI.GetComponent<WallUnitUIScript>().yGridIdentificationValue = y;
                    createdWallUnitUI.GetComponent<WallUnitUIScript>().unitExistenceArrayValue = grid[x, y];
                }
            }
        }
    }

    // Read an image file and store the color value of each pixel into an array
    void ImagePixelReader()
    {
        float currentPixelAlpha;

        // Captures dimensions of the analyzed image in pixels
        int texturePixelWidth = imageCutout.width;
        int texturePixelHeight = imageCutout.height;

        // Currently necessary for adjusting width and height values for method CreateWallUI so that they match
        width = texturePixelWidth;
        height = texturePixelHeight;

        // Initializs array with dimensions matching that of the image in pixels
        grid = new int[width, height];

        Debug.Log("The texture width in pixels = " + texturePixelWidth + " and the texture height in pixels = " + texturePixelHeight);

        // This will go through the entire grid array and assign each array element the alpha value of the associated pixel of the attached image
        for (int x = 0; x < texturePixelWidth; x++)
        {
            for (int y = 0; y < texturePixelHeight; y++)
            {
                //Color currentPixelColor = imageCutout.GetPixel(x, y);
                currentPixelAlpha = imageCutout.GetPixel(x, y).a;
                Debug.Log("The pixel color values at location " + x + ", " + y + " are: " + currentPixelAlpha);

                // Alpha values should generally be 1.000 or 0.000, so this should convert those to int values of either 1 or 0
                grid[x, y] = Mathf.RoundToInt(currentPixelAlpha);
                Debug.Log("The grid value at location " + x + ", " + y + " is now: " + grid[x, y]);
            }
        }

    }

    #endregion

}
