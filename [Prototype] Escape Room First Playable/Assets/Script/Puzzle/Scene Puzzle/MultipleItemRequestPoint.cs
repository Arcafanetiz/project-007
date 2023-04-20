using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleItemRequestPoint : MultiStateObjects
{
    public GameObject[] indicators;

    public UnityEvent OnStateChange;

    // Start is called before the first frame update
    void Start()
    {
        if (OnStateChange == null)
            OnStateChange = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if(!unlocked.activeInHierarchy)
        {
            if(ConditionCheck())
            {
                StateChangeUnlock();
                OnStateChange?.Invoke();
            }
        }
    }

    bool ConditionCheck()
    {
        foreach (GameObject point in indicators)
        {
            if(!point.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
