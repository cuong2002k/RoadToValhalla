using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer
{
    float baseTimer = 0;
    float timer = 0f;
    public float Timer => this.timer;
    public CountDownTimer(float timer)
    {
        this.baseTimer = timer;
        this.timer = this.baseTimer;
    }
    public void Tick(float deltaTime)
    {
        if (timer > 0)
        {
            timer -= deltaTime;
        }
    }

    public void Reset()
    {
        this.timer = this.baseTimer;
    }
}
