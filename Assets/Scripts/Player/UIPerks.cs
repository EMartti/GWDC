using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPerks : MonoBehaviour
{
    [SerializeField] private GameObject perkCanvas;

    private PlayerInputActions playerInputActions;
    private GameManager gameManager;

    private PlayerPerks playerPerks;
    private List<PerkButton> perkButtonList;

    [SerializeField] private TMPro.TextMeshProUGUI perkPoints;
    [SerializeField] private Button HP1btn;
    [SerializeField] private Button HP2btn;
    [SerializeField] private Button HP3btn;
    [SerializeField] private Button Dashbtn;

    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteHighlight;
    [SerializeField] private Sprite spriteLocked;



    #region Singleton
    public static UIPerks Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(UIPerks)) as UIPerks;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static UIPerks instance;
    #endregion

    // Start is called before the first frame update
    void Start()
    {      
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        gameManager = GameManager.Instance;

        playerInputActions.Player.OpenPerks.Enable();
        playerInputActions.Player.OpenPerks.started += OnOpenPerks;

        perkCanvas.SetActive(false);
    }

    private void OnOpenPerks(InputAction.CallbackContext obj)
    {
        perkCanvas.SetActive(!perkCanvas.activeSelf);

        if(perkCanvas.activeInHierarchy)        
            playerInputActions.Player.Fire.Disable();
        else
            playerInputActions.Player.Fire.Enable();
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


    // Functions for PerkButton OnClick Events -->
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
    // Functions for PerkButton OnClick Events <--

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


    // Perk Button class
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


        // Perk button visual change after unlock
        public void UpdateVisual()
        {
            Image image = button.image;

            // Perk unlockattu - vaihda väri vihreäksi
            if (playerPerks.isPerkUnlocked(perkType))
            {
                image.sprite = UIPerks.Instance.spriteActive;
                
                // Disable sprite changes after unlocking perk
                button.interactable = false;
            }
            else
            {
                // Perkin voi unlockata - väri valkoinen
                if (playerPerks.CanUnlock(perkType))
                {
                    image.sprite = UIPerks.Instance.spriteHighlight;
                }
                // Perkkiä ei voi vielä unlockata - Väri harmaa
                else
                {
                    image.sprite = UIPerks.Instance.spriteLocked;
                }
            }
        }
    }

}
