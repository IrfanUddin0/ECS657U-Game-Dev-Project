using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        if (clip == null)
            return;

        float volume_scale = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 1f;
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume * volume_scale;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }

    public static bool RngDifficultyScaled(float chance)
    {
        int difficulty = PlayerPrefs.HasKey("Difficulty") ? PlayerPrefs.GetInt("Difficulty") : 0;
        float rng = Random.Range(0.0f, 1.0f);
        return (rng <= chance * 1/Mathf.Pow(difficulty+1, 0.5f));
    }
}
