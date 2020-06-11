using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBM : MonoBehaviour
{
    private Transform BMlaunchPoint;

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject BallisticMissile;
    [SerializeField]
    private float timeBetween;
    private GameObject BMlaunched;
    private GameObject TargetLaunched;
    private bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        BMlaunchPoint = GameObject.FindGameObjectWithTag("BMLaunchPoint").GetComponent<Transform>();
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(coroutineAllowed && BMlaunched == null)
            {
                StartCoroutine(Launch());
            }
        }
        Debug.Log("CoroutineAllowed:" + coroutineAllowed);
        //Debug.Log("BMLaunched" + BMlaunched.name);
    }
    
    public void ButtonLaunch()
    {
        Debug.Log("Launch");
        if (coroutineAllowed==true)
        {
            StartCoroutine(Launch());
        }
    }

    private IEnumerator Launch()
    {
        if(TargetLaunched==null)
        {
            coroutineAllowed = false;
            Instantiate(target, BMlaunchPoint);
            yield return new WaitForSeconds(timeBetween);
            Instantiate(BallisticMissile, BMlaunchPoint);
            coroutineAllowed = true;
        }       
    }
}
