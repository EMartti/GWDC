using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIPerks : MonoBehaviour
{
    [Header("UI Elements & Menus")]
    [SerializeField] private GameObject perkCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject debugCanvas;
    [SerializeField] private TextMeshProUGUI levelText;


    private PlayerInputActions playerInputActions;
    private GameManager gameManager;
    private weaponType currentWeapon;

    private PlayerPerks playerPerks;
    private List<PerkButton> perkButtonList;

    [SerializeField] private TMPro.TextMeshProUGUI perkPoints;

    [Header("Perk Buttons")]
    [SerializeField] private Button HP1Btn;
    [SerializeField] private Button HP2Btn;
    [SerializeField] private Button HP3Btn;
    [SerializeField] private Button DashBtn;
    [SerializeField] private Button DashDrBtn;
    [SerializeField] private Button DashRedCool;

    [Header("Button Sprites")]
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteHighlight;
    [SerializeField] private Sprite spriteLocked;

    [SerializeField] private GameObject spriteMelee;
    [SerializeField] private GameObject spriteRange;
    [SerializeField] private GameObject spriteMagic;
    [SerializeField] private GameObject player;


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


    void Start()
    {      
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        gameManager = GameManager.Instance;
        player = GameObject.Find("Player");

        //--------------------Input----------------------------------------
        playerInputActions.Player.OpenPerks.Enable();
        playerInputActions.Player.OpenPerks.started += OnOpenPerks;
        playerInputActions.Player.PauseGame.Enable();
        playerInputActions.Player.PauseGame.started += OnPauseGame;
        playerInputActions.Player.DebugMenu.Enable();
        playerInputActions.Player.DebugMenu.started += OnOpenDebugMenu;
        //--------------------Input----------------------------------------

        perkCanvas.SetActive(false);
        pauseCanvas.SetActive(false);

        spriteMelee.SetActive(false);
        spriteMagic.SetActive(false);
        spriteRange.SetActive(false);

        SetCurrentWeapon();
        UpdateWeaponSprite(currentWeapon);
    }

    private void OnDisable()
    {        
        playerInputActions.Player.OpenPerks.Disable();
    }

    #region UIMenus
    private void OnOpenDebugMenu(InputAction.CallbackContext obj)
    {
        switch(debugCanvas.activeInHierarchy)
        {
            case false:
                debugCanvas.SetActive(true);
                break;
            default:
                debugCanvas.SetActive(false);
                break;
        }
    }

    private void OnOpenPerks(InputAction.CallbackContext obj)
    {
        perkCanvas.SetActive(!perkCanvas.activeSelf);

        if(perkCanvas.activeInHierarchy)        
            playerInputActions.Player.Fire.Disable();
        else
            playerInputActions.Player.Fire.Enable();
    }

    private void OnPauseGame(InputAction.CallbackContext obj)
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);      
        if (pauseCanvas.activeInHierarchy)
        {
            playerInputActions.Player.Fire.Disable();
            Time.timeScale = 0;
        }
        else
        {
            playerInputActions.Player.Fire.Enable();
            Time.timeScale = 1;
        }
            
    }

    // Update player Meta-level display
    public void UpdateMetaLevelText()
    {
        levelText.text = PlayerStats.Instance.playerLevel.ToString();
    }

    // Detectaa ja settaa pelaajan aseen ase-GameObjektien avulla
    public void SetCurrentWeapon()
    {
        if (player.GetComponent<Melee>())
        {
            currentWeapon = weaponType.Melee;
        }
        if (player.GetComponent<Range>())
        {
            currentWeapon = weaponType.Range;
        }
        if (player.GetComponent<Magic>())
        {
            currentWeapon = weaponType.Magic;
        }
        else
        {
            currentWeapon = weaponType.None;
        }
    }

    public void UpdateWeaponSprite(weaponType weapon)
    {
        switch (weapon)
        {
            case weaponType.Melee:
                spriteMelee.SetActive(true);
                spriteMagic.SetActive(false);
                spriteRange.SetActive(false);
                break;

            case weaponType.Magic:
                spriteMelee.SetActive(false);
                spriteMagic.SetActive(true);
                spriteRange.SetActive(false);
                break;

            case weaponType.Range:
                spriteMelee.SetActive(false);
                spriteMagic.SetActive(false);
                spriteRange.SetActive(true);
                break;

            default:
                spriteMelee.SetActive(false);
                spriteMagic.SetActive(false);
                spriteRange.SetActive(false);
                break;
        }
    }
    #endregion

    #region UIPerks
    public void SetPlayerPerks(PlayerPerks playerPerks)
    {
        this.playerPerks = playerPerks;

        perkButtonList = new List<PerkButton>();
        perkButtonList.Add(new PerkButton(HP1Btn, playerPerks, PlayerPerks.PerkType.MaxHP1));
        perkButtonList.Add(new PerkButton(HP2Btn, playerPerks, PlayerPerks.PerkType.MaxHP2));
        perkButtonList.Add(new PerkButton(HP3Btn, playerPerks, PlayerPerks.PerkType.MaxHP3));
        perkButtonList.Add(new PerkButton(DashBtn, playerPerks, PlayerPerks.PerkType.Dash));
        perkButtonList.Add(new PerkButton(DashDrBtn, playerPerks, PlayerPerks.PerkType.DashDr));
        perkButtonList.Add(new PerkButton(DashRedCool, playerPerks, PlayerPerks.PerkType.DashRedCool));

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
    public void UnlockDashDr()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.DashDr);
    }
    public void UnlockDashRedCool()
    {
        playerPerks.TryUnlockPerk(PlayerPerks.PerkType.DashRedCool);
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

    #endregion

}
