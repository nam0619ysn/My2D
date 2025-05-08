using UnityEngine;
namespace My2D
{
    public class TouchingDirection : MonoBehaviour
    {

        #region Varials
        public Animator animator;
        private CapsuleCollider2D touchingCollider;

        [SerializeField] private float groundDistance = 0.05f;//그라운드
        [SerializeField] private float cellingDistance = 0.05f;//천정 체크
        [SerializeField] private float wallDistance = 0.2f;//벽 체크

        [SerializeField]private ContactFilter2D contactFilter;
        //RaycastHit2D[]배열
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] cellingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];

        //Cast 체크
        [SerializeField]private bool isGround;
        [SerializeField]private bool isCelling;//천장
        [SerializeField]private bool isWall;
        #endregion

        #region Property
        public bool IsGround
        {
            get
            {
                return isGround;
            }
            private set
            {
                isGround = value;
                animator.SetBool(AnimationString.isGround, value);
            }
        }

        public bool IsCelling
        {
            get
            {
                return isCelling;
            }
            set
            {
                isCelling = value;
                animator.SetBool(AnimationString.isCelling, value);
            }
        }

        public bool IsWall
        {
            get
            {
                return isWall;
            }
            set
            {
                isWall = value;
                animator.SetBool(AnimationString.isWall, value);
            }
        }

        private Vector2 WallCheckDirection => (this.transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            touchingCollider = this.GetComponent<CapsuleCollider2D>();
        }
        private void FixedUpdate()
        {
            IsGround=(touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0);
            IsCelling = (touchingCollider.Cast(Vector2.up, contactFilter, cellingHits, cellingDistance) > 0);
            IsWall = (touchingCollider.Cast(WallCheckDirection, contactFilter, wallHits, wallDistance) > 0);
        }

        #endregion
    }
}