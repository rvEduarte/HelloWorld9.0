using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASERPOINT : MonoBehaviour
{
    [SerializeField] private float DistanceRay = 10;
    public Transform LaserFirePoint;
    public LineRenderer m_LineRenderer;
    Transform m_Tranform;
    

    private void Awake()
    {
        m_Tranform = GetComponent<Transform>();
    }

    private void Start()
    {
        if(Physics2D.Raycast(m_Tranform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_Tranform.position, transform.right);
            Draw2DRay(LaserFirePoint.position, _hit.point);

        }
        else
        {
            Draw2DRay(LaserFirePoint.position, LaserFirePoint.transform.right * DistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_LineRenderer.SetPosition(0, startPos);
        m_LineRenderer.SetPosition(1, endPos);
    }
}
