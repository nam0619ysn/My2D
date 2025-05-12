using UnityEngine;

namespace My2D
{  
    //float타입 변수 관리
   //상태 머신에 들어갈때 나올때 값 설정
    public class SetFloatBehavior : StateMachineBehaviour
    {

        #region Variables
        //값을 설정할 이름
        public string floatlName;

        //작동하는 상태, 들어올 때, 나갈 때 체크
        public bool updateOnStateEnter;
        public bool updateOnStateExit;
        //작동하는 상태, 상태머신 체크
        public bool updateOnStateMachineEnter;
        public bool updateOnStateMachineExit;

        //들어갈때롸 나올때의 값 설정
        public float valueEnter;
        public float valueExit;
        #endregion
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateEnter)
            {
                animator.SetFloat(floatlName, valueEnter);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateExit)
            {
                animator.SetFloat(floatlName, valueExit);
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachineEnter)
            {
                animator.SetFloat(floatlName, valueEnter);
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateExit)
            {
                animator.SetFloat(floatlName, valueExit);
            }
        }
    }
    }