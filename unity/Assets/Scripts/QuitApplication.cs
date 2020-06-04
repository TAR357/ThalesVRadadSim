using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A quick way to quit the build instead of Alt F4
/// </summary>
public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
