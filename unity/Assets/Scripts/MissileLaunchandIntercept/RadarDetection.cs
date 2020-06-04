using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetection : MonoBehaviour
{
    [SerializeField]
    public GameObject InterceptionMissile;
    [SerializeField]
    private Transform IMLaunchPoint;    
    //public int IMcount;
    public bool MissileDetected;
    private GameObject launchedMissile;
    TrajectoryScript trajectoryScript;
    
    // Start is called before the first frame update
    void Start()
    {
        MissileDetected = false;       
        //Debug.Log(MissileDetected);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(MissileDetected==true)
        {            
            if(launchedMissile==null)
            {                
                launchedMissile=Instantiate(InterceptionMissile, IMLaunchPoint);
            }
            MissileDetected = false;
        }
        //Debug.Log("imcount: " + IMcount);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.gameObject.tag=="BallisticMissile")
        {
            MissileDetected = true;
            
        }        
    }
    
}
