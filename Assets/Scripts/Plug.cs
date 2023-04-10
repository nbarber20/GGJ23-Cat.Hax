using UnityEngine;
using TMPro;

public class Plug : MonoBehaviour
{
    public Platform[] m_platforms;
    public TextAsset m_words;
    public bool right = false;

    [SerializeField]
    private string word = "";

    [SerializeField]
    private GameObject m_textCanvas = null;
    [SerializeField]
    private TextMeshProUGUI m_textHint = null;
    [SerializeField]
    private TextMeshProUGUI m_textEnter = null;
    [SerializeField]
    private Animator m_animator = null;

    private bool m_plugged = false;
    private Root m_currentRoot = null;
    private SpriteRenderer m_spriteRenderer = null;
    private int index = 0;

    [System.Serializable]
    private class jsonObj
    {
        public string[] Words;
    }

    private void Awake()
    {
        var json = JsonUtility.FromJson<jsonObj>(m_words.text);
        word = json.Words[Random.Range(0, json.Words.Length)];

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Plugged", false);
        m_plugged = false;

        foreach (Platform t in m_platforms)
        {
            t.activeCollider = false;
        }
        m_textHint.text = word;
        m_textEnter.text = "";
        m_textCanvas.SetActive(false);

        if(right)
        {
            m_textHint.rectTransform.anchoredPosition = new Vector2((-0.3f * word.Length) - 0.5f, 0.832f);
            m_textEnter.rectTransform.anchoredPosition = new Vector2((-0.3f * word.Length) - 0.5f, 0.832f);
        }
        else
        {
            m_textHint.rectTransform.anchoredPosition = new Vector2(0.5f, 0.832f);
            m_textEnter.rectTransform.anchoredPosition = new Vector2(0.5f, 0.832f);
        }
    }

    private void Update()
    {
        if(m_plugged)
        {
            KeyCode c = (KeyCode)System.Enum.Parse(typeof(KeyCode), ""+word[index], true);
            if (Input.GetKeyDown (c))
            {
                m_textEnter.text += "" + word[index];
                index++;
                if(index>=word.Length)
                {
                    PlugOut();
                    Destroy(this);
                }
            }
        }
    }

    public void PlugIn(Root r)
    {
        m_animator.SetBool("Plugged", true);
        m_plugged = true;
        m_currentRoot = r;
        m_textCanvas.SetActive(true);
    }

    public void PlugOut()
    {
        if (m_currentRoot == null) return;
        m_plugged = false;
        m_animator.SetBool("Plugged", false);
        m_currentRoot.UnPlug();

        foreach(Platform t in m_platforms)
        {
            t.activeCollider = true;
        }
        m_textCanvas.SetActive(false);
    }
}
