using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InterceptionMissile : MonoBehaviour
{
    public Transform target;

    private Vector3 direction;

    public float speed=10f;
    public float rotateSpeed=10f;
    private void Start()
    {
        target=GameObject.FindGameObjectWithTag("BallisticMissile").GetComponent<Transform>();
    }
    private void Update()
    {
        //Movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //MissileDirection
        direction = target.position - transform.position;

        direction.Normalize();
        var missileRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, missileRotation, rotateSpeed * Time.deltaTime);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="BallisticMissile")
        {
            Destroy(collision.gameObject);
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }
    }
}
