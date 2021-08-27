/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Calls ChangeSound on the Orchestrion.                           |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class NumberButton : ActionObject
{
    public Orchestrion m_orchestrion;
    public int m_number;
    /*===============================================Fmod====================================================
    |   The button click sound will be stored here.                                                         |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start ()
    {
        /*===============================================Fmod====================================================
        |   Get the attached button Emitter.                                                                    |
        =======================================================================================================*/
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	void Update ()
    {
	
	}

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_orchestrion.ChangeSound(m_number);
        /*===============================================Fmod====================================================
        |   Play the click sound.                                                                               |
        =======================================================================================================*/
        m_buttonEvent.Play();
    }
}