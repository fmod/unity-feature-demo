/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Panning                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Copies the main cameras actions but elsewhere                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PanningCamera : MonoBehaviour
{
    public Transform m_right;
    Camera m_follow;
    public float m_distance;

    void Start ()
    {
        m_follow = Camera.main;
    }

    void Update ()
    {
        transform.position = m_follow.transform.position + m_right.right * -m_distance;
        Camera cam = GetComponent<Camera>();
        cam.fieldOfView = m_follow.fieldOfView;
        cam.nearClipPlane = m_follow.nearClipPlane;
        cam.transform.eulerAngles = m_follow.transform.eulerAngles;
    }
}