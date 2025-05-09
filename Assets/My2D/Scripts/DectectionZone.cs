using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace My2D
{
    public class DectectionZone : MonoBehaviour
    {
        #region Variables
        public List<Collider2D> detectedColliders = new List<Collider2D>();
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //충돌체가 존에 들어오면 리스트 추가
            //Debug.Log($"{collision.name}가 충돌체가 존에 들어있다.");
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //충돌체가 존에 나가면 리스트 제거
            //Debug.Log($"{collision.name}가 충돌체가 존에 나갔다.");
            detectedColliders.Remove(collision);
        }
    }
}