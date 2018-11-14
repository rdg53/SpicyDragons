using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject ImageMenu;
	// Use this for initialization
	public void Return(){
		MainMenu.SetActive(true);
		ImageMenu.SetActive(false);
	}
}
