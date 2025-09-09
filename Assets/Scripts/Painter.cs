using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Painter : MonoBehaviour
{
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Random.ColorHSV();
    }
}
