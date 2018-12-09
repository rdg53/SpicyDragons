using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour, IInteractable {

    public enum ToolType {Sodering, Goggles, Multimeter};
    public ToolType currentToolType;

    public GameObject outerShell;

    public bool identified = true;
    //public bool identified = false;
    public bool displayInfo = false;

    
    bool IInteractable.Identified
    {
        get
        {
            return identified;
        }

        set
        {
            identified = true;
        }
    }
    

    public void OnSelect()
    {
        /*
        if (identified == true)
        {
            if (currentToolType == ToolType.Goggles)
            {
                ExperienceManager.instance.FoundGoggles();
                outerShell.SetActive(false);
                Destroy(gameObject);
            }
            if (currentToolType == ToolType.Sodering)
            {
                ExperienceManager.instance.FoundSoderingIron();
                Destroy(gameObject);

            }
            if (currentToolType == ToolType.Multimeter)
            {
                ExperienceManager.instance.FoundMultimeter();
                Destroy(gameObject);

            }
        } else
        {
            DisplayInformation();
            //identified = true;
        }
        */

        // TODO: Reset Scene here!
        Debug.Log("Tools method: OnSelect activated. The reset object has been identified. Tools.cs worked with GazeManager.");
        Debug.Log("This is where resetting the game will happen.");
        Destroy(gameObject);


    }

    public void OnHover()
    {
        //ground.grounded = false;
        //GetComponent<Renderer>().material = border;
        //tickerObject.GetComponent<Renderer>().material = border;
        Debug.Log("Tools method: OnHover activated.");
        DisplayInformation();
    }

    public void OnDeselect()
    {
        Debug.Log("Tools method: OnDeselect activated.");
        //tickerObject.GetComponent<Renderer>().material = nonBorder;
        if (identified == false)
        {
            //identified = true;
        }
        HideInformation();
    }


    public void DisplayInformation()
    {
        //identified = true;
        displayInfo = true;
    }
    public void HideInformation()
    {
        displayInfo = false;
    }
}
