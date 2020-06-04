using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector3 cpPosition;

    public LineRenderer lineRenderer;
    private Vector3[] positions=new Vector3[50];
    private int numpoints = 40;

    private void Start()
    {       
        lineRenderer =gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = numpoints;        
    }
    private void Update()
    {
        drawLineCurve();
    }

    private void OnDrawGizmos()
    {        
        for (float t = 0; t <= 1; t += 0.05f)
        {
            cpPosition = calculateTrajectory(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3],t);
            Gizmos.DrawSphere(cpPosition, 0.25f);
        }
        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z),
            new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z),
            new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));  
    }

    public Vector3 calculateTrajectory(Transform cp0, Transform cp1, Transform cp2, Transform cp3, float tparam)
    {
        Vector3 CalculationResult;
        CalculationResult= Mathf.Pow(1f - tparam, 3) * cp0.position +
                       3 * Mathf.Pow(1f - tparam, 2) * tparam * cp1.position +
                       3 * (1 - tparam) * Mathf.Pow(tparam, 2) * cp2.position +
                       Mathf.Pow(tparam, 3) * cp3.position;
        return CalculationResult;
    }

    public void drawLineCurve()
    {

        for (int i=1;i<numpoints+1;i++)
        {
            float t = i / (float)numpoints;
            positions[i - 1] = calculateTrajectory(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3], t);
        }
        lineRenderer.SetPositions(positions);
    }
}
