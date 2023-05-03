using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class MultiplicationPuzzleDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<TMP_Text> solutions = new List<TMP_Text>();
    public List<Transform> solPos = new List<Transform>();
    public UnityEvent onNext;
    bool[] arr = {false, false, false, false, false};
    public UnityEvent onCorrect;
    public UnityEvent onInCorrect;

    //public Transform answerSlot;
    public float desiredDuration = 10.0f;
    float elapsedTime;
    bool isCorrect = false;
    bool isOnStart= false;
    bool isDLO = false;
    bool isDMO = false;
    bool isDRO = false;
    bool isDEO = false;
    bool isDSO = false;
    //public TMP_Text answerText;
    public TMP_Text currentText;
    //public RectTransform answerTextPosition;
    public float dlo;
    public float dmo;
    public float dro;
    public float deo;
    public float dso;
    //public RectTransform currentTextPosition;

    public void Start()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isOnStart = false;
        setStates();
        elapsedTime = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dlo = Vector3.Distance(solutions[0].transform.position, transform.position);
        dmo = Vector3.Distance(solutions[1].transform.position, transform.position);
        dro = Vector3.Distance(solutions[2].transform.position, transform.position);
        deo = Vector3.Distance(solutions[3].transform.position, transform.position);
        dso = Vector3.Distance(solutions[4].transform.position, transform.position);


        if(dlo < 40)
        {
            isCorrect = isCorrectSlot(0);
        }
        else if (dmo < 40)
        {
            isCorrect = isCorrectSlot(1);
        }
        else if (dro < 40)
        {
            isCorrect = isCorrectSlot(2);
        }
        else if (deo < 40)
        {
            isCorrect = isCorrectSlot(3);
        }
        else if (dso < 40)
        {
            isCorrect = isCorrectSlot(4);
        }
        else
        {
            isCorrectSlot(5);
        }

        ////buttonsBeingDragged = null;
        if (isCorrect == false)
        {
            isOnStart = true;
        }
        else
        {
            if (isDLO && (currentText.text == solutions[0].text))
            {
                transform.position = solPos[0].position;
                isOnStart = false;
                setStates();
                arr[0] = true;
                //Debug.Log("Correct");
            }
            else if (isDMO && (currentText.text == solutions[1].text))
            {
                transform.position = solPos[1].position;
                isOnStart = false;
                setStates();
                arr[1] = true;
                //Debug.Log("Correct");
            }
            else if (isDRO && (currentText.text == solutions[2].text))
            {
                transform.position = solPos[2].position;
                isOnStart = false;
                setStates();
                arr[2] = true;
                //Debug.Log("Correct");
            }
            else if (isDEO && (currentText.text == solutions[3].text))
            {
                transform.position = solPos[3].position;
                isOnStart = false;
                setStates();
                arr[3] = true;
                //Debug.Log("Correct");
            }
            else if (isDSO && (currentText.text == solutions[4].text))
            {
                transform.position = solPos[4].position;
                isOnStart = false;
                setStates();
                arr[4] = true;
                //Debug.Log("Correct");
            }
            else
            {
                onInCorrect.Invoke();
                isOnStart = true;
            }

        }

        foreach (bool arrCorrect in arr)
        {
            if (arrCorrect == false)
            {
                break;
            }
            onNext.Invoke();
            setStates();
            Debug.Log("correct");
            isCorrect = false;
            arr[0] = false;
            arr[1] = false;
            arr[2] = false;
            arr[3] = false;
            arr[4] = false;
        }
    }

    public void Update()
    {

        if (isOnStart == true)
        {
            
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, percentageComplete);
        }
    }

    public bool isCorrectSlot(int sol)
    {
        if (sol == 0)
        {
            onCorrect.Invoke();
            isDLO= true;
            return true;
        }
        if (sol == 1)
        {
            onCorrect.Invoke();
            isDMO = true;
            return true;
        }
        if (sol == 2)
        {
            onCorrect.Invoke();
            isDRO = true;
            return true;
        }
        if (sol == 3)
        {
            onCorrect.Invoke();
            isDEO = true;
            return true;
        }
        if (sol == 4)
        {
            onCorrect.Invoke();
            isDSO = true;
            return true;
        }
        GameManager.Instance.AudioManager.PlaySound("WrongAnswer", 0.6f);
        return false;
    }

    void setStates()
    {
        isDLO = false;
        isDMO = false;
        isDRO = false;
        isDEO = false;
        isDSO = false;
    }

    public void setIsOnStart()
    {
        isOnStart = true;
    }
}