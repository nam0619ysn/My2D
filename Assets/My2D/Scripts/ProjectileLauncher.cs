using UnityEngine;
namespace My2D
{
    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        //참조
    
        public GameObject projectiletPreFab;
        public Transform firetPoint;
        #endregion

        #region Custom Method

      
        public void FireProjectile()
        {
            //Debug.Log("발사체를 발사위치에 ");
            GameObject projectile = Instantiate(projectiletPreFab, firetPoint.position, projectiletPreFab.transform.rotation);
            Vector3 oringScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3(
                oringScale.x * transform.parent.localScale.x > 0? 1: -1,
                oringScale.y,
                oringScale.z
                );

            Destroy(projectile, 3f);
        }
        #endregion

    }
}