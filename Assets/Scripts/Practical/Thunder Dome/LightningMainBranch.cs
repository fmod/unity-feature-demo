/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The main branch is much thicker than the rest.                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningMainBranch : LightningBranch
{
    void Start()
    {
        m_zags = new List<LightningZag>();
        m_numOfZags = Random.Range((int)m_lightning.m_randNumOfMainZags.x, (int)m_lightning.m_randNumOfMainZags.y);

        Vector3 lastPoint = m_lightning.transform.position;
        Vector3 lastDirection = m_lightning.m_direction;
        lastDirection = Quaternion.AngleAxis(Random.Range(0.0f, m_lightning.m_randDirectionAngle), new Vector3(1.0f, 0.0f, 0.0f)) * lastDirection;
        lastDirection = Quaternion.AngleAxis(Random.Range(0.0f, m_lightning.m_randDirectionAngle), new Vector3(0.0f, 0.0f, 1.0f)) * lastDirection;
        float lastWidth = Random.Range(m_lightning.m_randMainWidth.x, m_lightning.m_randMainWidth.x);
        float totalLength = 0.0f;
        float totalWidth = 0.0f;
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
            lastDirection = Quaternion.AngleAxis(Random.Range(m_lightning.m_randMainAngle.x, m_lightning.m_randMainAngle.y), new Vector3(1.0f, 0.0f, 0.0f)) * lastDirection;
            lastDirection = Quaternion.AngleAxis(Random.Range(m_lightning.m_randMainAngle.x, m_lightning.m_randMainAngle.y), new Vector3(0.0f, 0.0f, 1.0f)) * lastDirection;
            float length = Random.Range(m_lightning.m_randMainLength.x, m_lightning.m_randMainLength.y);
            zag.m_endPoint = lastPoint + lastDirection * length;
            lastPoint = zag.m_endPoint;
            totalLength += length;
            //direction
            zag.m_direction = lastDirection;
            //Width
            zag.m_width.x = lastWidth;
            if (i == m_numOfZags - 1)
            {
                zag.m_width.y = lastWidth * 0.05f;
                zag.m_isLastZag = true;
            }
            else
                zag.m_width.y = lastWidth * Random.Range(m_lightning.m_randMinMainWidthDegradation, m_lightning.m_randMaxMainWidthDegradation) * 0.01f;
            lastWidth = zag.m_width.y;

            totalWidth += lastWidth;
            //Main Zag
            zag.m_isMainZag = true;
            //Branch Number
            zag.m_branchNum = m_branchNum;
            m_zags.Add(zag);
        }

        float zags = (float)m_numOfZags / (int)m_lightning.m_randNumOfMainZags.y;
        float w = (totalWidth / m_numOfZags) / m_lightning.m_randMainWidth.y;
        float l = (totalLength / m_numOfZags) / m_lightning.m_randMainLength.y;

        int result = (int)(6.0f * ((zags + w + l) / 3.0f));

        m_lightning.PlayThunder(result);
    }
    void Update()
    {

    }
}
