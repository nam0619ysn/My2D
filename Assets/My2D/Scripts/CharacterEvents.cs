using UnityEngine;
using UnityEngine.Events;
namespace My2D
{
    public class CharacterEvents
    {
        public static UnityAction <GameObject,float> characterDamaged;

        public static UnityAction<GameObject, float> characterHealed;
        
    }
}