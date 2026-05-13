using UnityEngine;
using System;


public class FootStepManager : MonoBehaviour
{
    [Serializable]
    struct SurfaceFootstep
    {
        [SerializeField] private SurfaceType m_Surface;
        [SerializeField] private AudioClip[] m_footsteps;
    
        public AudioClip GetAudio()
        {
            if (m_footsteps == null || m_footsteps.Length == 0) return null;

            return m_footsteps[UnityEngine.Random.Range(0, m_footsteps.Length)];
        }
        
     
    }

    [SerializeField] private SurfaceFootstep[] m_FootstepsSurfaces;
        
    public enum SurfaceType { WOOD, GRASS, SAND};

    public static SurfaceType Surface = SurfaceType.GRASS;

    public void PlayFootstep()
    {

    }
}
