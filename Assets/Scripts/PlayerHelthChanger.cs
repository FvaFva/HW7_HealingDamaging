using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelthChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _countHelthChange;

    public void Change()
    {
        _player.ChangeHelth(_countHelthChange);
    }
}
