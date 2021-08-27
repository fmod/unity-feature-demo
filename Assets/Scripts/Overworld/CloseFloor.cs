using UnityEngine;
using System.Collections;

public class CloseFloor : MonoBehaviour {

    public GameObject m_floor;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.y >= 1.459)
        {
            m_floor.SetActive(true);
            Destroy(this);
        }	
	}
}
