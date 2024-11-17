using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat {
    [SerializeField]
    private Bar Healthbar;

    [SerializeField]
    private float currentValue;

    [SerializeField]
    private float maxValue;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            this.currentValue = Mathf.Clamp(value, 0, MaxValue);
            Healthbar.Value = currentValue;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            this.maxValue = value;
            Healthbar.MaxValue = maxValue;
        }
    }
    public void Initialize()
    {
        this.CurrentValue = currentValue;

        this.MaxValue = maxValue;
    }
}
