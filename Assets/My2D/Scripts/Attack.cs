using UnityEngine;
namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region Varables
        [SerializeField]
        private float attackDamage = 10f;
       
        //넉백
        [SerializeField]
        public Vector2 knockback = Vector2.zero;
        #endregion

        #region UNity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {

            //Debug.Log($"플레이이어에게 데미지 {attackDamage}");
            //collison 에서 Damageable 컴포넌트 찾아서 TakeDamage 줏[요
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                //공겻하는 캐릭터의 방향엘 따라 밀리는 방향 설정
                Vector2 deliveredKncockback = this.transform.parent.parent.localScale.x > 0
                    ? knockback : new Vector2(knockback.x, knockback.y);

                bool isHit= damageable.TakeDamage(attackDamage,knockback);

                if (isHit)
                {
                    Debug.Log(collision.name + "hir for" + attackDamage);
                }
            }

        }
        #endregion

        
    }
}