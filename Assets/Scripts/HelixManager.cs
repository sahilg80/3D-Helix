using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{

    [SerializeField]
    List<GameObject> Rings;
    public int TotalRingsToSpawn { get; private set; }
    float yPos;
    // Start is called before the first frame update
    void Start()
    {
        int value = HelixUIManager.Instance.GetTotalLevelCleared();
        TotalRingsToSpawn = ((value + 1) *5) + 2;
        HelixUIManager.Instance.ResetParameters(TotalRingsToSpawn);
        yPos = 0;
        SpawnRingModel(0);
        for (int i = 1; i < TotalRingsToSpawn; i++)
        {
            SpawnRingModel(Random.Range(1, Rings.Count - 1));
        }
        SpawnRingModel(Rings.Count - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRingModel(int index)
    {
        if(index == Rings.Count -1)
        {
            yPos += 5f;
        }
        GameObject go = Instantiate(Rings[index], new Vector3(0, yPos, 0), Quaternion.identity);
        go.transform.parent = transform;
        yPos -= 10f;
    }

}
