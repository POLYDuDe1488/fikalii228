using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; // »сточник фоновой музыки
    public AudioSource deathMusicSource; // »сточник музыки при смерти
    public AudioSource additionalSoundSource; // ƒополнительный звук

    void Start()
    {
        // ќстанавливаем все аудиоисточники при запуске
        StopAllSounds();
    }

    public void StartGame()
    {
        // ќстанавливаем музыку смерти и дополнительный звук, если они играют
        deathMusicSource.Stop();
        additionalSoundSource.Stop();

        // «апускаем фоновую музыку и дополнительный звук
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
        additionalSoundSource.Play();
    }

    public void ShowDeathScreen()
    {
        // ќстанавливаем фоновую музыку и дополнительный звук
        backgroundMusicSource.Stop();
        additionalSoundSource.Stop();

        // «апускаем музыку смерти
        deathMusicSource.Play();
    }

    public void RestartGame()
    {
        // ќстанавливаем музыку смерти
        deathMusicSource.Stop();

        // «апускаем фоновую музыку и дополнительный звук
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
