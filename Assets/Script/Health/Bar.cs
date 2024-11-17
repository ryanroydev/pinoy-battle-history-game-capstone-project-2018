using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
	
    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColor;

    private float fillamount;

    public float MaxValue { get; set; }

  public float Value {
        set
        {
            string[] tmp = valueText.text.Split(':');
			valueText.text = tmp [0] + ": " + value + "/" + MaxValue.ToString ();
			if(MaxValue!=0)
				fillamount = map(value, MaxValue, 0, 1, 0);
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (lerpColor)
        {
            content.color = fullColor;
        }
        
	
	}
	
	// Update is called once per frame
	void Update () {
        handleBar();
	}
    private void handleBar()
    {
        if (fillamount != content.fillAmount)
        {
			
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillamount, Time.deltaTime * lerpSpeed);
        }
        if (lerpColor)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillamount);
        }
        
    }

    private float map(float value, float inMax,float inMin, float outMax, float outMin)
    {
        return (value + inMin) * (outMax - outMin) / (inMax - inMin) - inMin;
    }
}
