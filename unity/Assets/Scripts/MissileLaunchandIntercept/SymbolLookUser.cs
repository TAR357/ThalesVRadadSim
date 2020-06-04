using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolLookUser : MonoBehaviour
{
    Transform SymbolTransform;
    public Transform User;
    // Start is called before the first frame update
    void Start()
    {
        User = GameObject.FindGameObjectWithTag("MainCamera").transform;
        SymbolTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SymbolTransform.transform.LookAt(User);
    }
}
