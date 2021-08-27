/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                A simple timer script that destroys the gameObject after it has |
|   expired.                                                                                    |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float m_timer;
    float m_elapsed;

    void Start()
    {

    }
    void Update()
    {
        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_timer)
            Destroy(gameObject);
    }
}