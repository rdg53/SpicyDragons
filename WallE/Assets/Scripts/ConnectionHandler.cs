using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using eaglib;
//EG REQUIRED
using enableGame;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        NetworkClientConnect.Instance.Connect(IpRemember.IpAddress);
    }
}
