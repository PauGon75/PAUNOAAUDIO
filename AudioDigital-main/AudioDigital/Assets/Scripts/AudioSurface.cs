using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AudioSurface : MonoBehaviour
{
    [SerializeField] private MoveBehaviour.SurfaceType surface = MoveBehaviour.SurfaceType.GRASS;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        MoveBehaviour.Surface = surface;
        Debug.Log(System.Enum.GetName(typeof(MoveBehaviour.SurfaceType), surface));
    }
}
