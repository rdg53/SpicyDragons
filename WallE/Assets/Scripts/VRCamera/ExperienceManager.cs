using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour {

    public static ExperienceManager instance = null;

    public bool gazing = false;
    //public bool firstGaze = false;
    public bool locked = false;
    public bool firstLocked = false;

    public bool selectionComplete = false;
    public bool firstSelectionComplete = false;
    public bool cancelProgress = false;
    public bool firstCancelProgress = false;

    public bool fadeOnSelect = false;
    public bool firstFadeOnSelect = false;

    public Image selectionProgress;
    public Image firstSelectionProgress;


    private float t = 0.0f;

    public enum ProgressBarState { Fill, Drain, FadeIn, FadeOut, Idle };
    public ProgressBarState currState;

    public bool reset = true;
    public bool firstReset = true;




    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }
	
	void Update () {
        //if (firstGaze == true)
        //{
        //    selectionProgress.gameObject.SetActive(false);
        //    firstSelectionProgress.gameObject.SetActive(true);
        //    FirstSelectionProgressBar();

        //}
        selectionProgress.gameObject.SetActive(true);
        firstSelectionProgress.gameObject.SetActive(false);
        SelectionProgressBar();
    }


    public void FirstSelectionProgressBar()
    {
        if (/*firstGaze == true && */firstSelectionComplete == false && firstLocked == false)
        {
            if (firstReset == true)
            {
                //GestureSounds.instance.PlaySingleIfPossible(GestureSounds.instance.Selecting);
                firstSelectionProgress.fillAmount = 0f;
                firstReset = false;
            }
            firstSelectionProgress.fillAmount += Time.deltaTime;
            var c = firstSelectionProgress.color;
            c.a = 255f * (firstSelectionProgress.fillAmount + 0.001f) / 255f;
            firstSelectionProgress.color = c;

            if (firstSelectionProgress.fillAmount >= 1)
            {
                firstSelectionProgress.fillAmount = 1f;
                firstSelectionComplete = true;
                firstFadeOnSelect = true;
            }

        }
        else if (firstCancelProgress == true && firstFadeOnSelect == false)
        {
            firstSelectionComplete = false;
            firstSelectionProgress.fillAmount -= Time.deltaTime;
            var c = firstSelectionProgress.color;
            c.a = 255f * (firstSelectionProgress.fillAmount + 0.001f) / 255f;
            firstSelectionProgress.color = c;

            if (firstSelectionProgress.fillAmount <= 0)
            {
                firstSelectionProgress.fillAmount = 0f;
                firstCancelProgress = false;
                firstReset = true;
                //GestureSounds.instance.StopSingle(GestureSounds.instance.Selecting);
            }

        }

        if (firstFadeOnSelect == true)
        {
            firstReset = true;
            //GestureSounds.instance.StopSingle(GestureSounds.instance.Selecting);
            var c = firstSelectionProgress.color;
            c.a = Mathf.Lerp(1f, 0f, t);
            firstSelectionProgress.color = c;

            t += Time.deltaTime;
            if (t > 1.0f)
            {
                firstSelectionProgress.fillAmount = 0f;
                firstFadeOnSelect = false;
                //firstGaze = false;

                t = 0.0f;
            }
        }
    }

    public void SelectionProgressBar()
    {
        if (gazing == true && selectionComplete == false && locked == false)
        {
            if (reset == true)
            {
                //GestureSounds.instance.PlaySingleIfPossible(GestureSounds.instance.Selecting);
                selectionProgress.fillAmount = 0f;
                reset = false;
            }
            selectionProgress.fillAmount += Time.deltaTime;
            var c = selectionProgress.color;
            c.a = 255f * (selectionProgress.fillAmount + 0.001f) / 255f;
            selectionProgress.color = c;

            if (selectionProgress.fillAmount >= 1)
            {
                selectionProgress.fillAmount = 1f;
                selectionComplete = true;
                fadeOnSelect = true;
            }

        }
        else if (cancelProgress == true && fadeOnSelect == false)
        {
            selectionComplete = false;
            selectionProgress.fillAmount -= Time.deltaTime;
            var c = selectionProgress.color;
            c.a = 255f * (selectionProgress.fillAmount + 0.001f) / 255f;
            selectionProgress.color = c;
            if (selectionProgress.fillAmount <= 0)
            {
                selectionProgress.fillAmount = 0f;
                cancelProgress = false;
                reset = true;
                //GestureSounds.instance.StopSingle(GestureSounds.instance.Selecting);
            }

        }

        if (fadeOnSelect == true)
        {
            reset = true;
            //GestureSounds.instance.StopSingle(GestureSounds.instance.Selecting);
            var c = selectionProgress.color;
            c.a = Mathf.Lerp(1f, 0f, t);
            selectionProgress.color = c;

            t += Time.deltaTime;
            if (t > 1.0f)
            {
                selectionProgress.fillAmount = 0f;
                fadeOnSelect = false;
                gazing = false;
                locked = true;

                t = 0.0f;
            }
        }
    }





    IEnumerator FadeInFadeOut(float sign, float target )
    {
        while(selectionProgress.fillAmount <= 1)
        {
            selectionProgress.fillAmount = (selectionProgress.fillAmount + (Time.deltaTime*sign));
            var c = selectionProgress.color;
            c.a = 255f * (selectionProgress.fillAmount + 0.001f) / 255f;
            selectionProgress.color = c;
        }
        yield return null;
    }




    public void FoundSoderingIron()
    {
        //foundSoderingIron = true;
        //GestureSounds.instance.PlaySingleIfPossible(GestureSounds.instance.PickedUp);
    }
    public void FoundGoggles()
    {
        //foundGoggles = true;
        //GestureSounds.instance.PlaySingleIfPossible(GestureSounds.instance.PickedUp);
    }
    public void FoundMultimeter()
    {
        //foundMultimeter = true;
        //GestureSounds.instance.PlaySingleIfPossible(GestureSounds.instance.PickedUp);
    }
}
