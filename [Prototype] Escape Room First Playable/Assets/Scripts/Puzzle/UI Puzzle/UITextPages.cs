﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class UITextPages : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private TMPro.TMP_Text pageNo;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;

    [Header("Swipe Settings")]
    public int startingPage = 0;
    public float fastSwipeThresholdTime = 0.3f;
    public int fastSwipeThresholdDistance = 100;
    public float decelerationRate = 10f;

    private int _fastSwipeThresholdMaxLimit;
    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;
    private bool _horizontal;
    
    // number of pages in container
    private int pageCount;
    private int currentPage;

    // whether lerping is in progress and target lerp position
    private bool _lerp;
    private Vector2 _lerpTo;

    // target position of every page
    private List<Vector2> _pagePositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    void Start() 
    {
        prevButton.SetActive(false);
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        pageCount = _container.childCount;

        // is it horizontal or vertical scrollrect
        if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical)
        {
            _horizontal = true;
        }
        else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical) 
        {
            _horizontal = false;
        } 
        else 
        {
            Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
            _horizontal = true;
        }

        _lerp = false;

        // initialization
        SetPagePositions();
        SetPage(startingPage);
        pageNo.text = (currentPage + 1) + "/" + pageCount;

        // prev and next buttons
        if (nextButton)
            nextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });
        if (prevButton)
            prevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
	}

    void Update()
    {
        if (currentPage == 0)
        {
            prevButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
        }

        if (currentPage == pageCount - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        // if moving to target position
        if (_lerp) 
        {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
            {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }
        }
    }

    private void SetPagePositions() {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int containerWidth = 0;
        int containerHeight = 0;

        if (_horizontal) 
        {
            // screen width in pixels of scrollrect window
            width = (int)_scrollRectRect.rect.width;
            // center position of all pages
            offsetX = width / 2;
            // total width
            containerWidth = width * pageCount;
            // limit fast swipe length - beyond this length it is fast swipe no more
            _fastSwipeThresholdMaxLimit = width;
        }
        else
        {
            height = (int)_scrollRectRect.rect.height;
            offsetY = height / 2;
            containerHeight = height * pageCount;
            _fastSwipeThresholdMaxLimit = height;
        }

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _pagePositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < pageCount; i++) {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            if (_horizontal) {
                childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            } else {
                childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
            }
            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
        }
    }

    private void SetPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        currentPage = aPageIndex; 
    }

    private void LerpToPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, pageCount - 1);
        _lerpTo = _pagePositions[aPageIndex];
        _lerp = true;
        currentPage = aPageIndex;
    }
    private void NextScreen()
    {
        LerpToPage(currentPage + 1);
        pageNo.text = (currentPage + 1) + "/" + pageCount;
    }

    private void PreviousScreen()
    {
        LerpToPage(currentPage - 1);
        pageNo.text = (currentPage + 1) + "/" + pageCount;
    }

    private int GetNearestPage()
    {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = currentPage;

        for (int i = 0; i < _pagePositions.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }

    public void OnBeginDrag(PointerEventData aEventData)
    {
        // if currently lerping, then stop it as user is draging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }

    public void OnEndDrag(PointerEventData aEventData)
    {
        // how much was container's content dragged
        float difference;
        if (_horizontal)
        {
            difference = _startPosition.x - _container.anchoredPosition.x;
        }
        else
        {
            difference = - (_startPosition.y - _container.anchoredPosition.y);
        }

        // test for fast swipe - swipe that moves only +/-1 item
        if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime && Mathf.Abs(difference) > fastSwipeThresholdDistance && Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit)
        {
            if (difference > 0)
            {
                NextScreen();
            }
            else
            {
                PreviousScreen();
            }
        }
        else
        {
            // if not fast time, look to which page we got to
            LerpToPage(GetNearestPage());
        }

        _dragging = false;
    }

    public void OnDrag(PointerEventData aEventData)
    {
        if (!_dragging)
        {
            // dragging started
            _dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            _timeStamp = Time.unscaledTime;
            // save current position of cointainer
            _startPosition = _container.anchoredPosition;
        }
    }
}