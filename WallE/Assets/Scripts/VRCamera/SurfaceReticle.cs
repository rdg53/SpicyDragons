using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceReticle : MonoBehaviour {

    private Vector3 defaultPosition = new Vector3(0f, 0f, 3f);
    public GameObject df;
    private float distance;

    public void PositionReticle(Vector3 position)
    {
        this.transform.position =  new Vector3 (position.x, position.y, position.z - 1f);
        this.transform.forward = GazeManager.Instance.Normal;
        distance = Vector3.Distance(this.transform.position, position);
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * (distance * 60);
    }

    public void ResetReticle()
    {
        //this.transform.position = defaultPosition;
        this.transform.forward = Vector3.zero;
        this.transform.position = df.transform.position;
        if (distance > 0)
        {
            this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }
    }
}
