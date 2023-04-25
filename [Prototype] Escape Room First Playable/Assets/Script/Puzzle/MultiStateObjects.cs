using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiStateObjects : MonoBehaviour
{
    public enum ObjectState { LOCKED, UNLOCKED };

    [Header("State")]
    public ObjectState currentState = ObjectState.LOCKED;
    public GameObject locked;
    public GameObject unlocked;

    [Header("Events")]
    public UnityEvent onStart;
    public UnityEvent onEnable;
    public UnityEvent onStateChange;
    public UnityEvent onStateChangeLock;
    public UnityEvent onStateChangeUnlock;
    public UnityEvent onDisable;

    public void Awake()
    {
        if (onStart == null)
            onStart = new UnityEvent();
        if (onEnable == null)
            onEnable = new UnityEvent();
        if (onStateChange == null)
            onStateChange = new UnityEvent();
        if (onStateChangeLock == null)
            onStateChangeLock = new UnityEvent();
        if (onStateChangeUnlock == null)
            onStateChangeUnlock = new UnityEvent();
        if (onDisable == null)
            onDisable = new UnityEvent();
    }

    private void StateInitialized()
    {
        // Initialize
        switch (currentState)
        {
            case ObjectState.LOCKED:
                StateChangeLock();
                break;
            case ObjectState.UNLOCKED:
                StateChangeUnlock();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StateInitialized();
        onStart?.Invoke();
    }

    private void OnEnable()
    {
        StateInitialized();
        onEnable?.Invoke();
    }

    public void StateChangeUnlock()
    {
        locked.SetActive(false);
        unlocked.SetActive(true);
        currentState = ObjectState.UNLOCKED;
        onStateChange?.Invoke();
        onStateChangeUnlock?.Invoke();
    }

    public void StateChangeLock()
    {
        locked.SetActive(true);
        unlocked.SetActive(false);
        currentState = ObjectState.LOCKED;
        onStateChange?.Invoke();
        onStateChangeLock?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    // Update is called once per frame
    void Update() { }
}
