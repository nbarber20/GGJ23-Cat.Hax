using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    private enum rootState
    {
        IDLE,
        THROWN,
        PLUGGED,
    }
    [SerializeField]
    private float m_idleSpeed = 0.0f;
    [SerializeField]
    private float m_throwSpeed = 0.0f;
    [SerializeField]
    private float m_throwTime = 0.0f;
    [SerializeField]
    private Transform m_rootRoot = null;
    [SerializeField]
    private PlayerController m_playerController = null;
    private Vector2 dir = Vector2.right;
    private Vector2 aimDir = Vector2.right;
    private rootState m_state = rootState.IDLE;
    private float m_throwStart = 0.0f;
    private SpriteRenderer m_sprite = null;
    private AudioSource m_audioSource = null;

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal < 0)
        {
            aimDir.x = -1;
        }
        else if (horizontal > 0)
        {
            aimDir.x = 1;
        }

        switch (m_state)
        {
            case rootState.IDLE:
            {

                transform.position = Vector3.Lerp(transform.position, m_rootRoot.position, m_idleSpeed * Time.deltaTime);

                if (vertical < 0)
                {
                    m_state = rootState.THROWN;
                    m_throwStart = Time.time;
                    dir = aimDir;
                    m_playerController.Throw();
                }

                break;
            }
            case rootState.THROWN:
            {
                transform.position += (Vector3)dir * m_throwSpeed * Time.deltaTime;
                if(Time.time - m_throwStart > m_throwTime)
                {
                    m_state = rootState.IDLE;
                }
                break;
            }
        }
    }

    public void UnPlug()
    {
        m_sprite.enabled = true;
        m_state = rootState.IDLE;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_state != rootState.THROWN) return;
        Plug p = other.GetComponent<Plug>();
        if (p != null)
        {
            m_audioSource.Play();
            m_state = rootState.PLUGGED;
            m_sprite.enabled = false;
            p.PlugIn(this);
        }
    }
}
