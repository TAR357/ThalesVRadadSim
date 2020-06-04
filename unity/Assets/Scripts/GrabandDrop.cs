using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabandDrop : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Grabable m_CurrentGrabable = null;
    [SerializeField] private List<Grabable> m_ContactGrabables = new List<Grabable>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }
    private void Update()
    {
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + "GRAB");
            Grab();
        }

        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + "DROP");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable"))        
        return;
        
        
        m_ContactGrabables.Add(other.gameObject.GetComponent<Grabable>());        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
            return;


        m_ContactGrabables.Remove(other.gameObject.GetComponent<Grabable>());
    }

    public void Grab()
    {

    }

    public void Drop()
    {

    }

    private Grabable NearestGrabable()
    {
        return null;
    }
}
