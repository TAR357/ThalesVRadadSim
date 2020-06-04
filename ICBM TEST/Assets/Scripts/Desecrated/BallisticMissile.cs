using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticMissile : MonoBehaviour
{
    public GameObject target;
    public Rigidbody ballisticmissileRB;
    public Vector3 targetPos;
    public Vector3 launchPos;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetPos = target.transform.position;
        launchPos = gameObject.transform.position;
        ballisticmissileRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ballisticmissileRB.velocity);
        launch();
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Distance from launch point to target
        Vector3 distance = target - origin;
        Vector3 distancexz = distance;
        distancexz.y = 0f;

        float h = distance.y;
        float xz = distancexz.magnitude;

        float Vxz = xz / time;
        float Vy = h / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distancexz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    void launch()
    {
        Ray camRay = cam.ScreenPointToRay(targetPos);
        RaycastHit hit;

        if(Physics.Raycast(camRay,out hit, 100f))
        {
            Vector3 Vorigin = CalculateVelocity(hit.point, launchPos, 1f);

            gameObject.transform.rotation = Quaternion.LookRotation(Vorigin);

            ballisticmissileRB.velocity = Vorigin;
        }        
    }
}
