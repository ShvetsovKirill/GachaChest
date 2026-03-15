using System.Collections.Generic;
using UnityEngine;

namespace Gacha_Test
{
  public class EffectsActivator : MonoBehaviour
  {
    [SerializeField] private List<ParticleSystem> _effectsIdle;
    [SerializeField] private List<ParticleSystem> _effectsOpen;

    public void Activate(bool active)
    {
      if (!active)
        ShowIdleEffects();
      else
        ShowOpenEffects();
    }

    private void ShowIdleEffects()
    {
      foreach (var effect in _effectsIdle)
        effect.Play();
      
      foreach (var effect in _effectsOpen)
        effect.Stop();
    }
    
    private void ShowOpenEffects()
    {
      foreach (var effect in _effectsIdle)
        effect.Stop();
      
      foreach (var effect in _effectsOpen)
        effect.Play();
    }
  }
}