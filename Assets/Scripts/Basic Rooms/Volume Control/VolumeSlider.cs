/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Volume Control                                                  |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                From a script you can easily adjust the volume of an event using|
|   the setVolume(float) parameter.                                                             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class VolumeSlider : ActionObject
{
    [FMODUnity.EventRef]
    public string m_eventPath;
    FMOD.Studio.EventInstance m_event;

    public Transform m_soundPosition;
    public Vector3 m_startPosition;
    public Vector3 m_endPosition;
    bool m_isActive;

    void Start()
    {
        /*===============================================FMOD====================================================
        |   The CreateInstance function takes in the name of the event as a parameter,                          |
        |   e.g. "event:/Basic Rooms/Footsteps".                                                                |
        |   This will simply create an instance of the event.                                                   |
        =======================================================================================================*/
        m_event = FMODUnity.RuntimeManager.CreateInstance(m_eventPath);
        /*===============================================FMOD====================================================
        |   This will play the event.                                                                           |
        =======================================================================================================*/
        m_event.start();
        /*===============================================FMOD====================================================
        |   If a 3D event needs to follow a gameobject, use this function. It has the optional rigidbody        |
        |   parameter, which is used to calculate where a sound will be.                                        |
        =======================================================================================================*/
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_event, m_soundPosition, GetComponent<Rigidbody>());

        float diff = (transform.position - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
        diff = Mathf.Clamp(diff, 0.0f, 1.0f);
        /*===============================================FMOD====================================================
        |   This is the function that will set an events volume.                                                |
        =======================================================================================================*/
        m_event.setVolume(Mathf.Lerp(0.0f,1.0f, diff));

        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
        if (Input.GetKeyUp(m_actionKeys[0]))
        {
            m_isActive = false;
        }
        if (m_isActive)
        {
            //It is done here and not in the ActionDown function, because If the player is no longer looking at the object but is still holding down the key, the slider will still move.
            RaycastHit rh;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rh, Mathf.Infinity, ~(1 << 10 | 1 << 2)))
            {
                if (rh.collider.gameObject.name == "Radio")
                {
                    //Slider based on movement calculations
                    float diff = 0.0f;
                    float dot = Vector3.Dot((rh.point - m_startPosition).normalized, (m_endPosition - m_startPosition).normalized);
                    if (dot > 0)
                    {
                        diff = (rh.point - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
                    }
                    transform.position = Vector3.Lerp(m_startPosition, m_endPosition, diff * dot);
                    /*===============================================FMOD====================================================
                    |   This is the function that will set an events volume.                                                |
                    =======================================================================================================*/
                    m_event.setVolume(Mathf.Lerp(0.0f, 1.0f, diff * dot));
                }
            }
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isActive = true;
    }

    void OnDestroy()
    {
        /*===============================================FMOD====================================================
        |   This will fadeout the sound to a complete stop.                                                     |
        =======================================================================================================*/
        m_event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        /*===============================================FMOD====================================================
        |   This will destroy the event when it has been stopped. Freeing up any resources that it no longer    |
        |   needs.                                                                                              |
        =======================================================================================================*/
        m_event.release();
    }
}