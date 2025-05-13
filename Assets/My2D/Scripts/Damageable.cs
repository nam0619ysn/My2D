using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Varables
        public Animator animator;
       

       [SerializeField] private float currentHealth;

       [SerializeField]private float maxHealth = 100;

        private bool isDeath = false;

        //무적 타이머
        private bool isInvincible = false; //true면 데메지 입지 않는다.
        [SerializeField]
        private float invincibleTIme = 3f;
        private float countdown = 0f;

        //델리게이트 함수 - 매개변수로 float Vector2
        public UnityAction<float, Vector2> hitAction;
        #endregion

        #region Property
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }

            set
            {
                currentHealth = value;
                if(currentHealth <= 0)
                {
                    Die();
                }
            }

        }

        public float MaxHealth 
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }

        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            private set
            {
                isDeath = value;
            }
        }

        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.lockVelocity);
            }
            set
            {
                animator.SetBool(AnimationString.lockVelocity, value);
            }
        }

        public bool IsHealthFull => CurrentHealth >= MaxHealth;
        #endregion

        #region UNity Event Method
        private void Start()
        {
            CurrentHealth = MaxHealth;
        }
        private void Update()
        {
            //타이머

            if (isInvincible)
            {
                countdown += Time.deltaTime;

                if (countdown >= invincibleTIme)
                {
                    //타이머기능
                    isInvincible = false;
                  
                }
            }
        }
        #endregion

        #region Custom Method
        //헬스 감산
        public bool TakeDamage (float damage,Vector2 knockback)
        {
           
            if (IsDeath || isInvincible)
            {
                return false;
            }

            CurrentHealth -= damage;
            Debug.Log($"CurrentHealth:{CurrentHealth}");

            //무적모드 셋팅 = 타이머 초기화
            isInvincible = true;
            countdown=0;
            //애니메이션
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;


            //효과 sfc vfx 넉백효과 UI
            //델리게이트 함수에 등록될 함수 호출:
            //if(hitAction != null)
            //{
            //    hitAction.Invoke(damage,knockback);
            //}
            hitAction?.Invoke(damage, knockback);
            //UI효과 데미지 text 프리팹 생성하는 람수가 등록된
            CharacterEvents.characterDamaged?.Invoke(gameObject,damage);
            return true;
        }
        private void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
        }

        //Health 가ㄴ산
        public bool Heal(float healAmount)
        {
            if (IsDeath || IsHealthFull )
            {
                return false;
            }
            CurrentHealth += healAmount;

            if (CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
            Debug.Log($"CurrentHealth:{CurrentHealth}");
            return true;
        }
        #endregion
    }
}