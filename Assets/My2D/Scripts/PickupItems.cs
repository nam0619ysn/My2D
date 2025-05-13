using UnityEngine;

namespace My2D
{
    public class PickupItems : MonoBehaviour
    {
        #region Variable
       
        //회전속도 y축 기준
        private Vector3 spinRotateSpeed = new Vector3(0f, 180f, 0f);
      

        [SerializeField]
        private float restoreHealth = 30f;

        private AudioSource pickupSource;
        #endregion

        #region Unity EventMethod

       

        private void Update()
        {
            transform.eulerAngles += spinRotateSpeed * Time.deltaTime;
        }
        private void Awake()
        {
            pickupSource = this.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log($"플레이어 회복:{restoreHealth}");

            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable)
            {
                bool isHeal= damageable.Heal(restoreHealth);
                if(isHeal)
                {
                    if (pickupSource)
                    {
                         pickupSource.PlayOneShot(pickupSource.clip);
                    }
                   
                     Destroy(gameObject);
                }

               
            }
        }
        #endregion
    }
}