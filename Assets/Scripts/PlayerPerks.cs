using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPerks
{
    public event EventHandler<OnPerkUnlockedEventArgs> OnPerkUnlocked;
    public class OnPerkUnlockedEventArgs : EventArgs
    {
        public PerkType perkType;
    }

    public enum PerkType
    {
        MaxHP1,
        MaxHP2,
        HPLeech,
        Attack1,
        Move1,
        Move2
    }

    private List<PerkType> unlockedPerkTypeList;

    public PlayerPerks()
    {
        unlockedPerkTypeList = new List<PerkType>();
    }

    public void UnlockPerk(PerkType perkType)
    {
        if (!isPerkUnlocked(perkType))
        {
            unlockedPerkTypeList.Add(perkType);
            OnPerkUnlocked?.Invoke(this, new OnPerkUnlockedEventArgs { perkType = perkType });
        }
    }

    public bool isPerkUnlocked(PerkType perkType)
    {
        return unlockedPerkTypeList.Contains(perkType);
    }
}
