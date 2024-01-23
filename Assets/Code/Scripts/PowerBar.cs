using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerBar : MonoBehaviour
{
    [field: SerializeField] public int maxValue { set; private get; }

    [field: SerializeField] public int value { set; private get; }

    [SerializeField] private RectTransform _topBar;
    [SerializeField] private RectTransform _bottomBar;

    private float _fullWidth;
    private float TargetWidth => value * _fullWidth / maxValue;

    private void Start()
    {
        _fullWidth = _topBar.rect.width;
    }


    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Change(20);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Change(-20);
        }
    }

    //private IEnumerator AdjustBarWidth(int amount)
    //{
    //    var suddenChargeBar = amount >= 0 ? _bottomBar : _topBar;
    //    var slowChangeBar = amount >= 0 ? _topBar : _bottomBar;
    //    suddenChargeBar.SetWidth(TargetWidth);

    //}

    public void Change(int amount)
    {
        value = Mathf.Clamp(value + amount, 0, maxValue);
    }
}
