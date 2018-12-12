using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tools : MonoBehaviour, IInteractable {

    public enum ToolType {Sodering, Goggles, Multimeter};
    public ToolType currentToolType;

    public GameObject outerShell;

    public bool identified = true;
    //public bool identified = false;
    public bool displayInfo = false;

    public Image reticleDisplay;

    private Animator objectAnimator;
    public Animation objectAnimation;
    public AnimationClip[] clips;

    //Types of selection objects
    public enum SelectionOptions {StartAnimation, ResetScene};
    public SelectionOptions currentSelectionOption = 0;

    public GameObject myGameLogic;

    private void Start()
    {
        //if (gameObject.GetComponent<Animator>() != null)
        //{
        //    Debug.Log(gameObject.name + " has an animator and it has been set.");
        //    objectAnimator = gameObject.GetComponent<Animator>();
        //}
        if (currentSelectionOption == SelectionOptions.StartAnimation)
        {
            Debug.Log(gameObject.name + " has an animator and it has been set.");
            objectAnimator = gameObject.GetComponent<Animator>();
        }

    }

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
        Debug.Log("Tools method: OnSelect activated. The reset object has been identified. Tools.cs worked with GazeManager.");
        if(currentSelectionOption == SelectionOptions.ResetScene)
        {
            ResetScene();
        }

        if(currentSelectionOption == SelectionOptions.StartAnimation)
        {
            myGameLogic.GetComponent<GameLogic>().StartGame();
            //StartCoroutine(WaitForSeconds("startanimation"));
            StartAnimation();
        }

    }

    public void OnHover()
    {
        //ground.grounded = false;
        //GetComponent<Renderer>().material = border;
        //tickerObject.GetComponent<Renderer>().material = border;
        Debug.Log("Tools method: OnHover activated.");
        reticleDisplay.enabled = true;
        DisplayInformation();
    }

    public void OnDeselect()
    {
        Debug.Log("Tools method: OnDeselect activated.");
        reticleDisplay.enabled = false;
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

    void StartAnimation()
    {


        //myGameLogic.GetComponent<GameLogic>().StartSequence("TPose Start");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        objectAnimator.enabled = true;
        objectAnimator.Play("TPose Start");


        //WaitForSeconds(objectAnimator.GetCurrentAnimatorClipInfo(0) + 1.0f);

        //objectAnimator.Play("01-nuetralpose");





        //StartCoroutine(WaitForMySeconds());
        /*for (int i = 0; i < clips.Length; i++)
        {
            objectAnimator.enabled = true;
            objectAnimator.Play(clips[i].ToString());
            //WaitForSeconds(clips[i].ToString());
        }*/
        //objectAnimator.Play("TPose Start");
        //objectAnimation.clip = clips[3];
        //objectAnimation.Play();
        //objectAnimation.playAutomatically = true ;
        //Debug.Log("playing");
        //myGameLogic.GetComponent<GameLogic>().StartSequence();
        //objectAnimator.speed = 0.5f;
        //objectAnimator.enabled = true;
        //gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("The scene has been reset.");
    }

    IEnumerator WaitForSeconds(float secs)
    {
      
        yield return new WaitForSeconds(secs);
            //objectAnimator.Play(waitFor);
            //StartAnimation();

    }
}
