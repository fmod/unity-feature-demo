/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                A target that, when shot, springs back up.                      |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class SpringBack : BaseTarget
{
    public float m_aliveTimer = 3.0f;
    public float m_deadTimer = 5.0f;
    public bool m_dead;

    float m_elapsed;
    bool m_originalDead;
    bool m_preparing;
    Quaternion m_originalRotation;

    void Start()
    {
        m_originalDead = m_dead;
        m_originalRotation = transform.localRotation;
        m_elapsed = 0.0f;

        if (m_dead)
        {
            transform.rotation *= Quaternion.AngleAxis(-90.0f, new Vector3(0.0f, 0.0f, 1.0f));
        }
    }
    void Update()
    {
        if (!m_active)
            return;
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (!m_preparing)
        {
            m_elapsed += Time.deltaTime;
            if (m_dead)
            {
                if (m_elapsed >= m_deadTimer)
                {
                    m_preparing = true;
                    m_elapsed = 0.0f;
                    m_dead = false;
                }
            }
            else
            {
                if (m_elapsed >= m_aliveTimer)
                {
                    m_preparing = true;
                    m_elapsed = 0.0f;
                    m_dead = true;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (!m_active)
            return;
        if (m_preparing)
        {
            m_elapsed += Time.fixedDeltaTime;
            if (m_dead)
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -90.0f * Time.fixedDeltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 90.0f * Time.fixedDeltaTime);

            }
            if (m_elapsed >= 1.0f)
            {
                m_preparing = false;
                m_elapsed = 0.0f;
                transform.rotation = m_originalRotation;
                if (m_dead)
                {
                    transform.rotation *= Quaternion.AngleAxis(-90.0f, new Vector3(0.0f, 0.0f, 1.0f));
                }
            }
        }
    }

    public override void Hit(Target Target)
    {
        if (!m_active || m_dead || m_preparing)
            return;
        m_manager.AddScore(m_points);
        m_elapsed = 0.0f;
        m_preparing = true;
        m_dead = true;
    }
    public override void Reset()
    {
        m_elapsed = 0.0f;
        m_preparing = false;
        m_dead = m_originalDead;

        transform.localRotation = m_originalRotation;
        if (m_dead)
        {
            transform.rotation *= Quaternion.AngleAxis(-90.0f, new Vector3(0.0f, 0.0f, 1.0f));
        }
    }
}