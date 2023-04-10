using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_playerController = null;

    [SerializeField]
    private float m_followSpeed = 0.0f;
    [SerializeField]
    float s1 = 0.0f;

    [SerializeField]
    float s2 = 0.0f;

    [SerializeField]
    private Transform bg1 = null;

    [SerializeField]
    private Transform bg2 = null;

    void Update()
    {
        float ypos = Mathf.Lerp(transform.position.y, m_playerController.transform.position.y, m_followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);

        bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, -ypos * s1, bg1.transform.localPosition.z);
        bg2.transform.localPosition = new Vector3(bg2.transform.localPosition.x, -ypos * s2, bg2.transform.localPosition.z);
    }
}
