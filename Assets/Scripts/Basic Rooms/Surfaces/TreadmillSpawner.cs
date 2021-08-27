/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Surfaces                                                        |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Manages platforms. Creates, Destroys, Translates.               |
===============================================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TreadmillSpawner : MonoBehaviour
{
    public ParticleSystem m_grassParticleEmitter;
    public GameObject[] m_floorTextures;
    public float m_speed;
    public int m_numOfTiles;
    public int m_numOfRepititions;

    int m_index;

    List<GameObject> m_floors;

    public Text m_type;
    public Text m_paramValue;

    Transform m_actor;

    void Start()
    {
        m_actor = Camera.main.transform.parent;
        m_floors = new List<GameObject>();
        for (int i = 0; i < m_numOfTiles; ++i)
        {
            AddTrack(transform.position + transform.forward * (m_floorTextures[0].transform.localScale.z * 2.0f) * i);
        }
    }
    void Update()
    {
        MoveTreadmill();
        RaycastHit m_info;
        Ray ray = new Ray(m_actor.position - new Vector3(0.0f, m_actor.localScale.y, 0.0f), -m_actor.up);

        int layerMask = (1 << 8);

        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out m_info, Mathf.Infinity, layerMask))
        {
            m_type.text = "Type: " + m_info.collider.gameObject.name;
            if (m_info.collider.gameObject.name.Contains("Grass"))
                m_paramValue.text = "Value:\n2";
            if (m_info.collider.gameObject.name.Contains("Carpet"))
                m_paramValue.text = "Value:\n1";
            if (m_info.collider.gameObject.name.Contains("Tile"))
                m_paramValue.text = "Value:\n3";
        }
    }

    void AddTrack(Vector3 a_position)
    {
        int index = m_index / m_numOfRepititions;
        index = Mathf.Clamp(index, 0, 2);
        GameObject floor = Instantiate(m_floorTextures[index]);
        floor.name = m_floorTextures[index].name;
        if (a_position == Vector3.zero)
        {
            floor.transform.position = transform.position;
        }
        else
        {
            floor.transform.position = a_position;
        }
        m_floors.Add(floor);
        floor.transform.SetParent(this.gameObject.transform);
        floor.transform.rotation = Quaternion.FromToRotation(floor.transform.forward, transform.forward);
        m_index++;
        m_index %= 3 * m_numOfRepititions;
    }
    void MoveTreadmill()
    {
        //Move Tiles
        for (int i = 0; i < m_floors.Count; ++i)
        {
            m_floors[i].transform.position += transform.forward * m_speed * Time.deltaTime;
        }

        //Play particle emitter
        if ((transform.position - m_floors[0].transform.position).magnitude + (m_floors[0].transform.localScale.z * 0.5f) >= 6.0f)
        {
            if (m_floors[0].tag == "Grass")
            {
                m_grassParticleEmitter.Play();
            }
        }

        //Spawn new tiles
        float diff = (transform.position - m_floors[m_floors.Count - 1].transform.position).magnitude;
        if (diff >= m_floors[m_floors.Count - 1].transform.localScale.z * 2.0f)
        {
            if (m_floors.Count == m_numOfTiles)
            {
                Destroy(m_floors[0]);
                m_floors.RemoveAt(0);
            }
            AddTrack(m_floors[m_floors.Count - 1].transform.position - (transform.forward * m_floors[0].transform.localScale.z * 2.0f));
        }
    }

    void OnTriggerStay(Collider a_col)
    {
        if (a_col.gameObject.tag == "Player")
        {
            a_col.gameObject.GetComponent<ActorControls>().MoveActor(transform.forward * m_speed * Time.deltaTime);
        }
    }
}