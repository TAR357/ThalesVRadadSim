using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// If this script is added to any gameobject it could simply reload the scene but only if the scene was set in the build settings.
/// </summary>
public class ResetScene : MonoBehaviour
{
    // Start is called before the first frame update

    public SteamVR_Action_Boolean resetScene;

    private SteamVR_Behaviour_Pose m_Pose = null;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetScene.GetStateUp(m_Pose.inputSource))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
    }
}
