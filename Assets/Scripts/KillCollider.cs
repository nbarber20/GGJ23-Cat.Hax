using UnityEngine;

public class KillCollider : MonoBehaviour
{
    [SerializeField]
    public float m_speed = 1.0f;
    [SerializeField]
    public float m_maxDist = 0.0f;
    [SerializeField]
    public PlayerController m_playerController = null;

    void Update()
    {
        if(m_playerController.transform.position.y - m_maxDist > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, m_playerController.transform.position.y - m_maxDist, transform.position.z);
        }
        transform.position += new Vector3(0, m_speed * Time.deltaTime, 0);
    }
}
