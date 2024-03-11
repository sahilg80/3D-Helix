using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float bounceForce;
    [SerializeField]
    GameObject splashPrefab;
    [SerializeField]
    GameObject helix;
    Rigidbody body;
    //float factor  = 1f;
    bool isLevelFinish;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        isLevelFinish = false;
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        body.velocity = new Vector3(0, Time.deltaTime * bounceForce, 0);
        //GameObject splash = Instantiate(splashPrefab,  new Vector3(transform.position.x,transform.position.y-0.3f,transform.position.z), splashPrefab.transform.rotation);
        GameObject splash = ObjectPoolManager.Instance.GetGameObjectFromPool(splashPrefab, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), splashPrefab.transform.rotation);
        CheckForSafety(collision.gameObject);
        splash.GetComponent<Splash>().HandleObject(helix);
        //StartCoroutine(FadeSplash(splash));
    }

    private void CheckForSafety(GameObject go)
    {
        if (go.CompareTag("Safe"))
        {
            Debug.Log("You are safe");
            AudioManager.Instance.PlaySound("Landed");
        }
        else if (go.CompareTag("Unsafe") && !isLevelFinish)
        {
            HelixUIManager.Instance.GameOverPanel();
            isLevelFinish =true;
        }
        else if (go.CompareTag("LastRing") && !isLevelFinish)
        {
            HelixUIManager.Instance.GameWinPanel();
            isLevelFinish =true;
        }
    }

    //public IEnumerator FadeSplash(GameObject go)
    //{
    //    go.transform.localScale = Random.Range(0.5f,1.3f)*Vector3.one;
    //    go.transform.parent = helix.transform;
    //    float val = 1f;
    //    while(val > 0f)
    //    {
    //        Color color = go.GetComponent<Renderer>().material.color;
    //        go.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, val);
    //        val -= factor * Time.deltaTime;
    //        yield return null;
    //    }
    //    DestroyImmediate(go);
    //}

}
