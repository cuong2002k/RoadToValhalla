using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer
{
    float timer = 0f;
    public float Timer => this.timer;
    public CountDownTimer(float timer)
    {
        this.timer = timer;
    }
    public void Tick(float deltaTime)
    {
        if (timer > 0)
        {
            timer -= deltaTime;
        }
    }
}
