using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public GameObject FirstPersonLocation;
    public GameObject ThirdPersonLocation;
    public GameObject myAvatar;
    public GameObject myCamera;
    public GameObject myImage;


    // Use this for initialization
	void Start () {
		
	}


    public void StartGame()
    {
        //myImage.SetActive(true);
        //myCamera.transform.SetParent(null);
        //myAvatar.transform.SetParent(null);
        myAvatar.transform.position = ThirdPersonLocation.transform.position;
        myAvatar.transform.rotation = ThirdPersonLocation.transform.rotation;
        myAvatar.SetActive(true);
        myImage.SetActive(false);
    }


    public void StartSequence(string sequencename)
    {
      

    }


    public void RestartGame()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
