/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      ThunderDome                                                     |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Creates sub branches if needed. The part that renders each line.|
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningZag : MonoBehaviour
{
    public bool m_isMainZag = false;
    public bool m_isLastZag = false;
    public int m_branchNum;
    public Lightning m_lightning;
    public string m_name;
    public Vector3 m_startPoint, m_endPoint;
    public Vector3 m_direction;
    public Vector2 m_width;

    List<LightningBranch> m_branches;
    LineRenderer m_renderer;

    public void Destroy()
    {
        for(int i = 0; i < m_branches.Count; ++i)
        {
            GameObject obj = m_branches[i].gameObject;
            m_branches[i].Destroy();
            Destroy(obj);
        }
        m_branches.Clear();
        Destroy(m_renderer);
    }

    void Start()
    {
        m_branches = new List<LightningBranch>();

        gameObject.name = m_name;
        m_renderer = gameObject.AddComponent<LineRenderer>();
        m_renderer.material = m_lightning.m_material;
        m_renderer.receiveShadows = false;
        m_renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        m_renderer.SetPosition(0, m_startPoint);
        m_renderer.SetPosition(1, m_endPoint);
        m_renderer.startWidth = m_width.x;
        m_renderer.endWidth = m_width.y;

        if(m_branchNum >= m_lightning.m_maxNumOfBranches - 1 || m_isLastZag)
        {
            return;
        }
        bool shouldBranch = (Random.Range(0, 100) <= (m_isMainZag ? m_lightning.m_mainBranchChance : m_lightning.m_subBranchChance) ? true : false);
        if(shouldBranch)
        {
            int numOfSplits = Random.Range((int)m_lightning.m_randNumOfSubSplits.x, (int)m_lightning.m_randNumOfSubSplits.y);
            for (int i = 0; i < numOfSplits; ++i)
            {
                GameObject branchObj = new GameObject();
                branchObj.name = "Branch" + m_branchNum;
                branchObj.transform.parent = this.transform;

                LightningSubBranch branch = branchObj.AddComponent<LightningSubBranch>();
                branch.m_lightning = this.m_lightning;
                branch.m_startDirection = m_direction;
                branch.m_startPosition = m_endPoint;
                branch.m_startWidth = m_width.y;
                branch.m_branchNum = m_branchNum + 1;
                m_branches.Add(branch);
            }
        }
    }
    void Update()
    {

    }
    void Branch()
    {

    }
}
