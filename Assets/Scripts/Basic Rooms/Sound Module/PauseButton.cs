/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Module                                                    |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Calls Pause on the Orchestrion.                                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PauseButton : ActionObject
{
    public Orchestrion m_orchestrion;
    /*===============================================Fmod====================================================
    |   The button click sound will be stored here.                                                         |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start()
    {
        InitButton();
        InitGlow();
        /*===============================================Fmod====================================================
        |   Get the attached button Emitter.                                                                    |
        =======================================================================================================*/
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_orchestrion.Pause();
        /*===============================================Fmod====================================================
        |   Play the click sound.                                                                               |
        =======================================================================================================*/
        m_buttonEvent.Play();
    }
}