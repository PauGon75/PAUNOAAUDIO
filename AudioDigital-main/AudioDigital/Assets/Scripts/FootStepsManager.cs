using UnityEngine;
using System;
using System.Collections.Generic;
using Invector.vCharacterController;

public class FootStepsManager : MonoBehaviour
{
    [Serializable]
    public struct SurfaceFootstep
    {
        public string surfaceName;
        public vThirdPersonController.TerrainType surfaceType;
        public List<AudioClip> footsteps;

        public AudioClip GetRandomClip()
        {
            if (footsteps == null || footsteps.Count == 0) return null;
            return footsteps[UnityEngine.Random.Range(0, footsteps.Count)];
        }
    }

    [Header("Footstep Setup")]
    public List<SurfaceFootstep> m_FootstepsSurfaces;
}