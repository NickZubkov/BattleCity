using UnityEngine;

namespace BattleCity
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
                if (other.CompareTag("Point"))
                {
                    other.GetComponentInParent<Cell>().TakeDamage(other.gameObject, tag);
                }
                
                if (!other.CompareTag("MainCell") && !other.CompareTag(tag))
                {
                    Destroy(gameObject);
                }
        }
    }
}