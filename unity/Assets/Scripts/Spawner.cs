using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

/// <summary>
/// Spawner script spawns the given satellites in.
/// </summary>
public class Spawner : MonoBehaviour
{
    [HideInInspector] public bool isInitialized = false;

    [SerializeField] private GameObject earth = null;

    [Header("Orbiting Satellite types")]
    [SerializeField] private GameObject geoStationSat = null;
    [SerializeField] private GameObject geoSynchSat = null;
    [SerializeField] private GameObject navSat = null;
    [SerializeField] private GameObject issSat = null;
    [SerializeField] private GameObject polarSat = null;
    [SerializeField] private GameObject leoSat = null;
    [SerializeField] private GameObject meoSat = null;

    private GameObject satSystem = null;
    private GameObject geoStatOrbit = null;
    private GameObject geoSynchOrbit = null;
    private GameObject navOrbit1 = null;
    private GameObject navOrbit2 = null;
    private GameObject navOrbit3 = null;
    private GameObject navOrbit4 = null;
    private GameObject navOrbit5 = null;
    private GameObject navOrbit6 = null;
    private GameObject leoStatOrbit = null;
    private GameObject meoStatOrbit = null;


    [Header("Amount of Satellites")]
    [SerializeField] private int geoStationSatAmount = 517;
    [SerializeField] private int geoSynchSatAmount = 402;
    [SerializeField] private int navSatAmount = 32;
    [SerializeField] private int issSatAmount = 1;
    [SerializeField] private int polarSatAmount = 2;
 
    //remaining 1146
    [SerializeField] private int leoSatAmount = 573;
    [SerializeField] private int meoSatAmount = 573;

    [SerializeField] private bool renderModels = false;
    [SerializeField] private bool testingSatellites = false;

    private float leoZoneMin;
    private float leoZoneMax;
    private float meoZoneMin;
    private float meoZoneMax;
    private float heoZone;
    private float navOrbitAmount1 = 1.0f;
    private float navOrbitAmount2 = 1.0f;
    private float navOrbitAmount3 = 1.0f;
    private float navOrbitAmount4 = 1.0f;
    private float navOrbitAmount5 = 1.0f;
    private float navOrbitAmount6 = 1.0f;

    private Vector3 center = Vector3.zero;

    [SerializeField] private RigControl controller = null;

    void Update()
    {
        if (controller != null)
        {
            if (controller.isWorldLoaded)
            {
                if (!isInitialized)
                {
                    InitializeSatelliteSystem();
                    isInitialized = true;
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (testingSatellites)
            { 
                InitializeSatelliteSystem();
            }
        }
    }

    /// <summary>
    /// This will initialize the Satellites System around the Earth.
    /// It will first destroy the current Satellite system if it exist before reinitalizing the system
    /// After that the orbits will be intialized as well as the current radius of the different orbits.
    /// Then the satellites will be spawned in.
    /// </summary>
    void InitializeSatelliteSystem()
    {
        //null check
        if (satSystem != null)
        {
            Destroy(satSystem);
        }
       
        //implementation of the satellite system
        satSystem = new GameObject();
        satSystem.transform.SetParent(earth.transform);
        satSystem.transform.localScale = Vector3.one;
        satSystem.transform.localPosition = Vector3.zero;
        satSystem.transform.rotation = new Quaternion(0, 0, 0, 0);
        satSystem.name = "satSystem";
        center = satSystem.transform.position;

        //initializeing the orbits
        InitializeOrbits();

        //setting the starting point for the amount of navigation satellites in the orbits
        navOrbitAmount1 = 1.0f;
        navOrbitAmount2 = 1.0f;
        navOrbitAmount3 = 1.0f;
        navOrbitAmount4 = 1.0f;
        navOrbitAmount5 = 1.0f;
        navOrbitAmount6 = 1.0f;

        //setting up the different radius
        heoZone = satSystem.transform.parent.localScale.x + (35786/3);
        leoZoneMin = satSystem.transform.parent.localScale.x + 200;
        leoZoneMax = satSystem.transform.parent.localScale.x + 2000;
        meoZoneMin = leoZoneMax;
        meoZoneMax = heoZone;

        //spawns in the sattellites. 
        SpawnSatellites();

        satSystem.transform.localPosition = new Vector3(0,1,0);
    }


    /// <summary>
    /// Initializes the orbits having the Satellite system as parent.
    /// </summary>
    void InitializeOrbits()
    {
        geoStatOrbit = new GameObject();
        geoStatOrbit.transform.SetParent(satSystem.transform);
        geoStatOrbit.transform.localScale = Vector3.one;
        geoStatOrbit.transform.localPosition = Vector3.zero;
        geoStatOrbit.name = "geoStatOrbit";

        geoSynchOrbit = new GameObject();
        geoSynchOrbit.transform.SetParent(satSystem.transform);

        geoSynchOrbit.transform.localScale = Vector3.one;
        geoSynchOrbit.transform.localPosition = Vector3.zero;
        geoSynchOrbit.name = "geoSynchOrbit";

        navOrbit1 = new GameObject();
        navOrbit1.transform.SetParent(satSystem.transform);
        navOrbit1.transform.localScale = Vector3.one;
        navOrbit1.transform.localPosition = Vector3.zero;
        navOrbit1.name = "navOrbit1";

        navOrbit2 = new GameObject();
        navOrbit2.transform.SetParent(satSystem.transform);
        navOrbit2.transform.localScale = Vector3.one;
        navOrbit2.transform.localPosition = Vector3.zero;
        navOrbit2.name = "navOrbit2";

        navOrbit3 = new GameObject();
        navOrbit3.transform.SetParent(satSystem.transform);
        navOrbit3.transform.localScale = Vector3.one;
        navOrbit3.transform.localPosition = Vector3.zero;
        navOrbit3.name = "navOrbit3";

        navOrbit4 = new GameObject();
        navOrbit4.transform.SetParent(satSystem.transform);
        navOrbit4.transform.localScale = Vector3.one;
        navOrbit4.transform.localPosition = Vector3.zero;
        navOrbit4.name = "navOrbit4";

        navOrbit5 = new GameObject();
        navOrbit5.transform.SetParent(satSystem.transform);
        navOrbit5.transform.localScale = Vector3.one;
        navOrbit5.transform.localPosition = Vector3.zero;
        navOrbit5.name = "navOrbit5";

        navOrbit6 = new GameObject();
        navOrbit6.transform.SetParent(satSystem.transform);
        navOrbit6.transform.localScale = Vector3.one;
        navOrbit6.transform.localPosition = Vector3.zero;
        navOrbit6.name = "navOrbit6";

        leoStatOrbit = new GameObject();
        leoStatOrbit.transform.SetParent(satSystem.transform);
        leoStatOrbit.transform.localScale = Vector3.one;
        leoStatOrbit.transform.localPosition = Vector3.zero;
        leoStatOrbit.name = "leoStatOrbit";

        meoStatOrbit = new GameObject();
        meoStatOrbit.transform.SetParent(satSystem.transform);
        meoStatOrbit.transform.localScale = Vector3.one;
        meoStatOrbit.transform.localPosition = Vector3.zero;
        meoStatOrbit.name = "meoStatOrbit";
    }

    /// <summary>
    /// Generates an object at a random position within a horizontal circle based on the center and radius.
    /// </summary>
    /// <param name="center">Center of the circle</param>
    /// <param name="radius">Radius of the circle</param>
    /// <returns>returns a Vec3 for the position</returns>
    Vector3 RandomHorizontalCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    /// <summary>
    /// Generates an object at a position within a circle based on the center, radius and the amount of satellites.
    /// </summary>
    /// <param name="center">Center of the circle</param>
    /// <param name="radius">Radius of the circle</param>
    /// <param name="i">Amount of satellites</param>
    /// <returns>returns a Vec3 for the position</returns>
    Vector3 PolarCircle(Vector3 center, float radius, int i)
    {
        float ang = 360 / (i + 1);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    /// <summary>
    /// Generates an object at a position within a circle based on the center, radius and the amount of satellites.
    /// </summary>
    /// <param name="center">Center of the circle</param>
    /// <param name="radius">Radius of the circle</param>
    /// <param name="i">Amount of satellites</param>
    /// <param name="max">The total amount of navigation satellites</param>
    /// <returns>returns a Vec3 for the position</returns>
    Vector3 NavOrbit(Vector3 center, float radius, float i, int max)
    {
        float slice = 1.0f / (max / 6.0f);
        Vector3 pos = Vector3.zero;

        float ang = (360 * (slice * i));
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }
    /// <summary>
    /// Generates an object at a random position within a circle based on the center and radius.
    /// </summary>
    /// <param name="center">Center of the circle</param>
    /// <param name="radius">Radius of the circle</param>
    /// <returns>returns a Vec3 for the position</returns>
    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    /// <summary>
    /// Generates an object at a random position within a radius surrounding a sphere
    /// </summary>
    /// <param name="minRadius">Center of the circle</param>
    /// <param name="maxRadius">Radius of the circle</param>
    /// <returns>returns a Vec3 for the position</returns>
    Vector3 RandomSphere(float minRadius, float maxRadius)
    {
        Vector3 pos;
        pos = Random.onUnitSphere * (Random.Range(minRadius, maxRadius));
        return pos;
    }

    /// <summary>
    /// Spawns/Instantiates satellites to their respective orbits with their given rotation and position.
    /// </summary>
    private void SpawnSatellites()
    {
        //GeoStationsatellites postion around the Earth
        if (geoStationSatAmount > 0)
        {
            for (int i = 0; i < geoStationSatAmount - 50; i++)
            {
                Vector3 pos = RandomHorizontalCircle(center, heoZone);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                GameObject sat = Instantiate(geoStationSat, pos, rot, geoStatOrbit.transform);
                sat.GetComponent<MeshRenderer>().enabled = renderModels;
            }

        }

        //GeoSynchsatellites postion around the Earth
        if (geoSynchSatAmount > 0)
        {
            for (int i = 0; i < geoSynchSatAmount - 50; i++)
            {
                Vector3 pos = RandomHorizontalCircle(center, heoZone);
                Quaternion rot = new Quaternion(0, 0, 0, 0);
                GameObject sat = Instantiate(geoSynchSat, pos, rot, geoSynchOrbit.transform);
                sat.GetComponent<MeshRenderer>().enabled = renderModels;
            }

            geoSynchOrbit.transform.rotation = Quaternion.Euler(45.0f, 0, 0);
        }

        //Navigationsatellites postion around the Earth
        if (navSatAmount > 0)
        {
            for (int i = 0; i < navSatAmount; i++)
            {
                if (navOrbitAmount1 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount1, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit1.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount1++;
                }

                else if (navOrbitAmount2 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount2, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit2.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount2++;
                }

                else if (navOrbitAmount3 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount3, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit3.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount3++;
                }

                else if (navOrbitAmount4 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount4, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit4.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount4++;
                }

                else if (navOrbitAmount5 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount5, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit5.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount5++;
                }

                else if (navOrbitAmount6 <= navSatAmount / 6.0f)
                {
                    Vector3 pos = NavOrbit(center, (meoZoneMax / 1.5f), navOrbitAmount6, navSatAmount);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject sat = Instantiate(navSat, pos, rot, navOrbit6.transform);
                    sat.GetComponent<Satellite>().id = 1;
                    sat.GetComponent<MeshRenderer>().enabled = renderModels;
                    navOrbitAmount6++;
                }
            }

            navOrbit1.transform.rotation = Quaternion.Euler(55, 85.0f, 15);
            navOrbit2.transform.rotation = Quaternion.Euler(55, -85.0f, 15);
            navOrbit3.transform.rotation = Quaternion.Euler(35, 65.0f, 35);
            navOrbit4.transform.rotation = Quaternion.Euler(35, -65.0f, 35);
            navOrbit5.transform.rotation = Quaternion.Euler(15, 45.0f, 55);
            navOrbit6.transform.rotation = Quaternion.Euler(15, -45.0f, 55);
        }

        //ISS position around the Earth
        if (issSatAmount > 0)
        {
            for (int i = 0; i < issSatAmount; i++)
            {
                Vector3 pos = RandomCircle(center, leoZoneMin + 200);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                GameObject sat = Instantiate(issSat, pos, rot, satSystem.transform);
                sat.GetComponent<Satellite>().id = 0;
                sat.GetComponentInChildren<MeshRenderer>().enabled = renderModels;
            }
        }

        //polar satellites
        if (polarSatAmount > 0)
        {
            for (int i = 0; i < polarSatAmount; i++)
            {
                Vector3 pos = PolarCircle(center, leoZoneMin + 500, i);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                GameObject sat = Instantiate(polarSat, pos, rot, satSystem.transform);
                sat.GetComponent<Satellite>().id = 3;
                sat.GetComponent<MeshRenderer>().enabled = renderModels;
            }
        }

        //meo satellites
        for (int i = 0; i < meoSatAmount - 100; i++)
        {
            Vector3 pos = RandomSphere(meoZoneMin, meoZoneMax);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject sat = Instantiate(meoSat, pos, rot, meoStatOrbit.transform);

            sat.GetComponentInChildren<MeshRenderer>().enabled = renderModels;
            sat.GetComponent<Satellite>().id = 1;
        }

        //leo satellites
        for (int i = 0; i < leoSatAmount - 100; i++)
        {
            Vector3 pos = RandomSphere(leoZoneMin, leoZoneMax);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject sat = Instantiate(leoSat, pos, rot, leoStatOrbit.transform);

            sat.GetComponent<MeshRenderer>().enabled = renderModels;
            sat.GetComponent<Satellite>().id = 1;
        }
    }

}