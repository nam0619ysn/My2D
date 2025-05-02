using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //걷는 속도
        [SerializeField]private float walkSpeed = 4f;

        private Vector2 inputMove;
        #endregion



        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

      
        private void Awake()
        {
            rb2D = this.GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            // 리니어 벨로시티를 사용한 좌우 이동
            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }
        
       

    }
}