using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour {

    Vector3 startPosition;
    GameObject wallPositionParent;
    GameObject wall;

    bool wallSpawned = false;

    public Canvas posePicker;

	// Use this for initialization
	void Start () {
        startPosition = new Vector3(0, 25, 500);
	}


    public void SpawnWall(GameObject cubeName)
    {
        this.HideShowPicker();
        wallPositionParent = GameObject.Find("wallPosition");
        wall = Instantiate(cubeName, wallPositionParent.transform.position, new Quaternion(0, 0, 0, 0));
        //wall.transform.SetPositionAndRotation(new Vector3(0, 0, 0),new Quaternion(0,0,0,0));
        wall.transform.parent = wallPositionParent.transform;
        wall.transform.localScale = new Vector3(0.01f,0.01f,0.01f);

        //wallSpawned = true;
        StartCoroutine(MoveOverSeconds(wall,new Vector3(0,0,0), 5f));

        /*if (wallSpawned)
        {
            Destroy(wall);
            wallSpawned = false;

        }*/

    }

    void HideShowPicker()
    {
        posePicker.enabled = !posePicker.isActiveAndEnabled;
    }


    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;

        Destroy(wall);
        this.HideShowPicker();

        Debug.Log("Over");
    }

    // Update is called once per frame
    void Update () {

	}
}
