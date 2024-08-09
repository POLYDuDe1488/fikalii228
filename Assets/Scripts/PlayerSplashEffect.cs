using UnityEngine;
using UnityEditor;

public class PlayerSplashEffect : MonoBehaviour
{
    private ParticleSystem splashEffect;

    void Start()
    {
        splashEffect = GetComponentInChildren<ParticleSystem>();
        if (splashEffect != null)
        {
            splashEffect.Stop(); // Останавливаем эффект брызг при старте
        }
    }

    public void StartSplashEffect()
    {
        if (splashEffect != null)
        {
            splashEffect.Play(); // Запускаем эффект брызг
        }
    }

    public void StopSplashEffect()
    {
        if (splashEffect != null)
        {
            splashEffect.Stop(); // Останавливаем эффект брызг
        }
    }
}
