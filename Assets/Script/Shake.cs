using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public IEnumerator Tremblement(float duree, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while (elapsed < duree)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
