using UnityEngine;
namespace My2D
{//시차
    public class PlayerAxEffect : MonoBehaviour
    {
        #region Varials
        public Transform followTarget;
        public Camera cam;

        //시차 적용 배경의 시작 지점
        private Vector2 startingPosition;
        //시차 적용 배경의 처음 z위치 값
        private float staringZ;
        #endregion

        #region Property
        //카메라 시작지점으로 부터  이동 거리
        Vector2 camMoveSinceStart => startingPosition - (Vector2)cam.transform.position;
        // 플레이어와 베경 사이 거리
        float zDistanceFromTarget => transform.position.z - followTarget.position.z;

        //배경 위치에 따라 플레이어와의 거리
        float clippingPlane => cam.transform.position.z + ((zDistanceFromTarget > 0) ?cam.farClipPlane : cam.nearClipPlane);
        //플레이어 이동에 따른 배경 이동 거리 비율
        float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        #endregion

        #region Unity Event Method

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            ///초기화(배경 시작 위치 저장)
            startingPosition = this.transform.position;
            staringZ = this.transform.position.z;
        }
        
        // Update is called once per frame
        private void Update()
        {
            Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

            this.transform.position = new Vector3(newPosition.x, newPosition.y, staringZ);
        }
        #endregion
    }
}