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
            splashEffect.Stop(); // ������������� ������ ����� ��� ������
        }
    }

    public void StartSplashEffect()
    {
        if (splashEffect != null)
        {
            splashEffect.Play(); // ��������� ������ �����
        }
    }

    public void StopSplashEffect()
    {
        if (splashEffect != null)
        {
            splashEffect.Stop(); // ������������� ������ �����
        }
    }
}
