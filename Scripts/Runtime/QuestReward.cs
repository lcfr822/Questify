using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestReward : MonoBehaviour
{
    [Range(0, 1000)]
    public int xpReward = 0;
    public GameObject customReward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
