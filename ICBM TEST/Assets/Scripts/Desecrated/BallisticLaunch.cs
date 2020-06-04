using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticLaunch : MonoBehaviour
{
    //flightTime
    public float flightTime;
    public Rigidbody missile;
    //TargetLocationbyCursor (Will be changed in implementation)
    public GameObject cursor;
    public GameObject Planet;
    //Launch Position
    public Transform originPos;
    public LayerMask layer;
       

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //originPos = GameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(originPos.position);
        launchMissile();
    }


    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Distance from launch point to target
        Vector3 distance = target - origin;
        Vector3 distancexz = distance;
        distancexz.y = 0f;
        //Highest point
        float h = distance.y;
        //Range
        float xz = distancexz.magnitude;
        //parabola calculation
        float Vxz = xz / time;
        float Vy = h / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distancexz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    void launchMissile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay,out hit,100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            
            Vector3 Vorigin = CalculateVelocity(hit.point, originPos.position , flightTime);
            
            if(Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(missile, originPos.position, Quaternion.identity);
                obj.transform.rotation = Quaternion.LookRotation(Vorigin);
                obj.velocity = Vorigin;
            }
        }
    }
}
