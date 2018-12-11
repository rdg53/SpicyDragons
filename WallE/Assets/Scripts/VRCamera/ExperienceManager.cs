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
    //public Image firstSelectionProgress;


    private float t = 0.0f;

    public enum ProgressBarState { Fill, Drain, FadeIn, FadeOut, Idle };
    public ProgressBarState currState;

    public bool reset = true;
    public bool firstReset = true;

    //additional variable
    public float selectionTimeScalingFactor = 1.0f;


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
        selectionProgress.gameObject.SetActive(true);
        SelectionProgressBar();
    }

    public void SelectionProgressBar()
    {
        if (gazing == true && selectionComplete == false && locked == false)
        {
            if (reset == true)
            {
                selectionProgress.fillAmount = 0f;
                reset = false;
            }
            //selectionProgress.fillAmount += Time.deltaTime;
            selectionProgress.fillAmount += Time.deltaTime * selectionTimeScalingFactor;
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
            //selectionProgress.fillAmount -= Time.deltaTime;
            selectionProgress.fillAmount -= Time.deltaTime * selectionTimeScalingFactor;
            var c = selectionProgress.color;
            c.a = 255f * (selectionProgress.fillAmount + 0.001f) / 255f;
            selectionProgress.color = c;
            if (selectionProgress.fillAmount <= 0)
            {
                selectionProgress.fillAmount = 0f;
                cancelProgress = false;
                reset = true;
            }

        }

        if (fadeOnSelect == true)
        {
            reset = true;
            var c = selectionProgress.color;
            c.a = Mathf.Lerp(1f, 0f, t);
            selectionProgress.color = c;

            //t += Time.deltaTime;
            t += Time.deltaTime * selectionTimeScalingFactor;
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
}
