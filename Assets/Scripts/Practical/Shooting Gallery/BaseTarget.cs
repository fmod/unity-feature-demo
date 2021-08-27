/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The Base class for all targets in the shooting gallery.         |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class BaseTarget : MonoBehaviour
{
    public int m_points;
    public ShootingGalleryManager m_manager;
    protected bool m_active;

    void Start()
    {

    }
    void Update()
    {

    }

    public virtual void Hit(Target a_target)
    {

    }
    public virtual void Reset()
    {

    }
    public virtual void Play()
    {
        m_active = true;
    }
    public virtual void Stop()
    {
        m_active = false;
    }
}