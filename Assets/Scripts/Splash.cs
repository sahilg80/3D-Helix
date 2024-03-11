using System.Collections;
using UnityEngine;

public class Splash : MonoBehaviour
{
    float factor = 1f;
    // Start is called before the first frame update
    public void HandleObject(GameObject go)
    {
        StartCoroutine(FadeSplash(go));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeSplash(GameObject helix)
    {
        this.gameObject.transform.localScale = Random.Range(0.5f, 1.3f) * UnityEngine.Vector3.one;
        this.gameObject.transform.parent = helix.transform;
        float val = 1f;
        while (val > 0f)
        {
            Color color = this.gameObject.GetComponent<Renderer>().material.color;
            this.gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, val);
            val -= factor * Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.Instance.ReturnObjectToPool(this.gameObject);
//        DestroyImmediate(this.gameObject);
    }
}
