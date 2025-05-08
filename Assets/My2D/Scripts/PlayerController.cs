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
        //벽 천장
        private TouchingDirection touchingDirection;
        //걷는 속도
        [SerializeField]private float walkSpeed = 4f;
        [SerializeField]private float runSpeed = 7f;
        //점프시 좌우 이동 속도
        [SerializeField] private float airSpeed = 2f;
        
        private Vector2 inputMove;
        

        [SerializeField]private bool isMoving = false;
        [SerializeField]private bool isRunning = false;
        [SerializeField]private bool isJump = false;

        private bool isFacingRight = true;
        //점프 키 눌렀을깨 위로 올라가는 속도갑
        [SerializeField] private float jumpForce = 5f;
        #endregion

        #region Property
        public bool IsMoving
        {
            get { return isMoving; }
            set 
            {
                isMoving = value;
                animator.SetBool(AnimationString.isMoving, value);
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                animator.SetBool(AnimationString.isRunning, value);
            }
        }

        //현 이동 속도 - 읽기 전용
        public float CurrentSpeed
        {
            get
            {
                //인풋 값 들어왔을때 and 벽에 부딛치지 않았을 때
                if (IsMoving && touchingDirection.IsWall == false)
                {
                    if (touchingDirection.IsGround)
                    {
                        if (IsRunning)//시프트를 누르고 있을때
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

                        return airSpeed;
                    }
                    
                }
                else//공중
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
            touchingDirection = this.GetComponent<TouchingDirection>();
            rb2D = this.GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            // 리니어 벨로시티를 사용한 좌우 이동
            rb2D.linearVelocity = new Vector2(inputMove.x * CurrentSpeed, rb2D.linearVelocity.y);

            //애니메이처 속도값 셋팅
            animator.SetFloat(AnimationString.yVelocity, rb2D.linearVelocityY);
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
          
            if (context.started)//button down
            {
              
                IsRunning = true;
            }
            else if(context.canceled)//button Up
            { 
               
                IsRunning = false;
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)//buttondown
            {
                //속도 연산-위로 이동하는 속도값
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocityX, jumpForce);

                animator.SetTrigger(AnimationString.jumpTrigger);      
            }
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirection.IsGround)//mouse left buttondown
            {
                
                animator.SetTrigger(AnimationString.attackTrigger);
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