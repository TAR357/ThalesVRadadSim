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
    private bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        BMlaunchPoint = GameObject.FindGameObjectWithTag("LaunchPoint").GetComponent<Transform>();
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(coroutineAllowed && BMlaunched==null)
            {
                StartCoroutine(Launch());
            }
        }
    }
    
    private IEnumerator Launch()
    {
        coroutineAllowed = false;
        Instantiate(target, BMlaunchPoint);
        yield return new WaitForSeconds(timeBetween);
        BMlaunched= Instantiate(BallisticMissile, BMlaunchPoint);
        coroutineAllowed = true;
    }
}
