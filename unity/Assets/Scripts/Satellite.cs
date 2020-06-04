using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Satellite rotates around his parent giving with a rotation speed and the id.
/// 
/// 0 = random direction
/// 1 = down
/// 2 = none
/// 3 = right
/// 4 = up
/// 5 = forward
/// 6 = left
/// 7 = back
/// </summary>
public class Satellite : MonoBehaviour
{
    public int id = 1;

    [SerializeField] float rotationSpeed = 1.0f;

    private int randomNum;
    private Vector3 dir;

    void Start()
    {

        // when wanting to go into a random direction at the start these 0 should be used.
        if (id == 0)
        {
            randomNum = Random.Range(0, 4);

            if (randomNum == 0)
            {
                dir = Vector3.right;
            }

            else if (randomNum == 1)
            {
                dir = Vector3.left;
            }

            else if (randomNum == 2)
            {
                dir = Vector3.up;
            }

            else if (randomNum == 3)
            {
                dir = Vector3.down;
            }

            else if (randomNum == 4)
            {
                dir = Vector3.forward;
            }

            else if (randomNum == 5)
            {
                dir = Vector3.back;
            }
        }
        
        else if (id == 1)
        {
            dir = Vector3.down;
        }

        else if (id == 2)
        {
            dir = Vector3.zero;
        }

        else if (id == 3)
        {
            dir = Vector3.right;
        }

        else if (id == 4)
        {
            dir = Vector3.up;
        }

        else if (id == 5)
        {
            dir = Vector3.forward;
        }

        else if (id == 6)
        {
            dir = Vector3.left;
        }

        else if (id == 7)
        {
            dir = Vector3.back;
        }


        
    }
    // Update is called once per frame
    void Update()
    {
        //makes the satellites rotate around the earth.
        transform.RotateAround(transform.parent.transform.position, dir, Time.deltaTime * rotationSpeed);

    }
}
