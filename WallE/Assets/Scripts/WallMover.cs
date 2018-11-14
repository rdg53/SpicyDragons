using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallMover : MonoBehaviour {

    Vector3 startPosition;
    GameObject wallPositionParent;
    GameObject wall;

    public Text percentageText;
    public Image renderImage;


    List<GameObject> poseList = new List<GameObject>();
     
    bool wallSpawned = false;

    public Canvas posePicker;

	// Use this for initialization
    //find start position for walls
	void Start () {
        startPosition = new Vector3(0, 25, 500);

        wallPositionParent = GameObject.Find("wallPosition");
    }

    //add or remove pose to list when button is clicked
    public void AddRemovePose(GameObject cubeName)
    {
        //this.HideShowPicker();
        //wallPositionParent = GameObject.Find("wallPosition");
        //wall = Instantiate(cubeName, wallPositionParent.transform.position, new Quaternion(0, 0, 0, 0));
        //wall.transform.SetPositionAndRotation(new Vector3(0, 0, 0),new Quaternion(0,0,0,0));
        //wall.transform.parent = wallPositionParent.transform;
        // wall.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
        if (poseList.Contains(cubeName))
        {
            poseList.Remove(cubeName);
        }
        else
        {
            poseList.Add(cubeName);
            Debug.Log(cubeName.ToString());
        }

    }

    //test function for button toggle color change
    public void ToggleButton(Button button)
    {
        if (button.colors.highlightedColor.r != 1.0f)
        {
            
        }

        Debug.Log(button.colors.highlightedColor.ToString());
    }

    //spawn the entire list of walls
    public void SpawnWallList()
    {     
        this.HideShowPicker();
        StartCoroutine(MoveOverSeconds(new Vector3(0, 22, 0), 5f));
    }

    //hide/show the picker canvas
    void HideShowPicker()
    {
        posePicker.enabled = !posePicker.isActiveAndEnabled;
    }

    //spawn, parent, and move the walls from start to end in 5 seconds
    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        foreach (GameObject cube in poseList)
        {
            //create the pose wall and set the position relative to the parent
            //wall = Instantiate(cube, wallPositionParent.transform.position, new Quaternion(0, 0, 0, 0));
            wall = Instantiate(cube, wallPositionParent.transform.position, Quaternion.identity);
            wall.transform.parent = wallPositionParent.transform;
            wall.transform.rotation = wallPositionParent.transform.rotation;
            Debug.Log("Instantiated object's rotation value is: " + wall.transform.rotation);

            wall.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            /*RenderUnlitCamera render = wall.GetComponent<RenderUnlitCamera>();

            render.RenderCamera();
            renderImage.gameObject.SetActive(true);
            renderImage.sprite = Sprite.Create(render.texture2D, new Rect(0, 0, render.renderTexture.width, render.renderTexture.height), new Vector2(0.5f,0.5f));
            StartCoroutine(render.CompareTexture(percentageText));
            */

            float elapsedTime = 0;
            Vector3 startingPos = wall.transform.position;

            //move the wall
            while (elapsedTime < seconds)
            {
                wall.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            wall.transform.position = end;

            Destroy(wall);
        }
        this.HideShowPicker();
        poseList.Clear();
    }

    // Update is called once per frame
    void Update () {

	}
}
