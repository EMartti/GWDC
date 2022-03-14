using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPerks : MonoBehaviour
{
    [SerializeField] private GameObject perkCanvas;

    private PlayerInputActions playerInputActions;

    private PlayerPerks playerPerks;
    private List<PerkButton> perkButtonList;

    [SerializeField] private TMPro.TextMeshProUGUI perkPoints;
    [SerializeField] private Button HP1btn;
    [SerializeField] private Button HP2btn;
    [SerializeField] private Button HP3btn;
    [SerializeField] private Button Dashbtn;


    // Start is called before the first frame update
    void Start()
    {      
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        playerInputActions.Player.OpenPerks.Enable();
        playerInputActions.Player.OpenPerks.started += OnOpenPerks;

        perkCanvas.SetActive(false);
    }

    private void OnOpenPerks(InputAction.CallbackContext obj)
    {
        perkCanvas.SetActive(!perkCanvas.activeSelf);
    }

    private void OnDisable()
    {        
        playerInputActions.Player.OpenPerks.Disable();
    }


    public void SetPlayerPerks(PlayerPerks playerPerks)
    {
        this.playerPerks = playerPerks;

        perkButtonList = new List<PerkButton>();
        perkButtonList.Add(new PerkButton(HP1btn, playerPerks, PlayerPerks.PerkType.MaxHP1));
        perkButtonList.Add(new PerkButton(HP2btn, playerPerks, PlayerPerks.PerkType.MaxHP2));
        perkButtonList.Add(new PerkButton(HP3btn, playerPerks, PlayerPerks.PerkType.MaxHP3));
        perkButtonList.Add(new PerkButton(Dashbtn, playerPerks, PlayerPerks.PerkType.Dash));

        playerPerks.OnPerkUnlocked += PlayerPerks_OnPerkUnlocked;
        playerPerks.OnPerkPointsChanged += PlayerPerks_OnPerkPointsChanged;
        UpdateVisuals();
    }

    private void PlayerPerks_OnPerkUnlocked(object sender, PlayerPerks.OnPerkUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

    private void PlayerPerks_OnPerkPointsChanged(object sender, System.EventArgs e)
    {
        UpdatePerkPoints();
    }

    public void UnlockHP1()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.MaxHP1);
    }

    public void UnlockHP2()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.MaxHP2);
        
    }

    public void UnlockHP3()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.MaxHP3);
    }

    public void UnlockDash()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.Dash);
    }

    private void UpdatePerkPoints()
    {
        perkPoints.SetText(playerPerks.GetPerkPoints().ToString());
    }

    private void UpdateVisuals()
    {
        foreach (PerkButton perkButton in perkButtonList)
        {
            perkButton.UpdateVisual();
        }
        
    }

    private class PerkButton
    {
        private Button button;
        
        private PlayerPerks playerPerks;
        private PlayerPerks.PerkType perkType;

        public PerkButton(Button button, PlayerPerks playerPerks, PlayerPerks.PerkType perkType)
        {
            this.button = button;
            this.playerPerks = playerPerks;
            this.perkType = perkType;      
        }


        public void UpdateVisual()
        {
            Image image = button.image;
            if (playerPerks.isPerkUnlocked(perkType))
            {
                image.color = Color.green;
            }
            else
            {
                if (playerPerks.CanUnlock(perkType))
                {
                    image.color = Color.white;
                }
                else
                {
                    image.color = Color.gray;
                }
            }
        }
    }

}
