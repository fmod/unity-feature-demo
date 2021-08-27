/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Used by other scripts to change parameters of EventInstance.    |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class SG_MainSpeaker : MonoBehaviour
{
    /*===============================================FMOD====================================================
    |   Call this to display the string in Unity Inspector with the FMOD Finder.                            |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_musicPath;
    /*===============================================FMOD====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_music;
    /*===============================================FMOD====================================================
    |   PARAMETER_ID - Used to reference a parameter stored in EventInstance. Example use case: changing    |
    |   from wood to carpet floor.                                                                          |
    =======================================================================================================*/
    FMOD.Studio.PARAMETER_ID m_resultParamID;
    FMOD.Studio.PARAMETER_ID m_roundsParamID;

    void Start()
    {
        /*===============================================FMOD====================================================
        |   Calling this function will create an EventInstance. The return value is the created instance.       |
        =======================================================================================================*/
        m_music = FMODUnity.RuntimeManager.CreateInstance(m_musicPath);
        /*===============================================FMOD====================================================
        |   Store the parameter IDs for later use.                                                              |
        =======================================================================================================*/
        FMOD.Studio.EventDescription musicDesc;
        m_music.getDescription(out musicDesc);
        FMOD.Studio.PARAMETER_DESCRIPTION paramDesc;
        
        musicDesc.getParameterDescriptionByName("Result", out paramDesc);
        m_resultParamID = paramDesc.id;

        musicDesc.getParameterDescriptionByName("Round", out paramDesc);
        m_roundsParamID = paramDesc.id;
    }

    public void SetGameResult(int a_value)
    {
        /*===============================================FMOD====================================================
        |   When these functions are called. It will change the parameter inside EventInstance to the passed in |
        |   value, changing the behaviour of the sound.                                                         |
        =======================================================================================================*/
        m_music.setParameterByID(m_resultParamID, a_value);
    }
    public void Play()
    {
        /*===============================================FMOD====================================================
        |   Calling this function will start the EventInstance.                                                 |
        =======================================================================================================*/
        m_music.start();
    }
    public void Stop()
    {
        /*===============================================Fmod====================================================
        |   This function stops the event. It takes in a parameter of type FMOD.Studio.STOP_MODE. Used for      |
        |   stopping events gradually rather than instantly.                                                    |
        =======================================================================================================*/
        m_music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void Pause(bool a_value)
    {
        m_music.setPaused(a_value);
    }
    public void SetRound(int a_value)
    {
        m_music.setParameterByID(m_roundsParamID, a_value);
    }
}
