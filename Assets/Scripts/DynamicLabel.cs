using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLabel : MonoBehaviour
{
    private string originalLabelName;
    private TextMeshProUGUI textLabel;
    // Start is called before the first frame update
    void Start()
    {
        textLabel = GetComponentInChildren<TextMeshProUGUI>();
        originalLabelName = textLabel.text;
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetAdjustedValue);
        slider.onValueChanged.Invoke(slider.value);
        slider.onValueChanged.AddListener((_) => GameObject.Find("Projectile").GetComponent<Projectile>().Reset());
    }

    public void SetAdjustedValue(float f) {
        textLabel.text = originalLabelName + " = " + (Mathf.Round(f * 100f) / 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
