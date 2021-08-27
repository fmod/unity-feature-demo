/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                22/08/2016                                                      |
|   Scene:                      Everywhere                                                      |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Prompt UI that is spread throughout the entire Project          |
|   used to explain some of the features and point you towards the correct documentation.       |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum HELPERSTATE
{
    STOPPED,
    OPENING,
    LOADING,
    IDLE
};

public class HelperUIControl : ActionObject
{
    [Range(0, 10.0f)]    public float m_timeUntilClosed = 2.0f;     // Time from once the player looks away, until the prompt will begin to close.
    [Range(0, 30.0f)]    public float m_maxPlayerDistance = 10.0f;  // Maximum distance the player can be from the prompt and still interact with it.
    public bool m_billboard = true;                                 // Bool to turn billboarding on/off per prompt.
    public string m_uiLoadAnim = "UI Prompt Load";
    public string m_uiOpenAnim = "UI Prompt Open";
    public string m_uiCloseAnim = "UI Prompt Close";

    /*===============================================Fmod====================================================
    |           Variables for creating and storing events to play during the UI animations.                 |
    |      Because there will only be once event playing at a time, we don't need 3 seperate events.        |
    =======================================================================================================*/
    [FMODUnity.EventRef]    public string m_uiHover;
    [FMODUnity.EventRef]    public string m_uiOpen;
    [FMODUnity.EventRef]    public string m_uiClose;
    FMOD.Studio.EventInstance m_currentEvent;
    
    bool m_updateFacing = false;                    // Bool to check if the player is close enough to actually billboard to.
    HELPERSTATE m_currentState = HELPERSTATE.IDLE;
    float m_currentAnimationProgress;
    GameObject m_playerRef = null;                  // Used for getting the players position to billboard the UI.
    Animator m_uiAnimator;

    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        m_playerRef = GameObject.FindGameObjectWithTag("Player");
        m_uiAnimator = GetComponentInChildren<Animator>();
        StopAnimation();
    }

    void FixedUpdate()
    {
        // Had to change to an action object because Unity decided to make a locked cursor not interact with UI
        if (InQuestion == 1)
        {
            ChangeState();
        }
        else if (InQuestion == 0)
        {
            StopHelper();
        }

        if (m_billboard && m_updateFacing)
        {
            transform.LookAt(m_playerRef.transform.position, Vector3.up);    // Lerp the facing direction to make smooth.
            transform.Rotate(Vector3.up, 180.0f);
        }

        m_currentAnimationProgress = AnimationProgress();
    }

    /// <summary>
    /// Used for billboarding when within a certain distance to the player.
    /// </summary>
    void LateUpdate()
    {
        if (m_playerRef != null)
        {
            if (Vector3.Distance(transform.position, m_playerRef.transform.position) < m_maxPlayerDistance)
            {
                m_updateFacing = true;
            }
        }
    }

    /// <summary>
    /// Start the load animation, change state to loading and start coroutine to load and open prompt.
    /// </summary>
    public void LoadHelper()
    {
        StopAllCoroutines();
        // Play loading animation for circle image
        m_uiAnimator.Play(m_uiLoadAnim, 0, 0.0f);
        // Set the animator time back to 0
        //m_uiAnimator.playbackTime = 0;
        // Sets a modifying variable to positive for Forward.
        PlayForward();
        // Set the current state to loading as loading has started.
        m_currentState = HELPERSTATE.LOADING;
        // Run the coroutine that will then run the next animation as long as this animation has completed.
        StartCoroutine(LoadWaitAndOpen());
    }

    /// <summary>
    /// Play the event for the current state.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadWaitAndOpen()
    {
        /*===============================================Fmod====================================================
        |                                   Create instance and start playing.                                   |
        =======================================================================================================*/
        m_currentEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiHover);
        m_currentEvent.start();
        // Loop until the animation has played once.
        while (m_currentAnimationProgress < 0.95f)
        {
            // If at any point the cursor has left the image before it has completed the animation, 
            if (m_currentState != HELPERSTATE.LOADING)
            {
                break;
            }
            yield return null;
        }
        if (m_currentState == HELPERSTATE.LOADING)
        {
            /*===============================================Fmod====================================================
            |                                      Stop the current instance.                                       |
            =======================================================================================================*/
            m_currentEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            OpenHelper();
        }
    }

    /// <summary>
    /// Play the opening animation and event.
    /// </summary>
    public void OpenHelper()
    {
        /*===============================================Fmod====================================================
        |                                   Create instance and start playing.                                   |
        =======================================================================================================*/
        m_currentEvent = FMODUnity.RuntimeManager.CreateInstance(m_uiOpen);
        m_currentEvent.start();
        
        // Play opening animation
        m_uiAnimator.Play(m_uiOpenAnim, 0);
        m_currentState = HELPERSTATE.OPENING;
    }

    /// <summary>
    /// If the prompt is loading or just not opening, stop the animation and strt the coroutine to play the animation in reverse.
    /// </summary>
    public void StopHelper()
    {
        // If the ui is not opening (allows the ui to open fully after loading)
        if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiLoadAnim) || 
            (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiOpenAnim) && m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1))
        {
            StopAnimation();
            /*===============================================Fmod====================================================
            |                                      Stop the current instance.                                       |
            =======================================================================================================*/
            if (m_currentEvent.hasHandle())
            {
                m_currentEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                m_currentState = HELPERSTATE.STOPPED;
            }
            StartCoroutine(WaitAndCloseHelper());
        }
    }

    /// <summary>
    /// If the state is stopped, wait for time variable and play the animation backwards to close the prompt.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAndCloseHelper()
    {
        while (m_currentState == HELPERSTATE.STOPPED)
        {
            yield return new WaitForSeconds(m_timeUntilClosed);

            // Play animation in reverse from current position
            if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiLoadAnim))
            {
                PlayBackwards();
                while (m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && m_uiAnimator.GetFloat("Speed") == -1)
                {
                    yield return null;
                }
            }
            else if (m_uiAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_uiOpenAnim) &&
                m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                m_uiAnimator.Play(m_uiCloseAnim, 0, 0f);
                PlayForward();
                /*===============================================Fmod====================================================
                |                                      Create one shot instance.                                        |
                =======================================================================================================*/
                FMODUnity.RuntimeManager.PlayOneShot(m_uiClose, GetComponent<Transform>().position);
                while (m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    yield return null;
                }
            }
            m_currentState = HELPERSTATE.IDLE;
        }
    }

    /// <summary>
    /// Explicitly play the current animation forward.
    /// </summary>
    void PlayForward()
    {
        m_uiAnimator.SetFloat("Speed", 1);
        m_uiAnimator.speed = 1;
    }

    /// <summary>
    /// Play the current animation backwards.
    /// </summary>
    void PlayBackwards()
    {
        m_uiAnimator.SetFloat("Speed", -1);
        m_uiAnimator.speed = 1;
    }

    /// <summary>
    /// Stop the current animation.
    /// </summary>
    void StopAnimation()
    {
        m_uiAnimator.speed = 0;
    }

    /// <summary>
    /// Returns the completed percentage of the current animation.
    /// </summary>
    /// <returns></returns>
    float AnimationProgress()
    {
        float totalTime = m_uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float fractionalTime = totalTime - (int)totalTime;

        if (totalTime >= 1 && fractionalTime == 0.0f)
        {
            return 1.0f;
        }

        return Mathf.Abs( fractionalTime );
    }

    public void ChangeState()
    {
        switch (m_currentState)
        {
            case HELPERSTATE.IDLE:
                LoadHelper();
                break;
            case HELPERSTATE.STOPPED:
                LoadHelper();
                break;
            case HELPERSTATE.OPENING:
                break;
            case HELPERSTATE.LOADING:
                break;
            default:
                break;
        }
    }
}
