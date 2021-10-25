using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float Strength, Seconds;
    public Vector3 WindDirection;
    public bool isTimed;
    public GameObject Wind;
    void OnTriggerStay(Collider col)
    {
        Rigidbody colRigidbody = col.GetComponent<Rigidbody>();
        if (colRigidbody != null)
        {
            colRigidbody.AddForce(WindDirection * Strength);
        }
        if(isTimed)
        {
            StartCoroutine(windTimer());
        }

    }

    IEnumerator windTimer()
    {
        Wind.gameObject.SetActive(true);
        yield return new WaitForSeconds(Seconds);
        Wind.gameObject.SetActive(false);
    }
}
