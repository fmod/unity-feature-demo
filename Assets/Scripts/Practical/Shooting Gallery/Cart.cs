/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     Yes                                                              |
|   Description:                The Cart class controls the movement of itself, disabling the   |
|   player, and moving the player with the cart                                                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cart : ActionObject
{
    public ShootingGalleryManager m_manager;
    public Transform m_seat;
    public float m_topSpeed = 2.0f;
    public float m_acceleration = 0.005f;
    public float m_turningPower = 0.1f;
    public float m_nearThreshold = 0.45f;

    ActorControls m_player;
    float m_currentVelocity;
    public float CurrentVelocity
    {
        set
        {
            m_currentVelocity = value;
        }
    }
    bool m_playerIsSeated;
    bool m_stopping;

    /*===============================================FMOD====================================================
    |   Where the rail Studio Event Emitter will be stored, so it can be changed in script.                 |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_railEvent;

    void Start()
    {
        InitGlow();
        m_playerIsSeated = false;
        m_player = Camera.main.GetComponentInParent<ActorControls>();
        /*===============================================FMOD====================================================
        |   Getting an existing Studio Event Emitter attached to this GameObject.                               |
        =======================================================================================================*/
        m_railEvent = GetComponent<FMODUnity.StudioEventEmitter>();
        m_stopping = false;
    }
    void Update()
    {
        if (!m_playerIsSeated)
            UpdateGlow();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < m_actionKeys.Length; ++i)
        {
            if (m_playerIsSeated && Input.GetKeyDown(m_actionKeys[i]))
            {
                ActionPressed(this.gameObject, m_actionKeys[i]);
            }
        }
        if ((m_manager.IsActive && !m_manager.IsPaused) || m_stopping)
        {
            CalculateHeading();
            Move();
            if (m_stopping && m_currentVelocity <= 0.01f)
            {
                Stop();
            }
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if (m_stopping)
            return;

        ResetGlow();
        if (!m_playerIsSeated)
        {
            m_manager.Play();
            m_player.DisableMovement = true;
            m_player.ActivateGun(true);
            m_player.GetComponent<CharacterController>().enabled = false;
            m_playerIsSeated = true;
            m_railEvent.Play();
        }
        else
        {
            m_manager.Pause();
            m_player.ActivateGun(false);
            /*===============================================FMOD====================================================
            |   Setting the Rail Events parameters.                                                                 |
            =======================================================================================================*/
            m_railEvent.SetParameter("Exit Vehicle", 1.0f);
            m_railEvent.SetParameter("Velocity", Mathf.Clamp((m_currentVelocity / m_topSpeed), 0.01f, 0.99f));
            m_stopping = true;
        }
    }
    void Stop()
    {
        m_player.DisableMovement = false;
        m_player.GetComponent<CharacterController>().enabled = true;
        m_playerIsSeated = false;
        m_stopping = false;
    }
    void CalculateHeading()
    {
        if ((m_manager.GetCurrentTrack().position - transform.position).magnitude <= m_nearThreshold)       //if the current target track has been reached within the threshold then increment current Track index or set it back to 0 if the current index is the last track in array.
        {
            m_manager.IncremetTrack();      //Tell manager to increment Track
        }

        
        Vector3 newForward = transform.forward + (m_manager.GetCurrentTrack().position - transform.position) * m_turningPower;  //Turn towards next track
        newForward.Normalize();
        transform.forward = newForward;     //Set the carts forward to the new forward
    }
    void Move()
    {
        //Add to velocity with acceleration
        if (!m_stopping)
            m_currentVelocity += m_acceleration;
        else
            m_currentVelocity -= m_acceleration;
        //Cap speed to top speed
        if (m_currentVelocity > m_topSpeed)
            m_currentVelocity = m_topSpeed;
        //Add to position with velocity
        transform.position += transform.forward * m_currentVelocity * Time.fixedDeltaTime;

        //Set players position to cart
        m_player.transform.position = m_seat.position;
    }
}