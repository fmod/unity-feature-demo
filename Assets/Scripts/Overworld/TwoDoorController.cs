/*===============================================================================================
|   Project:                    FMOD Unity Plugin Demo                                          |
|   Developer:                  Cameron Baron                                                   |
|   Company:                    Firelight Technologies                                          |
|   Date:                       26/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Controls the doors opening and closing as well as               |
|                               loading and unload scenes and FMOD Studio banks.                |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class TwoDoorController : ActionObject
{
    // Public Vars
    public GameObject m_upperDoor;      // Ref to upper door object that gets moved.
    public GameObject m_lowerDoor;      // Ref to lower door object that gets moved.

    public string m_sceneToLoad;        // Name of scene to be loaded/unloaded (needs to be exact).
    public float m_doorHoldTime;        // Amount of time the door will stay open after leaving the trigger area before closing.
    public float m_unloadDelay;         // Time until scene is unloaded once door has fully closed.
    
    /*===============================================Fmod====================================================
    |                   Bank reference is the same as and Event, except using BankRef.                      |
    |                   Another difference between Events and Banks is loading and                          |
    |                   unloading is done using the string and not a class or instance.                     |
    =======================================================================================================*/
    [FMODUnity.BankRef]
    public string m_bankToLoad;

    /*===============================================Fmod====================================================
    |                              Event doors play when opening and closing.                               |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_doorSound;
    [Range(0, 4)]    public float m_doorReverb = 0.4f;

    /*===============================================Fmod====================================================
    |       Reference to a single instance of the door closing snapshot that will affect all the rooms.     |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_doorCloseSnapshot;
    FMODUnity.ParamRef m_snapshotIntensity;

    // Private Vars
    FMOD.Studio.EventInstance m_doorEvent;
    FMOD.Studio.PARAMETER_ID m_reverbAmountID;      // Reverberation parameter can be modified to adjust the amount of reverberation on the door opening/closing sound.
    FMOD.Studio.PARAMETER_ID m_directionID;         // Direction parameter is used to determine which sound in the event is played, as there are seperate opening and closing sounds with slight differences.

    float m_upperDistToNewPos;                      // Distance between upper door and the current target position.
    float m_lowerDistToNewPos;                      // Distance between lower door and the current target position.
    bool m_opening = false;                         // Bool used to determine which way the door will move (open/close).
    Vector3 m_upperOpenPos, m_upperClosedPos;       // Positions of upper door (in local space) when open/closed.
    Vector3 m_lowerOpenPos, m_lowerClosedPos;       // Positions of lower door (in local space) when open/closed.

    bool m_completed = false;

    static AsyncOperation s_async;                  // Async operation, used to check when an operation has been completed.
    bool m_loading;
    SphereCollider m_collider;                      // Reference to door trigger.
	Coroutine m_lastCoroutine = null;

    void Start()
    {
        // If there is not scene assigned to this door controller, set the emission color to red and remove this script.
        if (m_sceneToLoad == "")
        {
            Renderer rend = m_lowerDoor.GetComponent<Renderer>();
            rend.materials[2].SetColor("_EmissionColor", Color.red);
            DynamicGI.SetEmissive(rend, Color.red);
			Destroy (this);
        }

        if (m_doorSound != "")
        {
            /*===============================================Fmod====================================================
            |             Create an instance, get the parameter IDs to control the "Reverb" and "Direction".        |
            =======================================================================================================*/
            m_doorEvent = FMODUnity.RuntimeManager.CreateInstance(m_doorSound);
            m_doorEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

            FMOD.Studio.EventDescription doorDesc;
            m_doorEvent.getDescription(out doorDesc);
            FMOD.Studio.PARAMETER_DESCRIPTION paramDesc;
            
            doorDesc.getParameterDescriptionByName("Reverb", out paramDesc);
            m_reverbAmountID = paramDesc.id;

            doorDesc.getParameterDescriptionByName("Direction", out paramDesc);
            m_directionID = paramDesc.id;
        }

        // Setting the thread priority to low forces the async operations to use less cpu.
        Application.backgroundLoadingPriority = ThreadPriority.Low;		

        // Find the upper door open and closed positions.
        m_upperClosedPos = m_upperDoor.transform.localPosition;
        m_upperOpenPos = m_upperClosedPos + (Vector3.up * (m_upperDoor.transform.localScale.y + 0.2f));

        // Find the lower door open and closed positions.
        m_lowerClosedPos = m_lowerDoor.transform.localPosition;
        m_lowerOpenPos = m_lowerClosedPos - (Vector3.up * (m_lowerDoor.transform.localScale.y + 0.2f));
        m_loading = false;

        m_collider = GetComponent<SphereCollider>();
        m_collider.center = Vector3.right * 6.0f;
    }

    void FixedUpdate()
    {
        // Get top door distance to either the open or closed position depending on the m_opening bool.
        m_upperDistToNewPos = Vector3.Distance(m_upperDoor.transform.localPosition, (m_opening ? m_upperOpenPos : m_upperClosedPos));
        if (m_upperDistToNewPos > 0.1f)
        {
            m_upperDoor.transform.localPosition += Vector3.up * Time.fixedDeltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_upperDoor.transform.localPosition = (m_opening ? m_upperOpenPos : m_upperClosedPos);
        }

        // Get lower door distance to either the open or closed position depending on the m_opening bool.
        m_lowerDistToNewPos = Vector3.Distance(m_lowerDoor.transform.localPosition, (m_opening ? m_lowerOpenPos : m_lowerClosedPos));

        if (m_lowerDistToNewPos > 0.1f)
        {
            m_lowerDoor.transform.localPosition -= Vector3.up * Time.fixedDeltaTime * (m_opening ? 1 : -1);
        }
        else
        {
            m_lowerDoor.transform.localPosition = (m_opening ? m_lowerOpenPos : m_lowerClosedPos);
        }

        // If the room has been 'visited' change the outer door light emission.
        if (m_completed)
        {
            Renderer rend = m_lowerDoor.GetComponent<Renderer>();
			rend.materials[2].SetColor("_EmissionColor", new Color(2f,0.70f,0f));
			DynamicGI.SetEmissive(rend, new Color(2f,0.70f,0f));
        }

        if (!SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
        {
            m_opening = false;
        }
    }

    /// <summary>
    /// If the scene has not already been loaded, start the coroutine to load the bank & scene.
    /// </summary>
    /// <param name="a_sender"></param>
    /// <param name="a_key"></param>
    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        if (!SceneManager.GetSceneByName(m_sceneToLoad).isLoaded)
        {
            m_collider.center = Vector3.right * 6.0f;
            m_loading = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name != "Overworld v2.0")
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                    m_doorCloseSnapshot.Stop();
                }
            }
            StartCoroutine(LoadBankThenScene());
        }
        else
        {
            m_doorCloseSnapshot.Stop();
            m_opening = true;
            PlayDoorSound();
        }
    }

    #region Private Functions

    /// <summary>
    /// Check if the colliding object is the Player. If it is, then load the scene and open the door.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        //Wait for door to close then unload
        float dotToCamera = Vector3.Dot(transform.right, (Camera.main.transform.position - transform.position).normalized);
		if (dotToCamera > 0) 
		{
			m_lastCoroutine = StartCoroutine (WaitForDoorToOpenThenClose ());
		} 
		else 
		{
			m_completed = true;
		}
    }

    void OnTriggerStay(Collider other)
    {
		if (m_lastCoroutine != null && m_opening)
			StopCoroutine(m_lastCoroutine);
    }

    /// <summary>
    /// Play the door opening/closing sound and set the parameters.
    /// </summary>
    void PlayDoorSound()
    {
        /*===============================================Fmod====================================================
        |                   Check to see if the sound is already playing, if not, then start                    |
        |                    the event and set the direction depending on the direction the                     |
        |                             door is moving. Update the 3D attributes.                                 |
        =======================================================================================================*/
        FMOD.Studio.PLAYBACK_STATE playState;
        m_doorEvent.getPlaybackState(out playState);
        if (playState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            return;
        }
        m_doorEvent.start();
        m_doorEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        m_doorEvent.setParameterByID(m_reverbAmountID, m_doorReverb);
        m_doorEvent.setParameterByID(m_directionID, (m_opening ? 180 : -180));
    }

    /// <summary>
    /// Load the FMOD Studio Bank, if it is valid, load the scene if it isn't loading already and finally play the door opening sound.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadBankThenScene()
    {
        m_doorCloseSnapshot.Stop();
        //~~~~~~~~~~~~~~~ Load the room audio ~~~~~~~~~~~~~~~\\
        if (m_bankToLoad != "")
        {
            /*===============================================Fmod====================================================
            |              Start loading the bank in the background including the audio sample data.                |
            =======================================================================================================*/
            FMODUnity.RuntimeManager.LoadBank(m_bankToLoad, true);

            /*===============================================Fmod====================================================
            |                         Keep yielding the coroutine until the bank has loaded.                        |
            =======================================================================================================*/
            while (FMODUnity.RuntimeManager.AnyBankLoading())
            {
                yield return true;
            }
        }

        if (!m_loading)
        {
            m_loading = true;
            s_async = SceneManager.LoadSceneAsync(m_sceneToLoad, LoadSceneMode.Additive);

            while (!s_async.isDone)
            {
                yield return true;
            }
        }
        
        m_opening = true;
        PlayDoorSound();
    }

    /// <summary>
    /// Make sure the door opens fully before closing, then unload the scene if it has been loaded and move the collider back into the Overworld.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForDoorToOpenThenClose()
    {
        while (m_upperDoor.transform.localPosition != m_upperOpenPos)
        {
            yield return false;
        }
        yield return new WaitForSeconds(m_doorHoldTime);
        m_opening = false;
        PlayDoorSound();

        // Check to see if door is closed
        while (m_lowerDoor.transform.localPosition != m_lowerClosedPos)
        {
            yield return false;
        }

        /*===============================================Fmod====================================================
        |                              Add effect here for when then door closes.                               |
        =======================================================================================================*/
        m_doorCloseSnapshot.Play();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(gameObject.transform.position, "FMODEmitter.tiff", true);
    }

    /// <summary>
    /// Unload the FMOD Audio Bank.
    /// </summary>
    void UnloadBank()
    {
        if (m_bankToLoad != "")
            FMODUnity.RuntimeManager.UnloadBank(m_bankToLoad);
    }

    #endregion
}
