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

    public Texture2D imageCutout;

    // For ResoultionControlledImagePixelReader test. The number of pixels to skip for each check, which reduces resolution
    // of grid relative to pixel count of source cutout image.
    public int resolutionScalingFactor = 1;
    #endregion

    #region Unity Methods

    private void Start()
    {
        ImageBasedWallCreatorUI();
    }

    // The following 2 methods provide different ways of creating the UI grid. There is an option for simply choosing
    // width and height values and creating a rectangle based on those dimensions, and there is an option for having 
    // the rectangle determined by the pixel dimensions of an input image.

    // Creates a UI grid based on simple width and height dimensions numerically input
    void SimpleGridWallCreatorUI()
    {
        GenerateWallGridUI();
        CreateWallUI();
    }

    // Creates a UI grid based on an input sprite file
    void ImageBasedWallCreatorUI()
    {
        ResolutionControlledImagePixelReader();
        CreateWallUI();
    }

    // Initializes creation of the wall unit array based on the given dimensions and fills it with a 
    // determined starting value.
    void GenerateWallGridUI()
    {
        grid = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }
    }

    // Read an image file and store the color value of varying amounts of pixels into an array.
    // This will look into skipping certain amounts of pixels to deal with higher resolution images.
    void ResolutionControlledImagePixelReader()
    {
        float currentPixelAlpha;

        // Captures dimensions of the analyzed image in pixels
        int texturePixelWidth = imageCutout.width / resolutionScalingFactor;
        int texturePixelHeight = imageCutout.height / resolutionScalingFactor;

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
                currentPixelAlpha = imageCutout.GetPixel(x * resolutionScalingFactor, y * resolutionScalingFactor).a;
                Debug.Log("The pixel color values at location " + x + ", " + y + " are: " + currentPixelAlpha);

                // Alpha values should generally be 1.000 or 0.000, so this should convert those to int values of either 1 or 0
                grid[x, y] = Mathf.RoundToInt(currentPixelAlpha);
                Debug.Log("The grid value at location " + x + ", " + y + " is now: " + grid[x, y]);
            }
        }

    }

    // Goes through the entire grid array and creates a wallUnitUI element as each position to fit it within the dimensional bounds designated for the particular UI element.
    // The bounds (maxWidthUIGrid and maxHeightUIGrid) create a scaling factor for each UI element so that the total area covered is constant regardless of how many elements are created.
    // The values for the grid array should be determined by another method before this runs.
    void CreateWallUI()
    {
        Vector2 currentPosition;
        float xScaleWallUnitUI;
        float yScaleWallUnitUI;
        float unityUIScalingFactor = 100; // Unclear if this is a consistent value at all times, testing just showed that 100 created the correct size as of now.

        // Calculates scaling values to apply to individual UI Grid element so that they will all fit within the designated bounding dimensions expressed
        xScaleWallUnitUI = maxWidthUIGrid / width;
        yScaleWallUnitUI = maxHeightUIGrid / height;

        // Changes this gameObjects scale so that all of the children scale down to fit a consistent area
        transform.localScale = new Vector2(xScaleWallUnitUI, yScaleWallUnitUI);

        // Creates the full UI grid, with each tile being a child of this gameObject. Each tile is also given GridIdentificationValues which help keep the identity of the tile
        // tied to its corresponding values found in other arrays. Its unitExistenceArrayValue is then initialized at whatever was set initially in the grid in this script.
        if (grid != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    currentPosition = new Vector2((unityUIScalingFactor * xScaleWallUnitUI) * (-width / 2 + x + 0.5f) + transform.position.x, (unityUIScalingFactor * yScaleWallUnitUI) * (-height / 2 + y + 0.5f) + transform.position.y);

                    // Creates the UI image gameObject and positions it
                    GameObject newGridUIObject = CreateUIImageObject(currentPosition);

                    // Fills out variables found within script attached to created newGridUIObject when they are created
                    newGridUIObject.GetComponent<WallUnitUIScript>().xGridIdentificationValue = x;
                    newGridUIObject.GetComponent<WallUnitUIScript>().yGridIdentificationValue = y;
                    newGridUIObject.GetComponent<WallUnitUIScript>().unitExistenceArrayValue = grid[x, y];
                }
            }
        }
    }

    // Creates a basic UI image gameObject as a child of this gameObject, positions it based on the input Vector2,
    // and adds a script to each of these objects.
    GameObject CreateUIImageObject(Vector2 currentElementInstantiationPosition)
    {
        // Creates new game object to hold image component
        GameObject newImageElement = new GameObject();

        // Adds image component to the game object
        newImageElement.AddComponent<Image>();

        // Sets current game object as parent of the created object
        newImageElement.transform.SetParent(gameObject.transform, false);

        // Places the newly created image in a position designated by the Vector2 input in the method
        newImageElement.transform.position = currentElementInstantiationPosition;

        // Adds WallUnitUIScript to each individually created game object so they will each have functionality of previous prefab
        newImageElement.AddComponent<WallUnitUIScript>();

        return newImageElement;
    }

    #endregion

}
