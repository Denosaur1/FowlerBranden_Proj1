using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraShake : MonoBehaviour
{
    private PostProcessVolume volume;
    private Vignette vignette;

    private void Awake()
    {
       
    }

    public IEnumerator Shake(float duration, float magnitude) {

        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0;
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);
            vignette.intensity.Interp(0,0.3f,duration);



            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        vignette.intensity.value = 0;
    }
}
