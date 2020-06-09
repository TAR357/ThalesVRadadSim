using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// The inputchecker has to be on both the controller and needs the RigControl to work accordingly.
/// </summary>
public class ControllerInput : MonoBehaviour
{
    
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]private GameObject ParentOrigin;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Grabable m_CurrentGrabable = null;
    [SerializeField] private List<Grabable> m_ContactGrabables = new List<Grabable>();

    private bool itemGrabbed = false;

    public SteamVR_Action_Boolean m_touchpadAction=null;
    public SteamVR_Action_Boolean m_gripAction = null;

    [SerializeField] private GameObject leftController = null;
    [SerializeField] private GameObject rightController = null;
    [SerializeField] private RigControl playerRig = null;

    [HideInInspector] private SteamVR_Input_Sources leftInput = SteamVR_Input_Sources.LeftHand;
    [HideInInspector] private SteamVR_Input_Sources rightInput = SteamVR_Input_Sources.RightHand;

    private void Awake()
    {
        ParentOrigin = GameObject.Find("Trigger");
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    void Start()
    {
        //sets the trakcing object
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        //checks which controllers is being used during the which sets the boolean on the respective controller to true or false when the trigger is used
        //Debug.Log(itemGrabbed);
        if (gameObject == leftController)
        {
            if (SteamVR_Actions._default.Squeeze.GetAxis(leftInput) == 1)
            {
                playerRig.isLeftGripped = true;                             
            }

            else
            {
                playerRig.isLeftGripped = false;               
            }
        }

        else if (gameObject == rightController)
        {
            if (SteamVR_Actions._default.Squeeze.GetAxis(rightInput) == 1)
            {
                playerRig.isRightGripped = true;                
            }
            
            else
            {
                playerRig.isRightGripped = false;                
            }
        }
        if(itemGrabbed==true)
        {
            if (m_touchpadAction.GetStateDown(m_Pose.inputSource))
            {
                Debug.Log("ButtonClicked");
                if (itemGrabbed == true)
                {
                    m_CurrentGrabable.action();
                }
            }
        }

        if (m_gripAction.GetStateDown(m_Pose.inputSource))
        {
            if (m_ContactGrabables.Count != 0)
            {
                if (itemGrabbed == false)
                {
                    StartCoroutine("Grabbing");
                    StopCoroutine("Dropping");
                }
                if (itemGrabbed == true)
                {
                    StopCoroutine("Grabbing");
                    StartCoroutine("Dropping");
                }
            }
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactGrabables.Add(other.gameObject.GetComponent<Grabable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactGrabables.Remove(other.gameObject.GetComponent<Grabable>());
    }    

    IEnumerator Grabbing()
    {
        m_CurrentGrabable = NearestGrabable();
       
        // Positioning
        //m_CurrentGrabable.transform.position = transform.position;
        m_CurrentGrabable.applyoffset(transform);
        //Attach
        Rigidbody targetBody = m_CurrentGrabable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        //Activate
        m_CurrentGrabable.m_Activated = this;
        m_CurrentGrabable.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(1f);
        itemGrabbed = true;       
    }

    IEnumerator Dropping()
    {
        //Dettach
        m_CurrentGrabable.transform.SetParent(ParentOrigin.transform);
        m_Joint.connectedBody = null;
        //Deactivate
        m_CurrentGrabable.m_Activated = null;
        yield return new WaitForSeconds(1f);
        itemGrabbed = false;
    }

    

    private Grabable NearestGrabable()
    {
        Grabable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;
        foreach(Grabable grabables in m_ContactGrabables)
        {
            distance = (grabables.transform.position - transform.position).sqrMagnitude;
            if(distance<minDistance)
            {
                minDistance = distance;
                nearest = grabables;
            }
        }    
        return nearest;
    }
}
