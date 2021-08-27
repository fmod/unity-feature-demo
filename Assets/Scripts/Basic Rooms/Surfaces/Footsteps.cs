/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Create oneshot sounds of footsteps and setting their parameters |
|   based on the type of floor currently set.                                                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    float m_currentParamValue;  /* carpet = 1.0f, grass = 2.0f, tile = 3.0f */
    float m_reverbValue;

    /*===============================================Fmod====================================================
    |   This piece of code will allow the string m_footstepSurfaceName to use the event browser to select   |
    |   the event, in the inspector.                                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================Fmod====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_footstepSurfaceName;
    
    void Start()
    {
        m_currentParamValue = 0.0f;
    }
    void Update()
    {

    }

    public void PlayFootstep(bool a_isRunning)
    {
        /*===============================================Fmod====================================================
        |   When the actors walking, create an instance of the footstep sound at every step. Creating a sound   |
        |   this way is known as a one shot sound, where we create an instance and release it, no longer        |
        |   in control of that instance.                                                                        |
        =======================================================================================================*/
        if (m_currentParamValue >= 1.0f)
        {
            /*===============================================Fmod====================================================
            |   The CreateInstance function takes in the name of the event as a parameter,                          |
            |   e.g. "event:/Basic Rooms/Footsteps".                                                                |
            |   This will simply create an instance.                                                                |
            =======================================================================================================*/
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(m_footstepSurfaceName);
            /*===============================================Fmod====================================================
            |   The setParameterByName function takes in the name of the parameter, and the value to give it.       |
            |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
            =======================================================================================================*/
            instance.setParameterByName("Surface", m_currentParamValue);
            instance.setParameterByName("Room Verb", m_reverbValue);
            instance.setParameterByName("Running", a_isRunning ? 0.0f : 1.0f);
            /*===============================================Fmod====================================================
            |   The start function will simply run the event.                                                       |
            =======================================================================================================*/
            instance.start();
            /*===============================================Fmod====================================================
            |   The set3DAttributes function is used to set the position of the audio. A Transform and rigidbody    |
            |   can be attached, instead of a Vector3 position, to the event, which will allow the sound to follow  |
            |   that transform.                                                                                     |
            =======================================================================================================*/
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position - new Vector3(0, transform.localScale.y, 0)));
            /*===============================================Fmod====================================================
            |   The release function will remove control, which means calling functions such as setParamaterValue   |
            |   will do nothing.                                                                                    |
            =======================================================================================================*/
            instance.release();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit a_hit)
    {
        m_reverbValue = 0.0f;
        string name = a_hit.gameObject.name;
        string tag = a_hit.gameObject.tag;
        /*===============================================Fmod====================================================
        |   If the type of floor is either grass, wood or carpet, the next footstep will play the correct sound.|
        =======================================================================================================*/
        if (name.Contains("Grass") || tag == "Grass")
        {
            m_currentParamValue = 2.0f;
        }
        else if (name.Contains("Carpet") || tag == "Carpet")
        {
            m_currentParamValue = 1.0f;
        }
        else if (name.Contains("Tile") || tag == "Tile")
        {
            m_currentParamValue = 3.0f;
        }
        else if (name.Contains("Mud") || tag == "Mud")
        {
            m_currentParamValue = 4.0f;
        }
        else if (name.Contains("Wood") || tag == "Wood")
        {
            m_currentParamValue = 5.0f;
        }
        else if (tag == "TileReverb")
        {
            m_currentParamValue = 3.0f;
            m_reverbValue = 1.0f;
        }
        else
        {
            m_currentParamValue = 0.0f;
        }
    }
}