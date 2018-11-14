using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

	public void ExitAction(){
		#if UNITY_EDITOR 
		if(Application.isEditor) UnityEditor.EditorApplication.isPlaying = false; 
		#endif 
		Application.Quit();
	}
}
