using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravObj : MonoBehaviour
{
    public FauxGravAttractor Attractor;
    private Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.transform;
        Attractor = FindObjectOfType<FauxGravAttractor>();
    }

    // Update is called once per frame
    void Update()
    {
        Attractor.Attract(myTransform);
    }
}
