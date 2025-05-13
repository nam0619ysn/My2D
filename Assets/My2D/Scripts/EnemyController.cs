using UnityEditor.Tilemaps;
using UnityEngine;
namespace My2D
{
    //이동방향

    public enum WalkableDirection
    {
        Left,
        Right
    }

    //적 캐릭 관리
    [RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rb2D;
        private TouchingDirection touchingDirection;
        public DectectionZone detectionZone;
        public DectectionZone cliffeDetection;
        public Animator animator;
        //걷는 속도
        [SerializeField] private float walkSpeed = 4f;
        
        //이동방향
        private Vector2 directionVector=Vector2.right;

        //현이동방향 저장
        private WalkableDirection walkDirection = WalkableDirection.Right;

        private float stopRate = 0.2f;

        private bool hasTarget = false;
        private Damageable damageable;
        #endregion
        #region Property
      public WalkableDirection WalkDirection
      {
         get
         {
           return walkDirection;
         }
            set
            {
                //방향전환
                if (walkDirection != value)
                {
                    this.transform.localScale *= new Vector2(-1, 1);


                    if (value == WalkableDirection.Right)
                    {
                        directionVector = Vector2.right;
                    }
                    else if (value == WalkableDirection.Left)
                    {
                        directionVector = Vector2.left;
                    }
                }
                walkDirection = value;
            }
                            
      }
            public bool CannotMove
            {
                get
                {
                return animator.GetBool(AnimationString.cannotMove);
                }
            }
 
      public bool HasTarget
      {
            get
            {
                return hasTarget;
            }
            set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.hasTarget, value);
            }
      }

      //공격 쿨타임: 읽어서 0보다 크면 3초타이머 돌려 0으로 다시 파라미터 셋팅

        public float CoolDownTime
        {
            get
            {
                return animator.GetFloat(AnimationString.cooldownTime);
            }
            set
            {
                animator.SetFloat(AnimationString.cooldownTime, value);
            }
        }
        #endregion
        #region Unity Method
        private void Awake()
        {
           
            rb2D = this.GetComponent<Rigidbody2D>();
            touchingDirection = this.GetComponent<TouchingDirection>();
            
            damageable = this.GetComponent<Damageable>();
            damageable.hitAction += OnHit;
            cliffeDetection.noColliderRemain += Flip;

        }
        private void Update()
        {
            //적감지
            HasTarget= detectionZone.detectedColliders.Count > 0;

            //CooldownTime
            if (CoolDownTime > 0)
            {
                CoolDownTime -= Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            //벽을 만났을때 방향전환하여 이동
            if (touchingDirection.IsWall && touchingDirection.IsGround)
            {
                Flip();
            }
            if(CannotMove)
            {
                rb2D.linearVelocity = new Vector2(Mathf.Lerp(rb2D.linearVelocityX ,0f,stopRate) , rb2D.linearVelocityY);
            }
            else
            {
                rb2D.linearVelocity = new Vector2(directionVector.x * walkSpeed, rb2D.linearVelocityY);
            }
            // 지속적으로 우측으로 이동 유지
            


        }

        #endregion

        void Flip()
        {
           
            if(WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("방향 전환 에러");
            }
        }

        public void OnHit(float damage, Vector2 knockback)
        {
            rb2D.linearVelocity = new Vector2(knockback.x, rb2D.linearVelocityY + knockback.y);
        }

    }
}