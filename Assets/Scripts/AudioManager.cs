using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; // �������� ������� ������
    public AudioSource deathMusicSource; // �������� ������ ��� ������
    public AudioSource additionalSoundSource; // �������������� ����

    void Start()
    {
        // ������������� ��� �������������� ��� �������
        StopAllSounds();
    }

    public void StartGame()
    {
        // ������������� ������ ������ � �������������� ����, ���� ��� ������
        deathMusicSource.Stop();
        additionalSoundSource.Stop();

        // ��������� ������� ������ � �������������� ����
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
        additionalSoundSource.Play();
    }

    public void ShowDeathScreen()
    {
        // ������������� ������� ������ � �������������� ����
        backgroundMusicSource.Stop();
        additionalSoundSource.Stop();

        // ��������� ������ ������
        deathMusicSource.Play();
    }

    public void RestartGame()
    {
        // ������������� ������ ������
        deathMusicSource.Stop();

        // ��������� ������� ������ � �������������� ����
        backgroundMusicSource.Play();
        additionalSoundSource.Play();
    }

    private void StopAllSounds()
    {
        backgroundMusicSource.Stop();
        deathMusicSource.Stop();
        additionalSoundSource.Stop();
    }
}
