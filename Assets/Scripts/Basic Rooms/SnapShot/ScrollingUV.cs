using UnityEngine;

public class ScrollingUV : MonoBehaviour
{
    public int m_materialIndex = 0;
    public float scrollSpeed = 0;
    public bool m_randSpeed = false;
    public float m_randMin = 1;
    public float m_randMax = 2;
	float m_elapsed = 0.0f;
    Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        if (m_randSpeed)
            scrollSpeed = Random.Range(m_randMin, m_randMax);
	}
	
	// Update is called once per frame
	void Update ()
    {
		m_elapsed += Time.deltaTime * scrollSpeed;
        if (rend.materials.Length > 1)
            rend.materials[m_materialIndex].mainTextureOffset = new Vector2(0, m_elapsed);
        else
            rend.material.mainTextureOffset = new Vector2(0, m_elapsed);
    }
}
