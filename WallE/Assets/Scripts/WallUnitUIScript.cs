using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WallUnitUIScript : MonoBehaviour, IPointerDownHandler {

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
        wallUnitUI = gameObject.GetComponent<Image>();

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

    // Performs necessary method when UI image object is clicked
    public void OnPointerDown (PointerEventData eventData)
    {
        ChangeWallUnitUIValue();
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


    #endregion
}
