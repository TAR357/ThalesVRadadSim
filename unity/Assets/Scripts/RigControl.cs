using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main controller for the controls and the way the LODS are handled.
/// 
/// This must be attached to the player. The Player's controller need have the ControllerInput for the inputchecks.
/// </summary>
public class RigControl : MonoBehaviour
{
    //public booleans
    [HideInInspector] public bool isLeftGripped = false; //Check to see if left trigger is gripped 
    [HideInInspector] public bool isRightGripped = false; //Check to see if right trigger is gripped 
    [HideInInspector] public bool isWorldLoaded = false;

    //Transforms
    [SerializeField] private Transform cameraHolder = null;
    [SerializeField] private Transform leftHandLoc = null;
    [SerializeField] private Transform rightHandLoc = null;

    //Gameobjects
    [SerializeField] private GameObject leftHandHint = null;
    [SerializeField] private GameObject rightHandHint = null;
    [SerializeField] private GameObject thalesMap = null;
    [SerializeField] private GameObject worldMap = null;
    [SerializeField] private GameObject planet = null;
    [SerializeField] private GameObject cone = null;
    private GameObject playerParent = null;
    private GameObject thalesParent = null;

    //Vector3
    private Vector3 initialLeftHandPos = Vector3.zero; //Left hand position
    private Vector3 initialRightHandPos = Vector3.zero; //Right hand position
    private Vector3 initialMapScale = Vector3.zero; //initial scale of the map
    private Vector3 initialWorldScale = Vector3.zero;
    private Vector3 prevMapScale = Vector3.zero; // previous map scale

    //Floats
    [SerializeField] private float heightMax = 10000.0f;
    [SerializeField] private float heightMin = 2.0f;
    private float initialHeight;
    private float scaleHint = 500.0f;
    private float distance = 0.0f;
    private float transparency = 0.0f;
    private float[] distances = null;
    private float timeLeft = 2f;

    //Ints
    //Amount of planes
    [SerializeField] private int amountOfPlanes = 15;

    //Particle System
    private ParticleSystem[] ps = null;

    //all private Booleans
    private bool isInitialPositionSet = false;
    private bool isSatTurningOn = false;
    private bool isParented = false;
    private bool isMapParented = false;
    private bool isStartingPoint = true;
    private bool isScaling = false;

    private Color coneColor = Color.clear;
    private MeshRenderer[] coneRender = null;

    [SerializeField] private Spawner spawner = null;

    //ints
    private int thalesCounter = 0;
    private int worldCounter = 0;

    // Grab and Drop


    void Start()
    {
        thalesCounter = thalesMap.transform.childCount;
        worldCounter = worldMap.transform.childCount;
        isWorldLoaded = false;
        isSatTurningOn = false;

        //gets the clone color the cone's component to be later adjusted
        coneColor = cone.GetComponent<MeshRenderer>().material.color;

        //nullCheck for all the controllers
        if (coneRender == null)
        {
            coneRender = cone.GetComponentsInChildren<MeshRenderer>();
        }

        //calculates the max distance that is required for the LOD based on the amount of planes and the maximum given height.
        float distanceValue = heightMax / amountOfPlanes;
        distances = new float[amountOfPlanes];
        for (int i = 0; i < amountOfPlanes; i++)
        {
            distances[i] = distanceValue + (distanceValue * i);
        }
    }

    /// <summary>
    /// Loads the world in a time frame from the biggest texture to the smallest, this takes time due to retrieving all the data at different sizes
    /// </summary>
    void LoadWorld()
    {
        timeLeft = 1f;

        if (worldCounter != 0)
        {
            worldCounter--;

            if (!worldMap.transform.GetChild(worldCounter).gameObject.activeInHierarchy)
            {
                worldMap.transform.GetChild(worldCounter).gameObject.SetActive(true);
            }
        }
        else if (thalesCounter != 1)
        {
            thalesCounter--;
            if (!thalesMap.transform.GetChild(thalesCounter).gameObject.activeInHierarchy)
            {
                thalesMap.transform.GetChild(thalesCounter).gameObject.SetActive(true);
            }
        }

        if (thalesMap.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            isWorldLoaded = true;
        }

    }

    void Update()
    {
        //If the world is not loaded load the world
        if (!isWorldLoaded)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                LoadWorld();
            }
        }

        else
        {
            InitializeParticleSystem();
            InitializeScaling();

            //check if scaling then update the cone coloring accordingly and the start scaling.
            if (isScaling)
            {
                ScaleRig();
                UpdateCone();
            }

            //distance check when the UI should be turned off.
            if (distance > scaleHint)
            {
                leftHandHint.SetActive(false);
                rightHandHint.SetActive(false);
            }

            //Skybox blending
            float blend = 0;

            blend = distance / heightMax;
            if (blend > 1.0f)
            {
                blend = 1.0f;
            }

            RenderSettings.skybox.SetFloat("_Blend", blend);

            transparency = (distance * 2) / heightMax;

            SwitchSatellites();

        }

    }

    /// <summary>
    /// Gathers all the particles from the childern within the Planet's gameobject.
    /// Stores them all in an array. 
    /// </summary>
    void InitializeParticleSystem()
    {
        //Null check for the particle system.
        if (spawner.isInitialized)
        {
            if (ps == null)
            {
                ps = planet.transform.GetChild(1).GetComponentsInChildren<ParticleSystem>();

                UpdateCone();
                UpdateMap();
            }
        }
    }

    /// <summary>
    /// Check wether or not both triggers are gripped
    /// Once that is done it will check if both the player and map are parented or not.
    /// After that will take the initial scaling and position of the gameobjects the moment both controllers are being gripped.
    /// Once that is set and done the scaling will be set to true
    /// </summary>
    void InitializeScaling()
    {

        if (isLeftGripped && isRightGripped)
        {
            if (!isInitialPositionSet)
            {
                if (!isParented)
                {
                    isParented = true;
                    playerParent = new GameObject();
                    playerParent.transform.SetParent(transform);
                    playerParent.transform.localPosition = cameraHolder.localPosition;
                    cameraHolder.SetParent(playerParent.transform);
                    playerParent.name = "PlayerHolder";
                }

                if (!isMapParented)
                {
                    isMapParented = true;
                    thalesParent = new GameObject();
                    thalesParent.transform.position = new Vector3(playerParent.transform.position.x,
                        thalesMap.transform.position.y, playerParent.transform.position.z);
                    if (isStartingPoint)
                    {
                        isStartingPoint = false;
                        thalesParent.transform.localScale = thalesParent.transform.localScale;
                    }

                    else
                    {
                        thalesParent.transform.localScale = prevMapScale;
                    }

                    initialMapScale = thalesParent.transform.localScale;
                    thalesMap.transform.SetParent(thalesParent.transform);
                    thalesParent.name = "ThalesHolder";
                }

                initialLeftHandPos = leftHandLoc.position;
                initialRightHandPos = rightHandLoc.position;
                initialHeight = transform.GetChild(0).localPosition.y;
                initialWorldScale = worldMap.transform.localScale;

                isInitialPositionSet = true;
            }

            isScaling = true;
        }

        else
        {
            isScaling = false;
            isInitialPositionSet = false;

        }

        //once everything is let go the childern get unparented and get a their old parent back.
        if (playerParent != null & !isScaling & thalesParent != null)
        {
            playerParent.transform.DetachChildren();
            cameraHolder.SetParent(transform);
            Destroy(playerParent);
            playerParent = null;


            prevMapScale = thalesParent.transform.localScale;
            thalesParent.transform.DetachChildren();
            Destroy(thalesParent);
            thalesParent = null;

            isMapParented = false;
            isParented = false;
        }
    }

    /// <summary>
    /// Does a check to see if the satelittes need to be rendered 
    /// Does one more check before it should go through the list or not if not then return.
    /// Else change the particles accordingly.
    /// </summary>
    void SwitchSatellites()
    {
        if (!isSatTurningOn)
        {
            if (ps != null)
            {
                if (planet.transform.GetChild(1).GetComponentInChildren<ParticleSystem>())
                {
                    if (planet.transform.GetChild(1).GetComponentInChildren<ParticleSystem>().isStopped)
                    {
                        return;
                    }

                    foreach (ParticleSystem item in ps)
                    {
                        item.Stop(true);
                    }
                }
            }
        }

        else
        {
            if (ps != null)
            {
                if (planet.transform.GetChild(1).GetComponentInChildren<ParticleSystem>())
                {
                    if (planet.transform.GetChild(1).GetComponentInChildren<ParticleSystem>().isPlaying)
                    {
                        return;
                    }

                    foreach (ParticleSystem item in ps)
                    {
                        item.Play(true);
                    }
                }
            }
        }
    }

    /// <summary>
    /// LOD of the map that changes the textures accordingly to the distance.
    /// </summary>
    void UpdateMap()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        isSatTurningOn = false;

        thalesMap.transform.GetChild(0).gameObject.SetActive(true);
        thalesMap.transform.GetChild(1).gameObject.SetActive(true);
        thalesMap.transform.GetChild(2).gameObject.SetActive(true);
        thalesMap.transform.GetChild(3).gameObject.SetActive(true);
        thalesMap.transform.GetChild(4).gameObject.SetActive(true);
        thalesMap.transform.GetChild(5).gameObject.SetActive(true);
        thalesMap.transform.GetChild(6).gameObject.SetActive(true);
        thalesMap.transform.GetChild(7).gameObject.SetActive(true);
        thalesMap.transform.GetChild(8).gameObject.SetActive(true);
        thalesMap.transform.GetChild(9).gameObject.SetActive(true);

        worldMap.transform.GetChild(0).gameObject.SetActive(true);
        worldMap.transform.GetChild(1).gameObject.SetActive(true);
        worldMap.transform.GetChild(2).gameObject.SetActive(true);
        worldMap.transform.GetChild(3).gameObject.SetActive(true);
        worldMap.transform.GetChild(4).gameObject.SetActive(true);
        worldMap.transform.GetChild(5).gameObject.SetActive(true);

        //Thales Map LOD
        if (thalesParent != null)
        {
            if (thalesParent.transform.localScale.x > 0.001f)
            {
                if (distance > distances[0])
                {
                    thalesMap.transform.GetChild(0).gameObject.SetActive(false);
                    thalesMap.transform.GetChild(1).gameObject.SetActive(false);
                }

                if (distance > distances[1])
                {
                    thalesMap.transform.GetChild(2).gameObject.SetActive(false);
                }

                if (distance > distances[2])
                {
                    thalesMap.transform.GetChild(3).gameObject.SetActive(false);
                }

                if (distance > distances[3])
                {
                    thalesMap.transform.GetChild(4).gameObject.SetActive(false);
                }

                if (distance > distances[4])
                {
                    thalesMap.transform.GetChild(5).gameObject.SetActive(false);
                }

                if (distance > distances[5])
                {
                    thalesMap.transform.GetChild(6).gameObject.SetActive(false);
                }

                if (distance > distances[6])
                {
                    thalesMap.transform.GetChild(7).gameObject.SetActive(false);
                }

            }

            else
            {
                thalesMap.transform.GetChild(0).gameObject.SetActive(false);
                thalesMap.transform.GetChild(1).gameObject.SetActive(false);
                thalesMap.transform.GetChild(2).gameObject.SetActive(false);
                thalesMap.transform.GetChild(3).gameObject.SetActive(false);
                thalesMap.transform.GetChild(4).gameObject.SetActive(false);
                thalesMap.transform.GetChild(5).gameObject.SetActive(false);
                thalesMap.transform.GetChild(6).gameObject.SetActive(false);
                thalesMap.transform.GetChild(7).gameObject.SetActive(false);
                thalesMap.transform.GetChild(8).gameObject.SetActive(false);
                thalesMap.transform.GetChild(9).gameObject.SetActive(false);
            }
        }

        //World Map LOD
        if (distance > distances[8])
        {
            thalesMap.transform.GetChild(8).gameObject.SetActive(false);
            thalesMap.transform.GetChild(9).gameObject.SetActive(false);
        }

        if (distance > distances[9])
        {
            worldMap.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (distance > distances[10])
        {

            worldMap.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (distance > distances[11])
        {

            worldMap.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (distance > distances[12])
        {

            worldMap.transform.GetChild(3).gameObject.SetActive(true);
            worldMap.transform.GetChild(4).gameObject.SetActive(true);
        }

        if (distance > distances[13])
        {
            if (!isSatTurningOn)
            {
                isSatTurningOn = true;
            }

            worldMap.transform.GetChild(5).gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Scaling of the map and movement of the player.
    /// Calculates the distance between the controllers to determine how much the player shoudl scale.
    /// </summary>
    void ScaleRig()
    {
        //Gets current position of hands
        Vector3 currentLeftHandPos = leftHandLoc.position;
        Vector3 currentRightHandPos = rightHandLoc.position;

        // calculate the initial grab distance of the controllers
        float currentGrabDistance = Vector3.Distance(currentLeftHandPos, currentRightHandPos);
        float initialGrabDistance = Vector3.Distance(initialLeftHandPos, initialRightHandPos);

        float currentGrabDistanceM = Vector3.Distance(currentLeftHandPos, currentRightHandPos);
        float initialGrabDistanceM = Vector3.Distance(initialLeftHandPos, initialRightHandPos);

        //scaling of the player

        float p = (initialGrabDistance / currentGrabDistance);
        Debug.Log(p);
        float pMap = (currentGrabDistanceM / initialGrabDistanceM);

        // calculate new object scale with p
        float newHeight = (initialHeight * p);

        Vector3 newMapScale = initialMapScale * pMap;

        Vector3 newWorldScale = initialWorldScale * pMap;

        if (newWorldScale.x < 100)
        {
            newWorldScale = Vector3.one * 100;
        }

        else if (newWorldScale.x > 100000)
        {
            newWorldScale = Vector3.one * 100000;
        }

        if (newMapScale.x > 1)
        {
            newMapScale = Vector3.one;
        }

        else if (newMapScale.x < 0.001f)
        {
            newMapScale = Vector3.one * 0.001f;
        }

        if (newHeight > heightMax)
        {
            newHeight = heightMax;
        }

        else if (newHeight < heightMin)
        {
            newHeight = heightMin;
        }

        thalesParent.transform.localScale = newMapScale;

        worldMap.transform.localScale = newWorldScale;

        transform.GetChild(0).localPosition = new Vector3(0, newHeight, 0);
        
        thalesParent.transform.position = new Vector3(thalesParent.transform.position.x, -1 + (newHeight / 1.1f),
            thalesParent.transform.position.z);

        worldMap.transform.position = new Vector3(worldMap.transform.position.x, -1 + (newHeight / 1.1f),
            worldMap.transform.position.z);

        UpdateMap();
    }

    /// <summary>
    /// Updates the cone accordingly to the distance.
    /// </summary>
    private void UpdateCone()
    {
        cone.GetComponent<MeshRenderer>().material.color =
            new Color(coneColor.r, coneColor.g, coneColor.b, ColorConversion(transparency));
        foreach (MeshRenderer item in coneRender)
        {
            item.material.color = new Color(coneColor.r, coneColor.g, coneColor.b, ColorConversion(transparency));
        }
    }

    /// <summary>
    /// Converts the color to the rightful code.
    /// </summary>
    /// <param name="num">the number in RGBA style from 0-255 in floats.</param>
    /// <returns>Returns the converted color</returns>
    private float ColorConversion(float num)
    {
        return (num / 255.0f);
    }
}
