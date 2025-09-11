using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    private void Awake()
    {
        Collider _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

}
