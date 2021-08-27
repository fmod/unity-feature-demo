/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Smaller branches.                                               |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningSubBranch : LightningBranch
{
    void Start()
    {
        m_zags = new List<LightningZag>();
        m_numOfZags = Random.Range((int)m_lightning.m_randNumOfSubZags.x, (int)m_lightning.m_randNumOfSubZags.y);

        Vector3 lastPoint = m_startPosition;
        Vector3 lastDirection = m_startDirection;
        float lastWidth = m_startWidth;

        for (int i = 0; i < m_numOfZags; ++i)
        {
            GameObject zagObj = new GameObject();
            LightningZag zag = zagObj.AddComponent<LightningZag>();
            //Name
            zag.m_name = "Zag " + i.ToString();
            //Parent
            zagObj.transform.parent = this.transform;
            //Lightning
            zag.m_lightning = m_lightning;

            //Start Position
            zag.m_startPoint = lastPoint;
            //End Position
            lastDirection = Quaternion.AngleAxis(Random.Range(m_lightning.m_randSubAngle.x, m_lightning.m_randSubAngle.y), new Vector3(1.0f, 0.0f, 0.0f)) * lastDirection;
            lastDirection = Quaternion.AngleAxis(Random.Range(m_lightning.m_randSubAngle.x, m_lightning.m_randSubAngle.y), new Vector3(0.0f, 0.0f, 1.0f)) * lastDirection;
            float length = Random.Range(m_lightning.m_randSubLength.x, m_lightning.m_randSubLength.y);
            zag.m_endPoint = lastPoint + lastDirection * length;
            lastPoint = zag.m_endPoint;
            //Directoin
            zag.m_direction = lastDirection;
            //Width
            zag.m_width.x = lastWidth;
            if (i == m_numOfZags - 1)
            {
                zag.m_width.y = lastWidth * 0.05f;
                zag.m_isLastZag = true;
            }
            else
                zag.m_width.y = lastWidth * Random.Range(m_lightning.m_randMaxSubWidthDegradation, m_lightning.m_randMaxSubWidthDegradation) * 0.01f;
            lastWidth = zag.m_width.y;
            //Branch Number
            zag.m_branchNum = m_branchNum;
            m_zags.Add(zag);
        }
    }
    void Update()
    {

    }
    void Branch()
    {

    }
}
