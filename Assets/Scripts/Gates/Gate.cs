using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool IsOpen = false;

    [SerializeField]
    private bool IsRotatingGate = true;

    [SerializeField]
    private AudioSource GateSound;

    [SerializeField]
    private float Speed = 1f;

    [Header("Rotation Configs")]
    [SerializeField]
    private float RotationAmount = 90f;

    [SerializeField]
    private float ForwardDirection = 0;

    [SerializeField]
    private Transform anchorPoint;

    private Vector3 StartRotation;
    private Vector3 Forward;
    private Coroutine AnimationCoroutine;

    private GameObject currentSigil;

    void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;

        Forward = transform.right;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSigil = GameObject.FindGameObjectWithTag("Sigil");
        currentSigil.transform.position = anchorPoint.position;
        currentSigil.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if(IsRotatingGate)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }  
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;
        GateSound.Play();
        if (ForwardAmount >= ForwardDirection) 
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
        else 
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }

        IsOpen = true;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        GateSound.Stop();

    }

    public void Close()
    {
        if(IsOpen)
        {
             if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if(IsRotatingGate)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);
        GateSound.Play();
        IsOpen = false;

        float time = 0;
        while ( time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        GateSound.Stop();
    }
}
