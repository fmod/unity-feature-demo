/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Transceiver                                                     |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Toggles the speakers, On/Off.                                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class TransceiverToggle : ActionObject
{
    /*===============================================Fmod====================================================
    |   The speakers event.                                                                                 |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_eventEmitter;
    void Start()
    {
        InitGlow();
        /*===============================================Fmod====================================================
        |   Get the event component and store it.                                                               |
        =======================================================================================================*/
        m_eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        UpdateGlow();
    }
    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        /*===============================================Fmod====================================================
        |   If the event is playing, set the parameter of the event to 0.0f. This will turn down the volume of  |
        |   the transceiver and the volume of the static noises.                                                |
        |   Otherwise turn it on.                                                                               |
        =======================================================================================================*/
        if (m_eventEmitter.IsPlaying())
        {
            m_eventEmitter.SetParameter("On", 0.0f);
            m_eventEmitter.Stop();
        }
        else
        {
            m_eventEmitter.Play();
            m_eventEmitter.SetParameter("On", 1.0f);
        }
    }
}