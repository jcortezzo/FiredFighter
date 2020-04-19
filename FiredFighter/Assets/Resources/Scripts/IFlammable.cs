using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlammable
{
    void Burn();

    int SmokeNumber();
    int FlameIncreaseNumber();
}
