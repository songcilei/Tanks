using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Complete {
    public class TankMovement2 : MonoBehaviour
    {

        public int m_PlayerNumber = 1;      //用来标记操作控制的是哪个TK(TK编号)      
        public float m_Speed = 12f;         //TK移动的速度
        public float m_TurnSpeed = 100f;    //TK每秒选择的角度
        public AudioSource m_MovementAudio; //播放声音
        public AudioClip m_EngineIdling;    //待机时的音频
        public AudioClip m_EngineDriving;   //移动时的音频
        public float m_PitchRange = 0.2f;   //音频大小的波动范围

        private string m_MovementAxisName;  //移动轴的名字
        private string m_TurnAxisName;      //旋转轴的名字
        private Rigidbody m_Rigidbody;      //刚体组件
        private float m_MovementInputValue; //当前输入的用来控制移动的数值
        private float m_TurnInputValue;     //输入的旋转值
        private float m_OriginalPitch;      //初始音频的值
        
        void Start()
        {
            //跟据编号不同，从键盘获取不同的值
            m_MovementAxisName = "Vertical" + m_PlayerNumber;
            m_TurnAxisName = "Horizontal" + m_PlayerNumber;
            //音源的音调设置为起始的音调
            m_OriginalPitch = m_MovementAudio.pitch;

        }

        private void Awake() {
            m_Rigidbody = GetComponent<Rigidbody>();//获取组件
        }

        private void OnEnable() {
            m_Rigidbody.isKinematic = false; //TK激活时，设置可参与物理碰撞
            m_MovementInputValue = 0f;   //移动 旋转值归0
            m_TurnInputValue = 0f;
        }

        private void OnDisable() {
            //TK禁用时不参与物理计算
            m_Rigidbody.isKinematic = true;
        }

        // Update is called once per frame
        void Update()
        {
            //跟据不同的输入给变量赋值
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        }

        private void EngineAudio() {
            //如果神马按键都没有按下
            if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
            {
                //如果当前播放的声音是正在移动的音频
                if (m_MovementAudio.clip == m_EngineDriving)
                {
                    //则把当前的声音设置为待机音频
                    m_MovementAudio.clip = m_EngineIdling;
                    //音频会在上面的区间内随机取一个值
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    //播放音频
                    m_MovementAudio.Play();

                }
            }
            else {
                //同上，如果按下了某个键，当前音频是待机音频
                if (m_MovementAudio.clip==m_EngineIdling) {
                    //则切换音频并播放
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch-m_PitchRange,m_OriginalPitch+m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
        }

        private void FixedUpdate() {

        }

        private void Move() {
            //创建一个变量，用来保存移动的距离
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
            //改变当前物体的位置
            m_Rigidbody.MovePosition(m_Rigidbody.position+movement);
        }


        private void Turn() {
            //每帧输入的值转换成角度值
            float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
            //旋转的角度
            Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
            //应用旋转的角度
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation*turnRotation);
        }

    }
}

