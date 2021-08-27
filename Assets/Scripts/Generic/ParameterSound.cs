/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                A script that creates an EventInstance and can set parameters.  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class ParameterSound : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   This piece of code will allow the string to use the event browser to select                         |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================Fmod====================================================
    |   Name of Event. Used in creation of the EventInstance.                                               |
    =======================================================================================================*/
    public string m_eventPath;
    /*===============================================Fmod====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_event;

    public bool m_startEventOnCreation;

    void Start()
    {
        /*===============================================Fmod====================================================
        |   The CreateInstance function takes in the name of the event as a parameter,                          |
        |   e.g. "event:/Basic Rooms/Footsteps".                                                                |
        |   This will simply create an instance.                                                                |
        =======================================================================================================*/
        m_event = FMODUnity.RuntimeManager.CreateInstance(m_eventPath);

        if(m_startEventOnCreation)
            Start();
    }

    //Starts the event, then attaches it to the gameObject
    public void StartEvent()
    {
        /*===============================================Fmod====================================================
        |   The start function will simply run the event.                                                       |
        =======================================================================================================*/
        m_event.start();
        /*===============================================Fmod====================================================
        |   The AttachInstanceToGameObject function is used to set the position of the audio.                   |
        =======================================================================================================*/
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_event, transform, GetComponent<Rigidbody>());
    }
    //Stops the event and takes in a STOP_MODE as a parameter, which controls the way the sound event stops
    public void StopEvent(FMOD.Studio.STOP_MODE a_stopMode)
    {
        /*===============================================Fmod====================================================
        |   This function stops the event. It takes in a parameter of type FMOD.Studio.STOP_MODE. Used for      |
        |   stopping events gradually rather than instantly.                                                    |
        =======================================================================================================*/
        m_event.stop(a_stopMode);
    }
    //Sets a parameter of the event, takes in the parameter to set and the value to set it at
    public void SetParameter(string a_parameter, float a_value)
    {
        /*===============================================Fmod====================================================
        |   The setParameterByName function takes in the name of the parameter, and the value to give it.       |
        |   Parameters can be used to change volumes, or to jump to sections in the sound, etc.                 |
        =======================================================================================================*/
        m_event.setParameterByName(a_parameter, a_value);
    }
}