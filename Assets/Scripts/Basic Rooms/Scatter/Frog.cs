/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Make the frogs jump.                                            |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour
{
    public float m_turningPower;
    public float m_jumpInterval;
    public float m_jumpPower;

    float m_elapsed;

    bool m_isJumping;
    Vector3 m_newPosition;
    Vector3 m_forceDirection;

    Animator m_animator;

    void Start()
    {
        m_isJumping = false;
        m_forceDirection = transform.forward;
        m_forceDirection = Quaternion.Euler(transform.right * -45.0f) * m_forceDirection;
    }
    void Update()
    {
        if (m_isJumping)
        {
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
                transform.forward = GetComponent<Rigidbody>().velocity.normalized;
        }
        else
        {
            m_elapsed += Time.deltaTime;
            if (m_elapsed >= m_jumpInterval)
            {
                m_forceDirection = Quaternion.Euler(0.0f, m_turningPower, 0.0f) * m_forceDirection;
                m_isJumping = true;
                GetComponent<Rigidbody>().AddForce(m_forceDirection * m_jumpPower);
                transform.forward = m_forceDirection;
                m_elapsed = 0.0f;
            }
        }
    }
    void OnCollisionEnter(Collision a_col)
    {
        if (m_isJumping)
        {
            if (a_col.gameObject.tag == "Mud")
            {
                m_isJumping = false;
                transform.forward = Vector3.Cross(a_col.contacts[0].normal, -transform.right);
            }
        }
    }
}