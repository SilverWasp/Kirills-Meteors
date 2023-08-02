using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class PlayerSavedData
{
    public int score;
    public int lives;
    public string stage;

    public PlayerSavedData() 
    { 
        lives = 5;
        score = 0;
        stage = "Stage 1";
    }

    public PlayerSavedData(int score, int lives, string stage)
    {
        this.score = score;
        this.lives = lives;
        this.stage = stage;
    }
}
