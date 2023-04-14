using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateObjects : MonoBehaviour
{
    public GameObject locked;
    public GameObject unlocked;

    public void StateChangeUnlock()
    {
        locked.SetActive(false);
        unlocked.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        locked.SetActive(true);
        unlocked.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }
}
