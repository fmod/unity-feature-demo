/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shootimg Gallery                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Shoots bullets.                                                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject m_bullet;
    public Transform m_gunStart;
    public GameObject m_glow;
    public float m_power = 2.0f;
    public float m_fireRate = 0.1f;
    public float m_glowDuration = 0.5f;

    /*===============================================FMOD====================================================
    |   Storing the shoot sound event name so we can use PlayOneShot.                                       |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_eventRef;

    float m_elapsed;
    float m_glowElapsed;

    void Start()
    {
        m_elapsed = 0.0f;
        m_glowElapsed = 0.0f;
        Color col = m_glow.GetComponent<SpriteRenderer>().color;
        col.a = 0.0f;
        m_glow.GetComponent<SpriteRenderer>().color = col;
    }
    void Update()
    {
        Shoot();
        if (m_glowElapsed > 0.0f)
        {
            m_glowElapsed -= Time.deltaTime;
            Color col = m_glow.GetComponent<SpriteRenderer>().color;
            col.a = Mathf.Max(m_glowElapsed / m_glowDuration, 0.0f);
            m_glow.GetComponent<SpriteRenderer>().color = col;
        }
    }

    void Shoot()
    {
        m_elapsed = Mathf.Min(m_elapsed + Time.deltaTime, m_fireRate);
        if (Input.GetMouseButton(0) && m_elapsed >= m_fireRate)
        {
            Color col = m_glow.GetComponent<SpriteRenderer>().color;
            col.a = 1.0f;
            m_glow.GetComponent<SpriteRenderer>().color = col;
            m_glowElapsed = m_glowDuration;
            /*===============================================FMOD====================================================
            |   This is how you play a oneshot sound quickly. Optionally a transform can be passed into this        |
            |   function.                                                                                           |
            =======================================================================================================*/
            FMODUnity.RuntimeManager.PlayOneShot(m_eventRef);

            GameObject obj = Instantiate(m_bullet) as GameObject;
            obj.transform.position = m_gunStart.position;
            obj.transform.forward = m_gunStart.forward;
            obj.GetComponent<Rigidbody>().AddForce(m_gunStart.forward * m_power);
            m_elapsed = 0.0f;
        }
    }
}