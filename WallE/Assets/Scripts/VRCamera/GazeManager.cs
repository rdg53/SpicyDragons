using UnityEngine;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// GazeManager determines the location of the user's gaze, hit position and normals.
    /// </summary>
    public class GazeManager : Singleton<GazeManager>
    {
        [Tooltip("Maximum gaze distance for calculating a hit.")]
        public float MaxGazeDistance = 50.0f;

        [Tooltip("Select the layers raycast should target.")]
        public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;

        /// <summary>
        /// Physics.Raycast result is true if it hits a Hologram.
        /// </summary>
        public bool Hit { get; private set; }

        /// <summary>
        /// HitInfo property gives access
        /// to RaycastHit public members.
        /// </summary>
        public RaycastHit HitInfo { get; private set; }

        /// <summary>
        /// Position of the user's gaze.
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// RaycastHit Normal direction.
        /// </summary>
        public Vector3 Normal { get; private set; }

        private GazeStabilizer gazeStabilizer;
        private Vector3 gazeOrigin;
        private Vector3 gazeDirection;



        //MINE
        //private cakeslice.Outline hitOutline;
        //private FillWire hitWire;
        //private Switch hitSwitch;
        //private Switch.SwitchState switchState;
        public SurfaceReticle sr;
        public GameObject currentHitInfo = null;

        //public QueueManager myQueueManager;

        
        void Awake()
        {
            gazeStabilizer = GetComponent<GazeStabilizer>();
        }

        private void Update()
        {
            gazeOrigin = Camera.main.transform.position;

            gazeDirection = Camera.main.transform.forward;

            gazeStabilizer.UpdateHeadStability(gazeOrigin, Camera.main.transform.rotation);

            gazeOrigin = gazeStabilizer.StableHeadPosition;

            UpdateRaycast();
        }

        /// <summary>
        /// Calculates the Raycast hit position and normal.
        /// </summary>
        private void UpdateRaycast()
        {
            RaycastHit hitInfo;

            Hit = Physics.Raycast(gazeOrigin,
                           gazeDirection,
                           out hitInfo,
                           MaxGazeDistance,
                           RaycastLayerMask);

            HitInfo = hitInfo;

            if (Hit)
            {
                // If raycast hit a hologram...
                Debug.Log("The raycast has hit: " + Hit);
                Position = hitInfo.point;
                Normal = hitInfo.normal;
            }
            else
            {
                // If raycast did not hit a hologram...
                // Save defaults ...
                Debug.Log("The raycast has missed everything.");
                Position = gazeOrigin + (gazeDirection * MaxGazeDistance);
                Normal = gazeDirection;
            }


            //NEW MINE

            if (hitInfo.collider != null)
            {
                //Place reticle on surface
                sr.PositionReticle(Position);
                currentHitInfo = hitInfo.collider.gameObject;
                //Code to display the hover outline on objects and detect what kind of object you are hovering over
                if (hitInfo.collider.GetComponent<IInteractable>() != null)
                {
                    hitInfo.collider.GetComponent<IInteractable>().OnHover();

                    ExperienceManager.instance.gazing = true;
                    /*
                    if (hitInfo.collider.GetComponent<IInteractable>().Identified == false) {
                        
                        //ExperienceManager.instance.firstGaze = true;
                    } else
                    {
                        ExperienceManager.instance.gazing = true;
                    }
                    */
                }



                //hitOutline.enabled = true;
                if (ExperienceManager.instance.firstSelectionComplete == true)
                {
                    if (hitInfo.collider.GetComponent<IInteractable>() != null)
                    {

                        if (hitInfo.collider.GetComponent<IInteractable>().Identified == false)
                        {
                            hitInfo.collider.GetComponent<IInteractable>().OnSelect();

                            //ExperienceManager.instance.firstGaze = false;
                            hitInfo.collider.GetComponent<IInteractable>().Identified = true;

                        }
                    }
                }

                if (ExperienceManager.instance.selectionComplete == true)
                {
                    if (hitInfo.collider.GetComponent<IInteractable>() != null)
                    {
                        hitInfo.collider.GetComponent<IInteractable>().OnSelect();

                        if (hitInfo.collider.GetComponent<IInteractable>().Identified == true)
                        {
                            ExperienceManager.instance.gazing = false;

                        }

                    }
                }
                
            }

            else
            {
                //sr.ResetReticle();
               // GM.instance.selectionComplete = false;
                //hitInfo.collider.GetComponent<IInteractable>().OnDeselect();
                if (currentHitInfo != null)
                {
                    if (currentHitInfo.GetComponent<IInteractable>() != null)
                    {
                        currentHitInfo.GetComponent<IInteractable>().OnDeselect();
                    }
                    
                }

                ExperienceManager.instance.locked = false;
                
                ExperienceManager.instance.gazing = false;
                //ExperienceManager.instance.firstGaze = false;
                ExperienceManager.instance.cancelProgress = true;
                ExperienceManager.instance.firstCancelProgress = true;
                ExperienceManager.instance.selectionComplete = false;
                ExperienceManager.instance.firstSelectionComplete = false;
            }

        }
    }
}