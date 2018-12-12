using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using eaglib;
//EG REQUIRED
using enableGame;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IpConnect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void Connect(InputField ipAddress)
    {
        // connect the client skeleton to the server skeleton (running in the enablegames launcher app)
        string address = ipAddress.text;
        IpRemember.IpAddress = address;
        print("Address= " + address);
        //NetworkClientConnect.Instance.Connect(address);
        //print("egAwake:after connect.");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
