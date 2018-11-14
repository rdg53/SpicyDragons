using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallElementCollision : MonoBehaviour {

    #region Variables




    #endregion



    #region Unity Methods

    private void OnCollisionEnter(Collision playerCollider)
    {
        Debug.Log(name + "has hit: " + playerCollider.collider.name);
        
    }



    #endregion

}
