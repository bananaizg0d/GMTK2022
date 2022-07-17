using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float pitchInSafeZone, normalPitch;
    [SerializeField] AudioSource src;
    SafeZone safeZone;

    IEnumerator Start()
    {
        src.pitch = pitchInSafeZone;
        GameObject go = null;

        while (go == null)
        {
            go = GameObject.FindWithTag(SafeZone.TAG);
            yield return new WaitForSeconds(0.1f);
        }

        safeZone = go.GetComponent<SafeZone>();

        safeZone.onSafeZoneOut += NormalizePitch;
    }

    void NormalizePitch()
    {
        src.pitch = normalPitch;
        safeZone.onSafeZoneOut -= NormalizePitch;
    }
}
