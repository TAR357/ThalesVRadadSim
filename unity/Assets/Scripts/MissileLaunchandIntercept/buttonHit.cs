using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Grabable;
    bool pushed;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Grabable = GameObject.FindGameObjectWithTag("Interactable");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Grabable.GetComponent<Grabable>().m_Activated!=null)
        {
            animator.SetBool("Pushed", true);
            Grabable.GetComponent<Grabable>().action();
        }
    }

    public void release()
    {
        animator.SetBool("Pushed", false);
    }
}
