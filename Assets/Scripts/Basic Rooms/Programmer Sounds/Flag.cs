/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Programmer Sounds                                               |
|   Fmod Related Scripting:     No                                                              |
|   Description:                When the flag has been acted upon. It will follow the actors    |
|   line of sight until action key is pressed again or until the actor has picked up another    |
|   flag.                                                                                       |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Flag : ActionObject
{
    public string m_sound;
    bool m_canCollide, m_isActive;
    float m_distance;
    Rigidbody m_rb;
    int m_index;
    public ParticleSystem m_particle;

    void Start()
    {
        m_index = 1;
        InitGlow();
        m_rb = GetComponent<Rigidbody>();
        m_distance = 0.0f;
        m_canCollide = false;
        m_isActive = false;
        m_particle.time = 1.0f;
    }
    void Update()
    {
        UpdateGlow();
        if (m_isActive)
        {
            //Raycast past flag to see if theres obstruction forcing the flag to be closer to the player
            RaycastHit rh;
            int layerMask = 1 << LayerMask.NameToLayer("PlayerIgnore");
            layerMask = ~layerMask;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rh, m_distance, layerMask) && rh.collider.gameObject.name != "Programmer Sounds Door Container")
            {
                transform.position = Camera.main.transform.position + (Camera.main.transform.forward * rh.distance);
            }
            else
            {
                transform.position = Camera.main.transform.position + (Camera.main.transform.forward * m_distance);
            }
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_distance = (transform.position - Camera.main.transform.position).magnitude;
        m_isActive = true;
        m_rb.useGravity = false;
        m_canCollide = false;
    }
    public override void ActionReleased(GameObject a_sender, KeyCode a_key)
    {
        if (a_key == KeyCode.None)
            return;
        m_isActive = false;
        m_canCollide = true;
        m_rb.useGravity = true;
        Vector3 pos = transform.position;
        if (pos.y <= a_sender.transform.position.y - a_sender.transform.localScale.y)
        {
            pos.y = a_sender.transform.position.y - a_sender.transform.localScale.y;
            transform.position = pos;
        }
    }

    void OnCollisionStay(Collision a_col)
    {
        //When a collision happens with the flag and the dialogue box, play the dialogue from the dialogue script.
        if (m_canCollide)
        {
            if (a_col.gameObject.name == "DialogueBox")
            {
                Dialogue dialogue = a_col.gameObject.GetComponent<Dialogue>();
                if (dialogue)
                {
                    m_particle.transform.position = transform.position - new Vector3(0.0f, 0.159426f, 0.0f);
                    m_particle.Play();
                    dialogue.PlayDialogue(m_sound + m_index.ToString());
                    m_canCollide = false;
                    m_index = (m_index + 1) % 3 + 1;
                }
            }
        }
    }
}