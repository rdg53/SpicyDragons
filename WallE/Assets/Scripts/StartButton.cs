using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	public GameObject StartMenu;
	public GameObject MainMenu;

	public void StartAction(){
		StartMenu.SetActive(false);
		MainMenu.SetActive(true);
	}
}
