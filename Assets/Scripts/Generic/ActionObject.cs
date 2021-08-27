/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The base class for all objects that can be intacted with.       |
|   Derived classes will inherit the appropriate classes.                                       |
===============================================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public KeyCode[] m_actionKeys; //List of all keys that can be pressed to activate the ActionObject
    public string[] m_actionVerbs; //List of corresponding keys strings that will be displayed on the UI

    public Renderer m_renderer;
    public int m_materialIndex = 0;

    public float m_idleGlowSpeed = 4.0f;
    [Range(0.0f, 1.0f)]
    public float m_idleGlowStrength = 0.5f;

    public float m_hoverGlowSpeed = 8.0f;
    [Range(0.0f, 1.0f)]
    public float m_hoverGlowStrength = 0.5f;

    public float m_clickGlowSpeed = 0.5f;
    [Range(0.0f, 1.0f)]
    public float m_clickGlowStrength = 1.0f;

    Material[] m_materials;
    Color m_baseColor;
    public Color m_newColor;
    int m_inQuestion;
    public int InQuestion
    {
        get
        {
            return m_inQuestion;
        }
        set
        {
            m_clickGlowElapsed = 0.0f;
            m_inQuestion = value;
        }
    }
    float m_clickGlowElapsed;

    public Vector3 m_localClickDirection;
    Vector3 m_originalLocalPosition;
    Vector3 m_clickLocalPosition;
    float m_clickSpeed, m_clickElapsed;

    bool m_isGlowing;
    public bool m_glowOnce;
    bool m_stoppingGlow;

    public float m_pressPerSecond = 0.0f;

    void Start()
    {
        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
    }
    protected void InitButton()
    {
        m_originalLocalPosition = transform.localPosition;
        m_clickLocalPosition = m_originalLocalPosition + m_localClickDirection;
    }
    protected void InitGlow()
    {
        if (!m_renderer)
            return;
        m_clickGlowElapsed = 0.0f;
        m_clickSpeed = 0.25f;
        m_clickElapsed = 0.5f;
        m_inQuestion = 0;
        m_baseColor = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
        m_isGlowing = true;
    }
    protected void UpdateGlow()
    {
        if (m_renderer && m_isGlowing)
        {
            if (m_inQuestion == 0)
            {
                Color col = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
                if (m_idleGlowSpeed != 0.0f)
                {
                    col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * m_idleGlowSpeed) * (m_idleGlowStrength * 0.5f) + (m_idleGlowStrength * 0.5f));
                }
                m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
            }
            else if (m_inQuestion == 1)
            {
                Color col = m_renderer.materials[m_materialIndex].GetColor("_EmissionColor");
                if (m_hoverGlowSpeed != 0.0f)
                {
                    col = Color.Lerp(m_baseColor, m_newColor, Mathf.Sin(Time.time * m_hoverGlowSpeed) * (m_hoverGlowStrength * 0.5f) + (m_hoverGlowStrength * 0.5f));
                }
                m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
            }
            else if (m_inQuestion == 2)
            {
                if (m_clickGlowSpeed != 0.0f && m_clickGlowStrength != 0.0f)
                {
                    float clickValue = (1.0f - (m_clickGlowElapsed / m_clickGlowSpeed));
                    if (m_clickGlowElapsed < m_clickGlowSpeed)
                    {
                        if (m_clickGlowElapsed == 0.0f)
                        {
                            m_clickElapsed = 0.0f;
                        }
                        //Glow
                        Color col = Color.Lerp(m_baseColor, m_newColor, clickValue * m_clickGlowStrength);
                        m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
                        m_clickGlowElapsed += Time.deltaTime;
                    }
                    else
                    {
                        m_inQuestion = 0;
                        m_clickGlowElapsed = 0.0f;
                        if (m_stoppingGlow)
                        {
                            ResetGlow();
                            m_isGlowing = false;
                        }
                    }
                }
                else
                {
                    m_inQuestion = 1;
                }
            }
        }

        if (m_clickElapsed < m_clickSpeed)
        {
            m_clickElapsed = Mathf.Clamp(m_clickElapsed + Time.deltaTime, 0.0f, m_clickSpeed);
            if (m_localClickDirection != Vector3.zero && m_originalLocalPosition != m_clickLocalPosition)
            {
                transform.localPosition = Vector3.Lerp(m_originalLocalPosition, m_clickLocalPosition, Mathf.Clamp(Mathf.Sin((Mathf.PI) * ((m_clickElapsed / m_clickSpeed))), 0.0f, 1.0f));
            }
        }
    }
    protected void ResetGlow()
    {
        Color col = m_baseColor;
        m_renderer.materials[m_materialIndex].SetColor("_EmissionColor", col);
        m_inQuestion = 0;
        m_clickElapsed = 0.0f;
        m_clickGlowElapsed = 0.0f;
    }
    public void LastGlow()
    {
        m_stoppingGlow = true;
    }
    public void StartGlow()
    {
        m_isGlowing = true;
        m_stoppingGlow = false;
    }
    public void StopGlow()
    {
        m_isGlowing = false;
        ResetGlow();
    }
    //When the key has been pressed that frame
    public virtual void ActionPressed(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been released that frame
    public virtual void ActionReleased(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been held down for more than one frame
    public virtual void ActionDown(GameObject a_sender, KeyCode a_key)
    {

    }
}
