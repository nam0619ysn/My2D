using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        //애니메이션
        public Animator animator;
        //걷는 속도
        [SerializeField]private float walkSpeed = 4f;
        [SerializeField]private float runSpeed = 7f;
        
        private Vector2 inputMove;
        

        [SerializeField]private bool isMoving = false;
        [SerializeField]private bool isRunning = false;

        private bool isFacingRight = true;
        #endregion

        #region Property
        public bool IsMoving
        {
            get { return isMoving; }
            set 
            {
                isMoving = value;
                animator.SetBool(AniamationString.isMoving, value);
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                animator.SetBool(AniamationString.isRunning, value);
            }
        }

        //현 이동 속도 - 읽기 전용
        public float CurrentSpeed
        {
            get
            {
                if (IsMoving)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {
                   return 0f;//idle 상태
                }
            }
        }

        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                //반전구현
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1,1);
                }
                isFacingRight = value;
            }
        }
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
            rb2D.linearVelocity = new Vector2(inputMove.x * CurrentSpeed, rb2D.linearVelocity.y);
        }

        #region Custom Method
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();

             SetFacingDirection(inputMove);
            //인풋 값이 들어오면 IsMove 
            IsMoving = (inputMove != Vector2.zero);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
          
            if (context.started)
            {
                Debug.Log("Shift Click");
                IsRunning = true;
            }
            else if(context.canceled)
            {
                Debug.Log("Shift Click Cancle");
                IsRunning = false;
            }
        }

        void SetFacingDirection(Vector2 moveInput)
        {
            
            if (moveInput.x > 0f &&IsFacingRight==false)// 왼쪽 바라보고 우 이동
            {
                IsFacingRight = true;
            }
            else if(moveInput.x <0f && IsFacingRight == true)// 오른쪽 바라보고 좌 이동
            {
                IsFacingRight = false;
            }
        }
        #endregion
    }
}