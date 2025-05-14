using TMPro;
using UnityEngine;
namespace My2D
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;

        public Canvas gameCanvas;

        private Camera camera;
        [SerializeField]private Vector3 offset;
        #endregion

        #region Unity Events Method
        public void Awake()
        {
            camera = Camera.main;
        }

        private void OnEnable()
        {
            //함수등록
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHeal;
        }
        private void OnDisable()
        {
            //함수 제거
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHeal;
        }
        #endregion

        #region Custom Method
        public void CharacterTakeDamage(GameObject character,float damage)
        {
            
            
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);

            GameObject textGo=Instantiate(damageTextPrefab, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);
            //테스트 객체
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            if (damageText)
            {
                damageText.text = damage.ToString();
            }

        }

        public void CharacterHeal(GameObject character, float healAmount)
        {
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);
            //테스트 객체
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            if (healText)
            {
               healText.text = healAmount.ToString();
            }


        }

      
        #endregion
    }
}