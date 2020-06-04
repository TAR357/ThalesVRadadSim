using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScaler : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 100f)]
    public float Scale;
    [SerializeField]
    public float rocketSpeed;
    private float tempRocketSpeed;
    public Vector3[] cp;
    private Transform Trajectory;

    private void Start()
    {
        rocketSpeed = 0.01f;
        Trajectory = GameObject.FindGameObjectWithTag("trajectory").GetComponent<Transform>();
        Trajectory = gameObject.GetComponent<Transform>();
        cp = new Vector3[Trajectory.childCount];
        for(int i=0;i<Trajectory.childCount;i++)
        {
            cp[i] = Trajectory.GetChild(i).position;
        }
        Scale = 1f;
        tempRocketSpeed = rocketSpeed;
    }

    void Update()
    {
        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Vector3 newPosition = scalePosition(cp[i]);
            Trajectory.GetChild(i).position = newPosition;
            //Debug.Log(cp[i]);
        }
        rocketSpeed = scaleSpeed(tempRocketSpeed);
    }

    public Vector3 scalePosition(Vector3 pointLocation)
    {
        Vector3 ScaleResult;
        ScaleResult = pointLocation * Scale;
        Debug.Log(ScaleResult);
        return ScaleResult;
    }

    public float scaleSpeed(float defaultspeed)
    {
        float scaledSpeed;
        scaledSpeed = defaultspeed * Scale;
        return scaledSpeed;
    }

}
