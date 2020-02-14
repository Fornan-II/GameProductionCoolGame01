using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSelfDestruct : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private bool _hasPlayed;
    
    private void Update()
    {
        if (_hasPlayed)
        {
            if (_particles.isPlaying)
            {
                _hasPlayed = true;
            }
        }
        else if (_particles.isStopped)
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!_particles)
            _particles = GetComponent<ParticleSystem>();
    }
#endif
}