using UnityEngine;

public class CameraControllerSimple : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    [SerializeField] float m_distance;
    [SerializeField] float m_vertical;
    [SerializeField] Vector3 m_lookAtOffset;

    void Update()
    {
        Vector3 offset = new Vector3(m_target.transform.forward.x * m_distance, m_target.transform.forward.y + m_vertical, m_target.transform.forward.z * m_distance);
        transform.position = m_target.transform.position - offset;
        transform.LookAt(m_target.transform.position + m_lookAtOffset);
    }
}
