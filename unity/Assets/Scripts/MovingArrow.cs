using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the arrow move back and forth
/// </summary>
public class MovingArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2;
    private float dir = 1;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x >= 1)
        {
            dir = -1;
        }

        else if (transform.localPosition.x <= -1)
        {
            dir = 1;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * dir * speed, transform.localPosition.y, transform.localPosition.z);
    }
}
