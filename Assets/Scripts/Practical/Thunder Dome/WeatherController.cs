/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Sets the parameters of the Event Instance from the individual   |
|   controllers.                                                                                |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour
{
    public WindSlider m_windController;
    public RainSlider m_rainController;
    public SunController m_sunController;

    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public FMODUnity.EventReference m_ambienceRef;

    /*===============================================FMOD====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_ambience;

    /*===============================================FMOD====================================================
    |   PARAMETER_ID - Used to reference a parameter stored in EventInstance. Example use case: changing    |
    |   from wood to carpet floor.                                                                          |
    =======================================================================================================*/
    FMOD.Studio.PARAMETER_ID m_windParamID;
    FMOD.Studio.PARAMETER_ID m_rainParamID;
    FMOD.Studio.PARAMETER_ID m_sunParamID;
    FMOD.Studio.PARAMETER_ID m_waterParamID;
    public float Rain { get { return m_rainController.RainValue; } }

    void Start()
    {
        /*===============================================FMOD====================================================
        |   Calling this function will create an EventInstance. The return value is the created instance.       |
        =======================================================================================================*/
        m_ambience = FMODUnity.RuntimeManager.CreateInstance(m_ambienceRef);
        /*===============================================FMOD====================================================
        |   Calling this function will start the EventInstance.                                                 |
        =======================================================================================================*/
        m_ambience.start();
        m_ambience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        /*===============================================FMOD====================================================
        |   Store the parameter IDs for later use.                                                              |
        =======================================================================================================*/
        FMOD.Studio.EventDescription ambienceDesc;
        m_ambience.getDescription(out ambienceDesc);
        FMOD.Studio.PARAMETER_DESCRIPTION paramDesc;

        ambienceDesc.getParameterDescriptionByName("Wind", out paramDesc);
        m_windParamID = paramDesc.id;

        ambienceDesc.getParameterDescriptionByName("Rain", out paramDesc);
        m_rainParamID = paramDesc.id;

        ambienceDesc.getParameterDescriptionByName("Water", out paramDesc);
        m_waterParamID = paramDesc.id;

        ambienceDesc.getParameterDescriptionByName("Time", out paramDesc);
        m_sunParamID = paramDesc.id;
    }

    void Update()
    {
        /*===============================================FMOD====================================================
        |   This function is used to set the ParameterInstance value.                                           |
        =======================================================================================================*/
        m_ambience.setParameterByID(m_windParamID,  m_windController.WindValue);
        m_ambience.setParameterByID(m_rainParamID,  m_rainController.RainValue);
        m_ambience.setParameterByID(m_waterParamID, m_rainController.WaterValue);
        m_ambience.setParameterByID(m_sunParamID,   m_sunController.SunValue);
    }

    void OnDestroy()
    {
        /*===============================================Fmod====================================================
        |   This function stops the event. It takes in a parameter of type FMOD.Studio.STOP_MODE. Used for      |
        |   stopping events gradually rather than instantly.                                                    |
        =======================================================================================================*/
        m_ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
