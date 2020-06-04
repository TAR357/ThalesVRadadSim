using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    const float G = 0.6674f;
    public GameObject planet;
    public Rigidbody planetRB;
    public Rigidbody obj;

    private void Start()
    {
        obj = gameObject.GetComponent<Rigidbody>();
        planet = GameObject.FindGameObjectWithTag("Earth");
        planetRB = planet.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        realisticgravity();
    }

    void realisticgravity()
    {
        Vector3 vectorRange = planetRB.position - obj.position;

        float range = Mathf.Abs(vectorRange.magnitude);

        float forceFormula = G * (planetRB.mass * obj.mass) / Mathf.Pow(range, 2);

        Vector3 force = vectorRange.normalized * forceFormula;

        obj.AddForce(force);
    }
}
