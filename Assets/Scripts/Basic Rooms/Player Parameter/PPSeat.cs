using UnityEngine;
using System.Collections;

public class PPSeat : ActionObject
{
    public Car m_car;
    public Ignition m_ignition;
    public Transform m_entrySeat;
    public Transform m_exitSeat;

    ActorControls m_actor;
    bool m_isSeated, m_isReady;

    void Start()
    {
        InitGlow();
        m_isSeated = false;
        m_isReady = false;
        m_actor = Camera.main.gameObject.GetComponentInParent<ActorControls>();
    }

    void Update()
    {
        if (m_isSeated)
        {
            if (m_isReady)
            {
                m_actor.transform.position = m_entrySeat.position;
                for (int i = 0; i < m_actionKeys.Length; ++i)
                {
                    if (Input.GetKeyDown(m_actionKeys[i]))
                    {
                        m_isSeated = false;
                        m_actor.transform.position = m_exitSeat.position;
                        m_actor.SetRotation(m_exitSeat.rotation);
                        GetComponent<Collider>().enabled = true;
                        m_actor.GetComponent<CharacterController>().enabled = true;
                        m_actor.DisableMovement = false;
                        m_ignition.StopGlow();
                        m_car.IgnitionOff();
                        return;
                    }
                }
            }
        }
        else
        {
            UpdateGlow();
        }
        m_isReady = true;
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        if (m_isSeated)
        {
            return;
        }
        m_isSeated = true;
        m_isReady = false;
        m_actor.transform.position = m_entrySeat.position;
        m_actor.SetRotation(transform.rotation);
        m_actor.DisableMovement = true;
        m_actor.GetComponent<CharacterController>().enabled = false;
        transform.GetChild(0).GetComponent<Collider>().enabled = false;
        ResetGlow();
        m_ignition.StartGlow();
    }
}