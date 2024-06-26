using System;
using UnityEngine.InputSystem.LowLevel;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    private List<PointInTime> PT;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        PT = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startRewinding();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            stopRewinding();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

    }
    void Rewind()
    {
        if (PT.Count > 0)
        {
            PointInTime p = PT[0];
            transform.position = p.position;
            transform.rotation = p.rotation;
            PT.RemoveAt(0);
        }
        else
        {
            stopRewinding();
        }
    }
    void Record()
    {
        if (PT.Count > Mathf.Round(5f/Time.fixedDeltaTime))
        {
            PT.RemoveAt(PT.Count-1);

        }
        PT.Insert(0,new PointInTime(transform.position,transform.rotation));
        
    }
    void startRewinding()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }
    
    void stopRewinding()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
