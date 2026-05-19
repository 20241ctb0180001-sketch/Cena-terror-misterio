using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using System.Runtime.Serialization;

namespace Artemis
{
    [RequireComponent(typeof(CharacterController))]
    public class FPControler : MonoBehaviour
    {
        [Header("moveParameters")]
        public float MaxSpeed => SprintInput ? RunSpeed : WalkSpeed;
        public float acceleration = 3f;

        [SerializeField] float WalkSpeed = 0.5f;
        [SerializeField] float RunSpeed = 1f;
        //[SerializeField] float JumpHeight = 2f;

        [Header("LookParameters")]
        public Vector2 LookSens = new Vector2(0.1f, 0.1f);
        public float PitchLimit = 85f;
        [SerializeField] float CurrPitch = 0f;

        public float CurrentPitch
        {
            get => CurrPitch;
            set
            {
                CurrPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
            }
        }

        [Header("Physiscs")]
        //[SerializeField] float GravityScale = 3f;
        public Vector3 CurrVelocity { get; private set; }
        public float CurrSpeed { get; private set; }
        public bool grounded => controller.isGrounded;

        [Header("input")]
        public Vector2 MoveInput;
        public Vector2 LookInput;
        public bool SprintInput;


        [Header("Components")]
        [SerializeField] CinemachineCamera fpCam;
        [SerializeField] CharacterController controller;

        #region Unity Methods
        void OnValidate() { if (controller == null) { controller = GetComponent<CharacterController>(); } }

        void Update()
        {
            MoveUpdate();
            LookUpdate();
        }

        #endregion

        #region ControlerMethods
        void MoveUpdate()
        {
            Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
            motion.y = 0;
            motion.Normalize();

            if (motion.sqrMagnitude >= 0.01f)
            {
                CurrVelocity = Vector3.MoveTowards(CurrVelocity, motion * MaxSpeed, acceleration * Time.deltaTime);
            }
            else { CurrVelocity = Vector3.MoveTowards(CurrVelocity, Vector3.zero, acceleration * Time.deltaTime); }

            float verticalVel = Physics.gravity.y * 20 * Time.deltaTime;

            Vector3 fullVel = new Vector3(CurrVelocity.x, verticalVel, CurrVelocity.z);
            controller.Move(fullVel * Time.deltaTime);
            CurrSpeed = CurrVelocity.magnitude;
        }
        void LookUpdate()
        {
            Vector2 input = new Vector2(LookInput.x * LookSens.x, LookInput.y * LookSens.y);
            //up/down
            CurrentPitch -= input.y;
            fpCam.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);
            //left/right
            transform.Rotate(Vector3.up * input.x);
        }

        #endregion
    }
}