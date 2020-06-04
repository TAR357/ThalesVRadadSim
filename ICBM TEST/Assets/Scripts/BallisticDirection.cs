using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticDirection : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target);
    }
}
