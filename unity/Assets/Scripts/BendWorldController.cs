using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BendWorldController controls the shader, Adjustablecurvedworld.
/// 
/// With this script you can adjust the parameters for the shader,
/// 
/// making it more curvy or completely flat
/// </summary>
[ExecuteInEditMode]
public class BendWorldController : MonoBehaviour
{
    [SerializeField] private Transform curveOrigin = null;
    [SerializeField] private Transform referenceDirection = null;
    [SerializeField] private float curvature = 0f;

    [Range(0.5f, 3f)]
    [SerializeField] private float scaleX = 1f;
    [Range(0.5f, 3f)]
    [SerializeField] private float scaleZ = 1f;

    [SerializeField] private float flatMargin = 0f;

    private int curveOriginId = 0;
    private int referenceDirectionId = 0;
    private int curvatureId = 0;
    private int scaleId = 0;
    private int flatMarginId = 0;

    private Vector3 scale = Vector3.zero;
    void Start()
    {
        curveOriginId = Shader.PropertyToID("_CurveOrigin");
        referenceDirectionId = Shader.PropertyToID("_ReferenceDirection");
        curvatureId = Shader.PropertyToID("_Curvature");
        scaleId = Shader.PropertyToID("_Scale");
        flatMarginId = Shader.PropertyToID("_FlatMargin");
    }

    // Update is called once per frame
    void Update()
    {
        scale.x = scaleX;
        scale.z = scaleZ;

        Shader.SetGlobalVector(curveOriginId, curveOrigin.position);
        Shader.SetGlobalVector(referenceDirectionId, referenceDirection.forward);
        Shader.SetGlobalFloat(curvatureId, curvature * 0.00001f);
        Shader.SetGlobalVector(scaleId, scale);
        Shader.SetGlobalFloat(flatMarginId, flatMargin);
    }

    /// <summary>
    /// Sets the margin for the shader

    /// </summary>
    /// <param name="margin">margin in float </param>
    public void SetMargin(float margin)
    {
        flatMargin = margin;
    }

    /// <summary>
    /// Sets the Curvatur for the shader
    /// </summary>
    /// <param name="curv">curv in float</param>
    public void SetCurvatur(float curv)
    {
        curvature = curv;
    }

    /// <summary>
    /// Sets the x and y scaling for the shader
    /// </summary>
    /// <param name="x">x in float</param>
    /// <param name="z">y in float</param>
    public void SetScaleXZ(float x, float z)
    {
        scaleX = x;
        scaleZ = z;
    }

    /// <summary>
    /// If the bender is disabled it will change the shader back to normal.
    /// </summary>
    private void OnDisable()
    {
        Shader.SetGlobalVector(curveOriginId, Vector3.zero);
        Shader.SetGlobalFloat(curvatureId, 0);
    }
}
