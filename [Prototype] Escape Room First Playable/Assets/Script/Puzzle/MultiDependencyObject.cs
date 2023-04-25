using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDependencyObject : MultiStateObjects
{
    public MultiStateObjects[] dependencies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ObjectState.LOCKED)
        {
            if (ConditionCheck())
            {
                StateChangeUnlock();
            }
        }
    }

    bool ConditionCheck()
    {
        foreach (MultiStateObjects obj in dependencies)
        {
            if (obj.currentState == ObjectState.LOCKED)
            {
                return false;
            }
        }
        return true;
    }
}
