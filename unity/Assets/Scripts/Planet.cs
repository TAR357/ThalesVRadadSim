using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Planet sets the moon as its child and changes the Scale according to the startingScale
/// </summary>
public class Planet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float startingScale = 20000;

    [SerializeField] private GameObject moon = null;

    void Start()
    {
        transform.localScale = Vector3.one * startingScale;

        if (moon != null)
        { 
            moon.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
