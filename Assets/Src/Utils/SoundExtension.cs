using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundExtension
{
    public static void PlaySound(this GameObject gameObject, AudioClip clip, float? endIn = null)
    {
        if (clip == null) return;

        Vector3 pos = gameObject.transform.position;
        AudioSource.PlayClipAtPoint(clip, pos);

        if (endIn != null)
        {
            float endDelay = (float)endIn;
        }
    }
}
