using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_score = null;
    private int scoreNum = 0;
    [SerializeField]
    private float segHeight = 5.0f;
    [SerializeField]
    private GameObject[] m_segments;
    [SerializeField]
    private PlayerController m_player = null;
    private float m_maxPlayerHeight = 0.0f;
    private float m_maxPlatformHeight = 0.0f;
    private List<GameObject> m_spawned;

    private void Awake()
    {
        m_spawned = new List<GameObject>();
    }

    public void Update()
    {
        if(m_player.transform.position.y+10.0f > m_maxPlayerHeight)
        {
            m_maxPlayerHeight = m_player.transform.position.y+10.0f;
        }

        if(m_maxPlayerHeight>m_maxPlatformHeight)
        {
            m_spawned.Add(Instantiate(m_segments[Random.Range(0, m_segments.Length)], new Vector3(0, m_maxPlatformHeight, 0), Quaternion.identity));
            m_maxPlatformHeight += segHeight;
            scoreNum++;
        }

        if(m_spawned.Count>5)
        {
            GameObject g = m_spawned[0];
            m_spawned.RemoveAt(0);
            Destroy(g);
        }

        m_score.text = "" + Mathf.Max(scoreNum-3,0);
    }
}
