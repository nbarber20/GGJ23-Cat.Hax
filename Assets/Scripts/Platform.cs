using UnityEngine;

public class Platform : MonoBehaviour
{
    public float offTransparency = 0.1f;
    public float playerHeight = 0.0f;
    public float platformHeight = 0.0f;
    public bool activeCollider = true;
    private SpriteRenderer m_spriteRenderer = null;
    private PlayerController m_controller = null;
    private BoxCollider2D m_collider = null;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<BoxCollider2D>();
        m_controller = FindObjectOfType<PlayerController>();
        m_collider.size = new Vector2(m_spriteRenderer.size.x, m_collider.size.y);
    }

    void Update()
    {
        if(activeCollider && m_controller != null)
        {
            Color c = m_spriteRenderer.color;
            c.a = 1.0f;
            m_spriteRenderer.color = c;
            if(m_controller.transform.position.y - playerHeight > transform.position.y + platformHeight)
            {
                m_collider.enabled = true;
            }
            else if(m_controller.transform.position.y - playerHeight < transform.position.y - platformHeight)
            {
                m_collider.enabled = false;
            }
        }
        else
        {
            Color c = m_spriteRenderer.color;
            c.a = offTransparency;
            m_spriteRenderer.color = c;
            m_collider.enabled = false;
        }        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y + platformHeight, transform.position.z), 0.05f);
    }
}
