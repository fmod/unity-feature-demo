/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Base Target class. Plays sound OnCollision.                     |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public float m_minRadius;
    public float m_maxRadius;
    public BaseTarget m_Parent;
    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================FMOD====================================================
    |   Name of Event. Used in conjunction with EventInstance.                                              |
    =======================================================================================================*/
    public string m_hitSoundPath;
    /*===============================================FMOD====================================================
    |   EventInstance. Used to play or stop the sound, etc.                                                 |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_hitSound;
    /*===============================================FMOD====================================================
    |   PARAMETER_ID - Used to reference a parameter stored in EventInstance. Example use case: changing    |
    |   from wood to carpet floor.                                                                          |
    =======================================================================================================*/
    FMOD.Studio.PARAMETER_ID m_hitMaterialID;
    /*===============================================FMOD====================================================
    |   This int will be used on start to change the parameter value of the hit.                            |
    =======================================================================================================*/
    [UnityEngine.Range(0, 2)]
    public int m_material;

    void Start()
    {
        float rngSize = Random.Range(m_minRadius, m_maxRadius);
        transform.localScale = new Vector3(rngSize, rngSize, rngSize);
        /*===============================================FMOD====================================================
        |   Calling this function will create an EventInstance. The return value is the created instance.       |
        =======================================================================================================*/
        m_hitSound = FMODUnity.RuntimeManager.CreateInstance(m_hitSoundPath);
        m_hitSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        /*===============================================FMOD====================================================
        |   Store the parameter ID for later use.                                                              |
        =======================================================================================================*/
        FMOD.Studio.EventDescription hitSoundDesc;
        m_hitSound.getDescription(out hitSoundDesc);
        FMOD.Studio.PARAMETER_DESCRIPTION paramDesc;
        
        hitSoundDesc.getParameterDescriptionByName("Material", out paramDesc);
        m_hitMaterialID = paramDesc.id;

        /*===============================================FMOD====================================================
        |   This function is used to set the ParameterInstance value.                                           |
        =======================================================================================================*/
        m_hitSound.setParameterByID(m_hitMaterialID, m_material);

    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
    void OnCollisionEnter(Collision a_col)
    {
        if (a_col.gameObject.name.Contains("Laser"))
        {
            /*===============================================FMOD====================================================
            |  Calling EventInstance.start() will start the event.                                                  |
            =======================================================================================================*/
            m_hitSound.start();

            /*===============================================FMOD====================================================
            |  Set this to attach the sound to the gameObject.                                                      |
            =======================================================================================================*/
            m_hitSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

            transform.GetChild(0).gameObject.SetActive(true);
            if (m_Parent)
                m_Parent.Hit(this);
        }
    }
}