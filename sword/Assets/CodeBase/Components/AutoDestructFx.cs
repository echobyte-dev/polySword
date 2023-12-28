using System.Collections;
using UnityEngine;

namespace CodeBase.Components
{
  [RequireComponent(typeof(ParticleSystem))]
  public class AutoDestructFx : MonoBehaviour
  {
    private void OnEnable()
    {
      StartCoroutine(CheckIfAlive());
    }

    private IEnumerator CheckIfAlive()
    {
      ParticleSystem particleSystem = GetComponent<ParticleSystem>();

      while (particleSystem != null)
      {
        yield return new WaitForSeconds(0.5f);

        if (!particleSystem.IsAlive(true))
        {
          gameObject.SetActive(false);
        }
        else
        {
          Destroy(gameObject);
          break;
        }
      }
    }
  }
}