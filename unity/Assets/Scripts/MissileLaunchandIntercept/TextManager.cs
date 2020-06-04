using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private TextMeshProUGUI Status;
    private TextMeshProUGUI warning;
    public GameObject warningGO;
    public GameObject StatusGO;
    public GameObject Radar;    
    private GameObject BM;
    private GameObject IM;
    //private TextMeshProUGUI Warning;
    // Start is called before the first frame update
    void Start()
    {
        StatusGO = GameObject.Find("StatusPanel");
        warningGO = GameObject.Find("Warning");
        warning = warningGO.GetComponent<TextMeshProUGUI>();
        Status = GameObject.Find("Status").GetComponent<TextMeshProUGUI>();
        StatusGO.SetActive(false);
        warningGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BM = GameObject.FindGameObjectWithTag("BallisticMissile");
        IM = GameObject.FindGameObjectWithTag("IM");
        if(BM != null && IM==null)
        {
            StatusGO.SetActive(true);
            BMLaunched();           
        }        
        if(Radar.GetComponent<RadarDetection>().MissileDetected==true)
        {
            BMDetected();
            StartCoroutine("WarningLoop");
        }            
        
    }

    public void BMLaunched()
    {
        Status.text = "Ballistic Missile Launched";
    }   

    public void BMDetected()
    {
        Status.text = "Ballistic Missile Detected";
    }

    public void Impact()
    {
        Status.text = "Impact";
    }

    private IEnumerator WarningLoop()
    {
        warningGO.SetActive(true);       
        yield return new WaitForSeconds(10f);
        warningGO.SetActive(false);
        StopCoroutine("WarningLoop");
    }
}
