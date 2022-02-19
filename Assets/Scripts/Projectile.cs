using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hasBeenLaunched = false;
    public TextMeshProUGUI outputText;
    private Vector3 launchPosition = Vector3.zero;
    private TrailRenderer trail = null;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginLaunch() {
        outputText.text = "Flying...";
        hasBeenLaunched = true;
        launchPosition = transform.position;
        trail.emitting = true;
    }

    public void FinishLaunch() {
        var displacement = transform.position.z - launchPosition.z;
        outputText.text = "Ball travelled " + displacement + " units.";
        hasBeenLaunched = false;
        trail.emitting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if(hasBeenLaunched) {
            FinishLaunch();
        }
    }
}
