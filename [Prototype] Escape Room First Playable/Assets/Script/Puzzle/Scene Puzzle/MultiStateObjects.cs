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

    public void StateChangeLock()
    {
        locked.SetActive(true);
        unlocked.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        StateChangeLock();
    }

    // Update is called once per frame
    void Update() { }
}
