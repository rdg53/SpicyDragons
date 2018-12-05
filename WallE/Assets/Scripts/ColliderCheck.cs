using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour {

    public SphereCollider avatarCollider;
    public SphereCollider animationCollider;
    public GameObject fireflyGeo;
    public Material red;
    public Material green;
    
    
    
    // Use this for initialization
	void Start () {
		
	}

    private void OnTriggerExit(Collider collider)
    {
        //Debug.LogWarning("yeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees");
       // Debug.LogWarning(collider.gameObject);
        Material[] mats = new Material[] { red };
        fireflyGeo.GetComponent<SkinnedMeshRenderer>().materials = mats;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("askdnaiushduiahuisdhuiahsduihauids");
        Material[] mats = new Material[] { green };
        fireflyGeo.GetComponent<SkinnedMeshRenderer>().materials = mats;
    }

    private void OnTriggerStay(Collider other)
    {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
