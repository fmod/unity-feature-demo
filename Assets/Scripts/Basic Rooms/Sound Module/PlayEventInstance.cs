/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			15/08/2016
==================================================================*/

using UnityEngine;


public class PlayEventInstance : MonoBehaviour 
{
    // Public Vars

    //---------------------------------Fmod-------------------------------
    //  Using the EventRef attribute will present the event selection
    //  UI in the Unity Editor.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    public string m_eventString;

    // Private Vars

    //---------------------------------Fmod-------------------------------
    //  Using the EventInstance class will allow us to manage an event
    //  over it's lifetime. Including starting, stopping and changing
    //  parameters.
    //--------------------------------------------------------------------
    FMOD.Studio.EventInstance m_eventInstance;

    //---------------------------------Fmod-------------------------------
    //  Playback state which will be queried before we play or stop.
    //--------------------------------------------------------------------
    FMOD.Studio.PLAYBACK_STATE m_playState;

    //---------------------------------Fmod-------------------------------
    //  Pausing uses bool checks rather than enum like play and stop.
    //--------------------------------------------------------------------
    bool m_isPaused = false;

    //---------------------------------Fmod-------------------------------
    //  Used for error checking: many FMOD functions return a RESULT.
    //--------------------------------------------------------------------
    FMOD.RESULT m_result;

    void Start () 
	{
        //---------------------------------Fmod-------------------------------
        //  This shows how to create an instance of an Event.
        //  CreateInstance takes in a string/path to sound, and gives back an
        //  FMOD Event.
        //--------------------------------------------------------------------
        m_eventInstance = FMODUnity.RuntimeManager.CreateInstance(m_eventString);
	}

    void OnDestroy()
    {
        //---------------------------------Fmod-------------------------------
        //  Release resources when this Unity object is destroyed.
        //--------------------------------------------------------------------
        m_eventInstance.release();
    }

	public void PlaySound()
    {
        //---------------------------------Fmod-------------------------------
        //  Check the current PlayBackState, to make sure the sound isn't
        //  already playing.
        //--------------------------------------------------------------------
        m_eventInstance.getPlaybackState(out m_playState);
        m_eventInstance.getPaused(out m_isPaused);

        if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            //---------------------------------Fmod-------------------------------
            //  Playing an Event is as simple as calling Event.start() 
            //--------------------------------------------------------------------
            m_eventInstance.start();
        }
        else if (m_isPaused)
        {
            //---------------------------------Fmod-------------------------------
            //  Pausing and Event is done by calling setPaused(true/false)
            //--------------------------------------------------------------------
            m_eventInstance.setPaused(false);
        }
    }

    public void PauseSound()
    {
        //---------------------------------Fmod-------------------------------
        //  Check the current PlayBackState, to make sure the sound isn't
        //  already playing.
        //--------------------------------------------------------------------
        m_eventInstance.getPaused(out m_isPaused);
        if (!m_isPaused && m_playState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            //---------------------------------Fmod-------------------------------
            //  Pausing and Event is done by calling setPaused(true/false)
            //--------------------------------------------------------------------
            m_eventInstance.setPaused(true);
        }
    }

    public void StopSound()
    {
        //---------------------------------Fmod-------------------------------
        //  Check the current PlayBackState, to make sure the sound is
        //  already playing.
        //--------------------------------------------------------------------
        m_eventInstance.getPlaybackState(out m_playState);
        m_eventInstance.getPaused(out m_isPaused);

        if (m_playState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            //---------------------------------Fmod-------------------------------
            //  Playing an Event is as simple as calling Event.stop()
            //  Although, there are two stopping modes.
            //  Immediate and Allowfadeout.
            //  Immediate - stops sound immediately.
            //  Allowfadeout - allows for the AHDSR set by the designer to control
            //  the fadeout.
            //--------------------------------------------------------------------
            m_eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
