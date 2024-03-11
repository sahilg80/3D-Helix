using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ring : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    GameObject[] Rings;
    private bool isBallPassed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < transform.position.y && !isBallPassed)
        {
            AudioManager.Instance.PlaySound("PassThrough");
            for (int i=0; i<Rings.Length; i++)
            {
             //   if (Rings[i]==null) { return; }
                Rigidbody rb = Rings[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddExplosionForce(800f, Rings[i].transform.position,4f);
                Rings[i].transform.parent = null;
                Destroy(Rings[i], 2f);
            }
            HelixUIManager.Instance.UpdateProgressBar();
            Destroy(this.gameObject,7f);
            isBallPassed = true;
        }
    }
}
