using UnityEngine;

public class Box : MonoBehaviour
{
    // DEFINE THE TAKE DAMAGE METHOD HERE:
    
    
    
    // ----------------------------------------
    
    
    
    
    
    
    
    
    
    
    
    
    
    // ...
    
    [SerializeField] private GameObject _upgradePickupPrefab;
    
    private void OnBoxDestroyed()
    {
        SpawnUpgrade();
        Destroy(gameObject);
    }
    
    private void SpawnUpgrade()
    {
        if (_upgradePickupPrefab != null)
            Instantiate(_upgradePickupPrefab, transform.position, Quaternion.identity);
    }
}
