﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class WallMover : MonoBehaviour {

    Vector3 startPosition;
    GameObject wallPositionParent;
    GameObject wall;

    public GameObject MainMenu;
    public GameObject ResultsMenu;

    public Text percentageText;
    public Image renderImage;
    public Transform renderPos;


    List<GameObject> poseList = new List<GameObject>();
    bool wallSpawned = false;
    public Canvas posePicker;

    public PlayableDirector playableDirector;

    List<GameObject> pickedPanel = new List<GameObject>();

	// Use this for initialization
    //find start position for walls
	void Start () {
        startPosition = new Vector3(0, 25, 500);
        wallPositionParent = GameObject.Find("wallPosition");
        playableDirector.gameObject.SetActive(false);
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
    public void TogglePanel(GameObject buttonPanel)
    {

        if (pickedPanel.Contains(buttonPanel))
        {
            pickedPanel.Remove(buttonPanel);
            buttonPanel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.392f);
        }
        else
        {
            pickedPanel.Add(buttonPanel);
            buttonPanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.392f);
        }

        /*Color panelColor = buttonPanel.GetComponent<Image>().color;
        Debug.Log(panelColor);



        if (panelColor == new Color(1.0f,1.0f,1.0f,0.392f))
        {
            buttonPanel.GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f,0.392f);

           Debug.Log(buttonPanel.GetComponent<Image>().color.r.ToString());
        }
        else
        {
            //panelColor = Color.white;
            buttonPanel.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.392f);

           Debug.Log("unselected");
        }*/




        /*if (button.colors.highlightedColor.r != 1.0f)
        {
            
        }
        */
        //Debug.Log(button.colors.highlightedColor.ToString());
    }

    //spawn the entire list of walls
    public void SpawnWallList()
    {

        
        this.HideShowPicker();
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();

        // playableDirector.on


        /*foreach (GameObject but in pickedPanel)
        {
            but.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.392f);
            pickedPanel.Remove(but);
        }*/


        StartCoroutine(MoveOverSeconds(new Vector3(0, 22, 0), 5f));




    }

    //hide/show the picker canvas
    void HideShowPicker()
    {
        posePicker.enabled = !posePicker.isActiveAndEnabled;
    }

    void ShowResults(){
        posePicker.enabled = !posePicker.isActiveAndEnabled;
        MainMenu.SetActive(false);
        ResultsMenu.SetActive(true);

    }

    //spawn, parent, and move the walls from start to end in 5 seconds
    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        while(playableDirector.time < playableDirector.duration-0.5f){
            yield return true;
        }
        playableDirector.gameObject.SetActive(false);

        foreach (GameObject cube in poseList)
        {
            //create the pose wall and set the position relative to the parent
            //wall = Instantiate(cube, wallPositionParent.transform.position, new Quaternion(0, 0, 0, 0));
            wall = Instantiate(cube, wallPositionParent.transform.position, Quaternion.identity);
            wall.transform.parent = wallPositionParent.transform;
            wall.transform.rotation = wallPositionParent.transform.rotation;
            Debug.Log("Instantiated object's rotation value is: " + wall.transform.rotation);

            wall.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            

            float elapsedTime = 0;
            Vector3 startingPos = wall.transform.position;

            //move the wall
            while (elapsedTime < seconds)
            {
                wall.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            // wall.transform.position = end;
            wall.transform.position = renderPos.position;
            RenderUnlitCamera render = wall.GetComponent<RenderUnlitCamera>();
            render.RenderCamera();
            renderImage.gameObject.SetActive(true);
            renderImage.sprite = Sprite.Create(render.texture2D, new Rect(0, 0, render.renderTexture.width, render.renderTexture.height), new Vector2(0.5f,0.5f));
            StartCoroutine(render.CompareTexture(percentageText));
            

            Destroy(wall);
        }
        // this.HideShowPicker();
        ShowResults();

        foreach (GameObject but in pickedPanel)
        {
            but.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.392f);
            //pickedPanel.Remove(but);
        }

        pickedPanel.Clear();
        poseList.Clear();
    }

    // Update is called once per frame
    void Update () {

	}
}
