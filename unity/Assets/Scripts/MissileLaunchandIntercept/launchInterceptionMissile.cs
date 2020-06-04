using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchInterceptionMissile : MonoBehaviour
{
    [SerializeField]
    public GameObject InterceptionMissile;
    private Transform IMLaunchPoint;
    // Start is called before the first frame update
    void Start()
    {
        IMLaunchPoint = this.transform;
    }

    // Update is called once per frame
    public void launchInterception()
    {
        Debug.Log("IM Launched");
        Instantiate(InterceptionMissile, IMLaunchPoint);
    }
}
