using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_sfxJump = null;
    [SerializeField]
    private AudioClip m_sfxLand = null;
    [SerializeField]
    private AudioClip m_sfxThrow = null;
    private AudioSource m_audioSource = null;
    [SerializeField]
    private Animator m_bodyAnim = null;
    [SerializeField]
    private LayerMask m_mask;
    [SerializeField]
    private float m_moveFriction = 0.0f;
    [SerializeField]
    private float m_friction = 0.0f;
    [SerializeField]
    private float m_jumpGrav = 0.0f;
    [SerializeField]
    private float m_normalGrav = 0.0f;
    [SerializeField]
    private float m_moveSpeed = 0.0f;
    [SerializeField]
    private float m_jumpSpeed = 0.0f;
    private Rigidbody2D m_rb = null;

    private bool grounded = false;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");

        m_rb.AddForce(transform.right * horizontal * m_moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

        bool falling = m_rb.velocity.y <= 0 && !grounded;
        if (falling)
        {
            m_rb.gravityScale = m_normalGrav;

            RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - transform.up, m_mask);
            if(hit && hit.normal.y > 0.01f)
            {
                m_audioSource.clip = m_sfxLand;
                m_audioSource.Play();
                grounded = true;
            }
        }

        if (jump && grounded)
        {
            m_rb.gravityScale = m_jumpGrav;
            grounded = false;
            m_rb.AddForce(transform.up * m_jumpSpeed, ForceMode2D.Impulse);
            m_bodyAnim.SetTrigger("Jump");
            m_audioSource.clip = m_sfxJump;
            m_audioSource.Play();
        }
        
        if(horizontal < 0)
        {
            m_bodyAnim.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
        else if(horizontal > 0)
        {
            m_bodyAnim.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        m_bodyAnim.SetBool("Falling", falling);
        m_bodyAnim.SetBool("Running", horizontal != 0);
        m_bodyAnim.SetBool("Grounded", grounded);

        if (!grounded || horizontal == 0)
        {
            m_rb.drag = m_friction;
        }
        else
        {
            m_rb.drag = m_moveFriction;
        }
    }
    
    public void Throw()
    {
        m_audioSource.clip = m_sfxThrow;
        m_audioSource.Play();
        m_bodyAnim.SetTrigger("Throw");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<KillCollider>())
        {
            SceneManager.LoadScene(1);
        }
    }
}