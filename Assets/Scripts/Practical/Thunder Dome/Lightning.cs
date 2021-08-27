/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Creates randomly generated lightning.                           |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightning : MonoBehaviour
{
    public WeatherController m_weatherController;
    public Material m_material;

    public Vector3 m_positionDeviation;

    public Vector3 m_direction;
    public float m_randDirectionAngle;

    public Vector2 m_randMainAngle;
    public Vector2 m_randSubAngle;

    [Range(0, 100)]
    public int m_mainBranchChance;
    [Range(0, 100)]
    public int m_subBranchChance;
    public int m_maxNumOfBranches;

    public Vector2 m_randNumOfMainSplits;
    public Vector2 m_randNumOfSubSplits;

    public Vector2 m_randNumOfMainZags;
    public Vector2 m_randNumOfSubZags;

    public Vector2 m_randMainWidth;
    [Range(0.0f, 100.0f)]
    public float m_randMinMainWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMaxMainWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMinSubWidthDegradation;
    [Range(0.0f, 100.0f)]
    public float m_randMaxSubWidthDegradation;

    public Vector2 m_randMainLength;
    public Vector2 m_randSubLength;

    public Vector2 m_duration, m_interval;
    [Range(0.0f, 100.0f)]
    public float m_randMinFadeoutPercent, m_randMaxFadeoutPercent;

    float m_durationElapsed, m_intervalElapsed, m_fadeOut;
    Vector3 m_originalPosition;

    LightningMainBranch m_mainBranch;

    LightningSound m_thunder;

    void Start()
    {
        m_thunder = GetComponent<LightningSound>();
        m_originalPosition = transform.position;
        m_durationElapsed = Random.Range(m_duration.x, m_duration.y);
        m_fadeOut = m_durationElapsed * (Random.Range(m_randMinFadeoutPercent, m_randMaxFadeoutPercent) * 0.01f);
        m_intervalElapsed = Random.Range(m_interval.x, m_interval.y);
        Vector4 col = m_material.GetVector("_Color");
        col.w = 1.0f;
        m_material.SetVector("_Color", col);
    }

    void GenerateLightning()
    {
        Vector3 newPosition = m_originalPosition;
        newPosition.x += Random.Range(-m_positionDeviation.x, m_positionDeviation.x);
        newPosition.y += Random.Range(-m_positionDeviation.y, m_positionDeviation.y);
        newPosition.z += Random.Range(-m_positionDeviation.z, m_positionDeviation.z);
        transform.position = newPosition;
        if (m_mainBranch)
        {
            m_mainBranch.Destroy();
            Destroy(m_mainBranch.gameObject);
        }
        GameObject mainBranchObj = new GameObject();
        mainBranchObj.name = "Main";
        mainBranchObj.layer = 8;
        m_mainBranch = mainBranchObj.AddComponent<LightningMainBranch>();
        m_mainBranch.m_lightning = this;
        m_mainBranch.transform.parent = this.transform;
        m_mainBranch.m_branchNum = 0;
    }

    void Update()
    {
        m_durationElapsed -= Time.deltaTime;
        if (m_durationElapsed <= 0.0f)
        {
            Vector4 col = m_material.GetVector("_Color");
            col.w = 0.0f;
            m_material.SetVector("_Color", col);
            if (m_weatherController.Rain >= 0.8f && m_intervalElapsed > 0.0f)
            {
                m_intervalElapsed -= Time.deltaTime;
            }
            if (m_intervalElapsed <= 0.0f)
            {
                if (m_weatherController.Rain >= 0.8f)
                {
                    GenerateLightning();
                    m_durationElapsed = Random.Range(m_duration.x, m_duration.y);
                    m_fadeOut = m_durationElapsed * (Random.Range(m_randMinFadeoutPercent, m_randMaxFadeoutPercent) * 0.01f);
                    m_intervalElapsed = Random.Range(m_interval.x, m_interval.y);
                    col = m_material.GetVector("_Color");
                    col.w = 1.0f;
                    m_material.SetVector("_Color", col);
                }

            }
        }
        else
        {
            if (m_durationElapsed <= m_fadeOut)
            {
                Vector4 col = m_material.GetVector("_Color");
                col.w = Mathf.Clamp((m_durationElapsed / m_fadeOut), 0.0f, 1.0f);
                m_material.SetVector("_Color", col);
            }
        }
    }

    public void PlayThunder(int a_thunder)
    {
        m_thunder.Play(a_thunder);
    }
}