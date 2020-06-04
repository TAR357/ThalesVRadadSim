using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravAttractor : MonoBehaviour
{
    public float gravity = -50f;
    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 objUp = body.up;

        body.gameObject.GetComponent<Rigidbody>().AddForce(gravityUp *= gravity);
    }
}
