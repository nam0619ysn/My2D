using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace My2D
{
    public class FadeReMoveBehavior : StateMachineBehaviour
    {
        #region Timer Variables
        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;
        //지연시간 티이머
        public float delayTime = 1f;
        private float delayCountdown = 0f;

        //페이드 효과타이머
        public float fadeTime = 1f;
        private float fadeCountdown = 0f;

        private Color startColor;
        #endregion
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //참조
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            removeObject = animator.transform.parent.gameObject;
            //처기화
            startColor = spriteRenderer.color;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           // 지연 시간 타이머
            if(delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
            }
            else
            {//지연시간 지남
                fadeCountdown += Time.deltaTime;
                float newAlpha = startColor.a*(1 - fadeCountdown / fadeTime);
                spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b,newAlpha);
                if(fadeCountdown >= fadeTime)
                {
                    Destroy(removeObject);
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}