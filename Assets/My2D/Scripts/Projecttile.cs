using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
namespace My2D
{
    public class Projecttile : MonoBehaviour
    {
        #region Variables

        //참조
        private Rigidbody2D rb2D;

      

        //좌우 이동
        [SerializeField] private Vector2 moveVelocity;

        [SerializeField]private float projectileDamage = 20f;
        [SerializeField] private Vector2 knockback = Vector2.zero;

        public GameObject projectEffectPreFab;
        public Transform EffectPos;
        #endregion

        #region UnityEvent
        private void Awake()
        {
            //참조
            rb2D = this.GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            //초기화
            rb2D.linearVelocity = new Vector2(moveVelocity.x * this.transform.localScale.x, moveVelocity.y); 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if(damageable)
            {
                Vector2 deliveredKnokback = this.transform.localScale.x > 0 ? knockback :new Vector2(-knockback.x,knockback.y);
                
                bool isHit=damageable.TakeDamage(projectileDamage, knockback);
                
                if (isHit)
                {
                    GameObject effecGo=Instantiate(projectEffectPreFab,EffectPos.position,Quaternion.identity);
                    Destroy(effecGo, 0.4f);
                }
               
            }
            Destroy(gameObject);
        }
        #endregion
    }
}