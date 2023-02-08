using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] private int currentView;
    [SerializeField] private int previousView;
    [SerializeField] private int maxView;

    public KeyCode nextKey = KeyCode.E;
    public KeyCode prevKey = KeyCode.Q;

    public GameObject[] views;
    public int p_currentView
    {
        get { return currentView; }
        set
        {
            if (value > maxView - 1)
            {
                currentView = 0;
            }
            else if (value < 0)
            {
                currentView = maxView - 1;
            }
            else
            {
                currentView = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxView = views.Length;
        currentView = 0;
        previousView = maxView;

        views[0].SetActive(true);
        for (int i = 1; i < maxView; i++)
        {
            views[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextKey))
        {
            ChangeView(1);
        }
        if (Input.GetKeyDown(prevKey))
        {
            ChangeView(-1);
        }
    }

    public void ChangeView(int dir)
    {
        previousView = currentView;
        p_currentView += dir;
        views[previousView].SetActive(false);
        views[currentView].SetActive(true);
    }
}