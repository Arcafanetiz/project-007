using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInteractables : MonoBehaviour
{
    public UnityEvent OnClickEvent;

    private void Awake()
    {
        if (OnClickEvent == null)
            OnClickEvent = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
