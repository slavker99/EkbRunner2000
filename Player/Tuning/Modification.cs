using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modification : TuningObject
{
    [SerializeField] private int price = 0;
    [SerializeField] private bool isDefaultMod = false;
    [SerializeField] private bool needRecolor = false; // ����� �� ������������� ��� ����� �����
    protected PlayerTuningView tuningView;
    public bool isMain { get; private set; } // ���� true �� ����� ���������� �� ������ ��� ����
    public bool isPurchased { get; private set; } // ���������� ��� ���

    public int GetPrice() => price;
    public bool GetIsDefaultMod() => isDefaultMod;
    public bool GetNeedRecolor() => needRecolor;


    public void SetTuningView(PlayerTuningView view) => tuningView = view;

    private void Awake()
    {
        if (isDefaultMod)
            isPurchased = true;
            
    }

    public void SetIsMain(bool value)
    {
        isMain = value;
    }

    public void SetIsPurchased(bool value)
    {
        isPurchased = value;
    }
    public abstract void Recolor(Material material);
}

