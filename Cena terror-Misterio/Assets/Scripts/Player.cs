using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Artemis
{
    [RequireComponent(typeof(FPControler))]
    public class Player : MonoBehaviour
    {
        [Header ("components")]
        [SerializeField] FPControler FpCtrl;

        #region InputHandling
        void OnMove(InputValue value)
        {
            FpCtrl.MoveInput = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            FpCtrl.LookInput = value.Get<Vector2>();
        }

        #endregion

        #region UnityMethods
        void OnValidate()
        { 
            if(FpCtrl == null) FpCtrl = GetComponent<FPControler>(); 
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion
    }
}
