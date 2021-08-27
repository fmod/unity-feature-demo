/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Spawns target in a linear motion, infinetely.                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinearTargetEmitter : BaseTarget
{
    public Target m_target;
    public float m_respawnRate;
    public float m_targetSpeed;
    public float m_maxTargetDistance;

    List<Target> m_targets;
    float m_elapsed;

    void Start()
    {
        m_targets = new List<Target>();
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
    void Update()
    {
        if (!m_active)
            return;
        MoveTargets();

        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_respawnRate)
        {
            m_elapsed = 0.0f;
            Target obj = Instantiate(m_target);
            obj.transform.parent = transform;
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 90.0f);
            obj.m_Parent = this;
            m_targets.Add(obj);
        }
    }

    void MoveTargets()
    {
        for (int i = 0; i < m_targets.Count; ++i)
        {
            m_targets[i].transform.position += transform.forward * m_targetSpeed * Time.deltaTime;
            if ((transform.position - m_targets[i].transform.position).magnitude > m_maxTargetDistance)
            {
                Destroy(m_targets[i].gameObject);
                m_targets.RemoveAt(i);
            }
        }
    }
    public override void Hit(Target a_target)
    {
        if (!m_active)
            return;
        m_manager.AddScore(m_points);
        if (a_target.gameObject.transform.GetChild(0))
            a_target.gameObject.transform.GetChild(0).parent = null;
        Destroy(a_target.gameObject);
        m_targets.Remove(a_target);
    }
    public override void Reset()
    {
        for (int i = 0; i < m_targets.Count; ++i)
        {
            Destroy(m_targets[i].gameObject);
        }
        m_targets.Clear();
    }
}