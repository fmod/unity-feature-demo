/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the suns rotation.                                     |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class SunController : ActionObject
{
    ActorControls m_actor;
    Material m_skyMat;

    bool m_inControl;
    float m_sunValue;
    public float SunValue { get { return m_sunValue; } }
    public Light m_sunLight;

    void Start()
    {
        m_actor = Camera.main.GetComponentInParent<ActorControls>();
        m_skyMat = GameObject.Find("NightSky").GetComponent<Renderer>().material;
        m_sunValue = 180.0f * (1.0f - m_skyMat.GetFloat("_Opacity"));
    }
    void Update()
    {
        if (m_inControl)
        {
            float mouseX = Input.GetAxis("Mouse X") * 4.0f;
            if (mouseX != 0.0f)
            {
                transform.Rotate(new Vector3(0.0f, -mouseX, 0.0f));
                float val = Mathf.Sin(Mathf.PI * (m_sunValue / 360.0f));
                m_skyMat.SetFloat("_Opacity", 1.0f - val);
                m_sunLight.intensity = val;

                m_sunValue = m_sunValue + mouseX;
                while (m_sunValue >= 360.0f)
                {
                    m_sunValue -= 360.0f;
                }
                while (m_sunValue < 0.0f)
                {
                    m_sunValue += 360.0f;
                }
            }
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_actor.m_disabledMouse = true;
        m_inControl = true;
    }
    public override void ActionReleased(GameObject sender, KeyCode a_key)
    {
        m_actor.m_disabledMouse = false;
        m_inControl = false;
    }
}