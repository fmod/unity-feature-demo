/*===============================================================================================
|   Project:		            FMOD Unity Plugin Demo                                          |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                18/10/2016                                                      |
|   Scene:                      Snapshot Room                                                   |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                When the player falls down off the catwalk, on landing on the   |
|                               target, play the hurt sound & the snapshot. Then after the      |
|                               timer, move the player back up to the catwalk and turn off the  |
|                               hurt sound and snapshot.                                        |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PlayerHurt : MonoBehaviour
{
    [FMODUnity.EventRef]    public string m_event;      // Sound event to play after player falls down.
    FMOD.Studio.EventInstance m_hurtEvent;              
    [SerializeField]    Transform m_resetPosition;      // Position to reset the player back to.
    float m_resetTimer = 4.0f;                          // Time before the play is reset to the catwalk, set to 4 seconds to match animation.
    
	void Start ()
    {
        //---------------------------------Fmod-------------------------------
        //           If the event has been set, create an instance.
        //--------------------------------------------------------------------
        if (m_event != "")
        {
            m_hurtEvent = FMODUnity.RuntimeManager.CreateInstance(m_event);
        }
        
	}
	
    /// <summary>
    /// If colliding with the Player, start the coroutine to play the hurt event/s and reset the players position.
    /// </summary>
    /// <param name="col"></param>
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetPlayerAfterTimer(col.gameObject));
        }
    }

    /// <summary>
    /// Lock the play movement, play the screen effect animation and start the event/s. After the timer, reset the players position and stop the event/s.
    /// </summary>
    /// <param name="a_player"></param>
    /// <returns></returns>
    IEnumerator ResetPlayerAfterTimer(GameObject a_player)
    {
        // Lock player controls/position.
        a_player.GetComponent<ActorControls>().DisableMovement = true;
        Camera.main.GetComponent<Animator>().Play("RedScreen",0, 0.0f);

        // Display hurt screen overlay on camera

        //---------------------------------Fmod-------------------------------
        //           If the event has been set, start the event.
        //--------------------------------------------------------------------
        if (m_event != "")
            m_hurtEvent.start();

        // Wait for timer.
        yield return new WaitForSeconds(m_resetTimer);

        //---------------------------------Fmod-------------------------------
        //           If the event has been set, stop the event.
        //--------------------------------------------------------------------
        if (m_event != "")
            m_hurtEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        // Reset players position.
        a_player.transform.position = m_resetPosition.position;
        a_player.GetComponent<ActorControls>().DisableMovement = false;
    }
}
