using TMPro;
using UnityEngine;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float moveSpeed = 10f;
        private RectTransform textTransform;

        private TextMeshProUGUI healthtext;
        //지연시간 티이머
        private float delayTime = 1f;
        private float delayCountdown = 0f;

        [SerializeField] public float fadeTime = 1f;
        private float fadeCountdown = 0f;

        private Color startColor;
        #endregion

        private void Awake()
        {
            textTransform = this.GetComponent<RectTransform>();
            healthtext= GetComponent<TextMeshProUGUI>(); 
            startColor = healthtext.color;
        }

        private void Update()
        {
            textTransform.position += Vector3.up * Time.deltaTime * moveSpeed;
            Fade();
        }

        private void Fade()
        {
            // 지연 시간 타이머
            if (delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
            }
            else
            {//지연시간 지남
                fadeCountdown += Time.deltaTime;
                float newAlpha = startColor.a * (1 - fadeCountdown / fadeTime);
                healthtext.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
                if (fadeCountdown >= fadeTime)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}