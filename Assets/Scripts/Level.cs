using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int expirience;
    public int currentLevel;
    public Action OnLevelUp;

    public int MAX_EXP;
    public int MAX_LEVEL = 30;

 public Level(int level, Action OnLevUp)
    {
        MAX_EXP = GetXpForLevel(MAX_LEVEL);
        currentLevel = level;
        expirience = GetXpForLevel(level);
        OnLevelUp = OnLevUp;
        
    }
    public int GetXpForLevel(int level)
    {
        if (level > MAX_LEVEL)
            return  0;

        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
        }
        if (secondPass > MAX_EXP&&MAX_EXP!=0)
            return MAX_EXP;
        if (secondPass < 0)
            return MAX_EXP;
        return secondPass;
        
    }
    public int GetLevelForXP(int exp)
    {
        if (exp > MAX_EXP)
            return MAX_EXP;
        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle < MAX_LEVEL; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
            if (secondPass > exp)
                return levelCycle;
        }
        if (exp > secondPass)
            return MAX_LEVEL;
        return 0;
    }
    public bool AdExp(int amount)
    {
        if (amount + expirience < 0 || expirience > MAX_EXP)
        {
            if (expirience > MAX_EXP)
                expirience = MAX_EXP;
            return false;
        }
        int oldLevel = GetLevelForXP(expirience);
        expirience += amount;
        if (oldLevel < GetLevelForXP(expirience))
        {
            if (currentLevel < GetLevelForXP(expirience))
            {
                currentLevel = GetLevelForXP(expirience);
                if (OnLevelUp != null)
                    OnLevelUp.Invoke();
                return true;
            }
        }
        return false;
    }
}
