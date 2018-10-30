using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallUnitUIScript : MonoBehaviour {

    #region Variables

    public Image wallUnitUI;
    public int xGridIdentificationValue = 0;
    public int yGridIdentificationValue = 0;
    public int unitExistenceArrayValue = 0;


    private WallGenerator wallGenerator;
    #endregion

    #region Unity Methods

    // Initializes tile as existing or nonexisting color based on its initial value.
    private void Start()
    {
        wallGenerator = GameObject.Find("WallGenerator").GetComponent<WallGenerator>();

        if (unitExistenceArrayValue == 1)
        {
            ExistingWallUnit();
        }
        else
        {
            NonexistingWallUnit();
        }

    }

    // Changes wall unit value and applies new existing condition based on that new value, and changes corresponding array value in the WallGenerator to match.
    public void ChangeWallUnitUIValue()
    {
        if (unitExistenceArrayValue == 1)
        {
            unitExistenceArrayValue = 0;
            NonexistingWallUnit();
        }
        else
        {
            unitExistenceArrayValue = 1;
            ExistingWallUnit();
        }

        wallGenerator.map[xGridIdentificationValue, yGridIdentificationValue] = unitExistenceArrayValue;
    }

    // Changes color of UI element to that decided for nonexisting state
    public void NonexistingWallUnit()
    {
        wallUnitUI.GetComponent<Image>().color = Color.black;
    }

    // Changes color of UI element to that decided for existing state
    public void ExistingWallUnit()
    {
        wallUnitUI.GetComponent<Image>().color = Color.white;
    }

    // Following does not work for images, only works with detecting physical objects

    //void ImageDetectionRayCast()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2, Color.yellow);
    //        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit");

    //        unitExistenceArrayValue = 0;
    //        NonexistingWallUnit();
    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    //        Debug.Log("Did not Hit");
    //    }
    //}


    #endregion
}
