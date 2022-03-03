using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPerks : MonoBehaviour
{
    [SerializeField] private GameObject perkCanvas;

    private PlayerInputActions playerInputActions;

    private PlayerPerks playerPerks;

    // Start is called before the first frame update
    void Start()
    {
        perkCanvas.SetActive(false);

        playerInputActions = PlayerInputs.Instance.playerInputActions;

        playerInputActions.Player.OpenPerks.Enable();
        playerInputActions.Player.OpenPerks.started += OnOpenPerks;
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
    }

    public void UnlockHP1()
    {
        playerPerks.UnlockPerk(PlayerPerks.PerkType.MaxHP1);
    }

}
