using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLaunch : MonoBehaviour
{
    public Rigidbody sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLaunchClick() {
        var force = 1000;
        var angle = Mathf.PI / 4;
        sphere.GetComponent<Projectile>().BeginLaunch();
        sphere.AddForce(new Vector3(0, force*Mathf.Sin(angle), force*Mathf.Cos(angle)), ForceMode.Force);
    }
}
