using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 100f)]
    public float Scale;
    public Transform Trajectory;
    public Vector3[] obj;
    public float scaleChecker = 1f;
    // Start is called before the first frame update
    void Start()
    {
        obj = new Vector3 [Trajectory.childCount];
        Trajectory = gameObject.GetComponent<Transform>();
        Debug.Log(Trajectory.childCount);
        for(int i = 0; i < Trajectory.childCount; i++)
        {
            obj[i] = Trajectory.GetChild(i).position;
            Debug.Log(obj[i]);
        }        
        Scale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Vector3 newPosition = scalePosition(obj[i]);
            Trajectory.GetChild(i).position=newPosition;
            Debug.Log(obj[i]);
        }
    }

    public Vector3 scalePosition(Vector3 OBJ)
    {
        Vector3 ScaleResult;
        ScaleResult = OBJ * Scale;
        Debug.Log(ScaleResult);
        return ScaleResult;
    }
}
