using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminationSequence : MonoBehaviour
{
    public float terminationTime = 5.0f;

    public void InitiateTerminationSequence()
    {
        Destroy(gameObject, terminationTime);
    }
}
