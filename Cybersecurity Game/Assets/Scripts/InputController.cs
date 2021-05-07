using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController instance;

    GameObject computerUI;

    public PauseMenu linkPauseMenu;

    public ComputerUI linkComputerUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            computerUI = GameObject.Find("ComputerUI");
            if (computerUI == null)
            {
                linkPauseMenu.GamePause();
            }
            else
            {
                linkComputerUI.CloseComputer();
            }
        }

        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     NotificationManager.SetNewNotification(new Notification("CEO", "Maybe my computer got infected. Could you take a look?"));
        // }
    }
}
