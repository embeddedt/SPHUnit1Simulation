using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public bool hasBeenLaunched = false;
    public TextMeshProUGUI outputText;
    public Button launchButton;
    private Vector3 launchPosition = Vector3.zero;
    private TrailRenderer trail = null;
    private Vector3 initialPosition;
    private float _launchForce = 1400;
    private GameObject launchArrow;
    private float launchTime;
    private float peakHeight;
    public float launchForce {
        get {
            return _launchForce;
        }
        set {
            _launchForce = value;
        }
    }

    private float _launchAngle = 45;
    public float launchAngle {
        get {
            return _launchAngle;
        }
        set {
            _launchAngle = value;
            launchArrow.transform.rotation = Quaternion.Euler(90 - _launchAngle, 0, 0);
        }
    }

    void Awake() {
        launchArrow = transform.Find("LaunchArrow").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        initialPosition = transform.position;
        Reset();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        peakHeight = Mathf.Max(peakHeight, transform.position.y - launchPosition.y);
    }

    IEnumerator TimeoutLaunch() {
        yield return new WaitForSeconds(7f);
        Reset();
        outputText.text = "Ball never landed (too much force?).";
    }

    public void Launch() {
        var angle = _launchAngle * Mathf.PI / 180f;
        launchButton.interactable = false;
        Rigidbody sphere = GetComponent<Rigidbody>();
        outputText.text = "Launched!";
        hasBeenLaunched = true;
        launchTime = Time.time;
        launchPosition = transform.position;
        peakHeight = 0;
        trail.emitting = true;
        sphere.AddForce(new Vector3(0, _launchForce*Mathf.Sin(angle), _launchForce*Mathf.Cos(angle)), ForceMode.Force);
        launchArrow.SetActive(false);
        StartCoroutine("TimeoutLaunch");
    }

    void FreezeProjectile() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    static float RoundToTwoDecimals(float f) {
        return Mathf.Round(f * 100f) / 100f;
    }

    public void FinishLaunch() {
        FreezeProjectile();
        StopCoroutine("TimeoutLaunch");
        var displacement = transform.position.z - launchPosition.z;
        var timeInAir = Time.time - launchTime;
        if(displacement > 0)
            outputText.text = "Ball travelled "
                + RoundToTwoDecimals(displacement)
                + " m horizontally for "
                + RoundToTwoDecimals(timeInAir)
                + " seconds. Peak height was "
                + RoundToTwoDecimals(peakHeight)
                + " m.";
        hasBeenLaunched = false;
        trail.emitting = false;
    }

    public void Reset() {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        FinishLaunch();
        trail.Clear();
        launchButton.interactable = true;
        launchArrow.SetActive(true);
        outputText.text = "Press Launch to begin.";
    }

    void OnCollisionEnter(Collision collision)
    {
        if(hasBeenLaunched) {
            FinishLaunch();
        } else {
            FreezeProjectile();
            initialPosition = transform.position;
        }
    }
}
