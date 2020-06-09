using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Vector3 m_offset=Vector3.zero;
    public Vector3 m_offsetBody = Vector3.zero;
    private GameObject LaunchPt;
    private GameObject mainCamera;
    private GameObject Button;
    private GameObject BM;
    private Transform Socket;
    public float coolDownTimer;
    public Material ButtonM;
    public bool Clicked=false;
    

    [HideInInspector]
    public ControllerInput m_Activated = null;

    private void Awake()
    {
        coolDownTimer = 5f;
        Button = GameObject.FindGameObjectWithTag("Button");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        LaunchPt = GameObject.FindGameObjectWithTag("BMLaunchPoint");
        Socket = GameObject.Find("Socket").GetComponent<Transform>();
        ButtonM.color = Color.green;
    }

    public void Update()
    {
        if(!m_Activated)
        {
            transform.position = Socket.position;
            transform.rotation = Socket.rotation;
        }
        BM = GameObject.FindGameObjectWithTag("BallisticMissile");  
        
        if(Clicked==false)
        {
            ButtonM.color = Color.green;
            Button.GetComponent<buttonHit>().release();
            StopCoroutine(Countdount());
        }
        if(Clicked==true)
        {
            ButtonM.color = Color.red;
            StartCoroutine(Countdount());
        }
    }


    public virtual void action()
    {
        if(Clicked==false)
        {
            LaunchPt.GetComponent<LaunchBM>().ButtonLaunch();
            ButtonM.color = Color.red;
            Clicked = true;
        }
    }

    public void applyoffset(Transform hand)
    {
        transform.SetParent(hand);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = m_offset;
        transform.SetParent(null);
    }

    private IEnumerator Countdount()
    {
        yield return new WaitForSeconds(coolDownTimer);
        Clicked = false;       
    }
}
