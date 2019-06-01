using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl2 : MonoBehaviour
{
    public float m_DampTime = 0.2f;         //相机移动和聚焦所用时间
    public float m_ScreenEdgeBuffer = 4f;   //相机视口边缘缓冲范围
    public float m_MinSize = 6.5f;          //最小焦距值
    //[HideInInspector]
    public Transform[] m_Targets;           //相机所要显示的所有目标          
    private Camera m_Camera;                //摄像机
    private float m_ZoomSpeed;              //视口缩放时的速度
    private Vector3 m_MoveVelocity;         //摄像机跟踪移动时的速度
    private Vector3 m_DesiredPosition;      //摄像机要跟随的位置 (2个或多个坦克的中间位置)

    private void Awake() {
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate() {

    }


    private void Move() {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position,m_DesiredPosition,ref m_MoveVelocity,m_DampTime);
    }

    private void FindAveragePosition() {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
        for (int i=0;i<m_Targets.Length;i++) {
            if (!m_Targets[i].gameObject.activeSelf) {
                continue;
            }
            averagePos += m_Targets[i].position;
            numTargets++;
        }
        if (numTargets>0) {
            numTargets /= numTargets;
            averagePos.y = transform.position.y;
            m_DesiredPosition = averagePos;
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
