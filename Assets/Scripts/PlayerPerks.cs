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
        None,
        MaxHP1,
        MaxHP2,
        MaxHP3,
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

    private void UnlockPerk(PerkType perkType)
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


    public bool CanUnlock(PerkType perkType)
    {

        PerkType perkRequirement = GetPerkRequirement(perkType);

        if (perkRequirement != PerkType.None)
        {
            if (isPerkUnlocked(perkRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public PerkType GetPerkRequirement(PerkType perkType)
    {
        switch (perkType)
        {
            case PerkType.MaxHP2: return PerkType.MaxHP1;

            case PerkType.MaxHP3: return PerkType.MaxHP2;
        }
        return PerkType.None;
    }

    public bool TryUnlockPerk(PerkType perkType)
    {
        if (CanUnlock(perkType))
        {
            UnlockPerk(perkType);
            return true;
        }
        else
        {
            return false;
        }
    }
}
