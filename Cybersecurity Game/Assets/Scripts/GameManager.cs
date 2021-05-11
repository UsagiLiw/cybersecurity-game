using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager SingletonGameManager; // Singleton Class

    private ReputationManager reputationManager;

    private BudgetManager budgetManager;

    private PasswordManager passwordManager;

    private EmailManager emailManager;

    private ShopManager shopManager;

    public static float dayTime = 60f; //Time for 1 day in game (seconds)

    public static float currentTimer;

    public static int days;

    public static Scenario scenario;

    public static string scenarioDetail;

    public delegate void DayPassHandler();

    public static event DayPassHandler DayPassed;

    void Awake()
    {
        if (SingletonGameManager != null)
        {
            // There is another GameManager already existed
            // Destroy this object
            Destroy(this.gameObject);
            return;
        }

        FindObjectOfType<AudioManager>().Play("bgm_gameplay");
    }

    // Start is called before the first frame update
    void Start()
    {
        SingletonGameManager = this; // I am the singleton
        GameObject.DontDestroyOnLoad(this.gameObject); // Don't kil me

        reputationManager = GetComponent<ReputationManager>();
        budgetManager = GetComponent<BudgetManager>();
        passwordManager = GetComponent<PasswordManager>();
        emailManager = GetComponent<EmailManager>();

        currentTimer = 0;
        days = 0;

        emailManager.SetDictionaries();
        LoadData();
        SceneManager.LoadScene("ITRoom");
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= dayTime)
        {
            days++;
            currentTimer = 0;
            emailManager.SendRandomMail();
            (scenario, scenarioDetail) = ScenarioManager.Instance.CheckStatus();
            SaveData();
            DayPassed?.Invoke();
        }
    }

    public static void SaveData()
    {
        int currentBudget = BudgetManager.currentBudget;
        int currentReputation = ReputationManager.currentReputation;
        (int[] currentInbox, int[] currentSceMails) = EmailManager.ReturnInboxIndex();
        (bool[] readMail, bool[] readSce) = EmailManager.ReturnInboxRead();
        string currentPassword1 = PasswordManager.password1;
        string currentPassword2 = PasswordManager.password2;
        string[] purchaseArr = ShopManager.Instance.SendSaveData();

        SaveObject saveObject =
            new SaveObject {
                day = days,
                budget = currentBudget,
                reputation = currentReputation,
                purchases = purchaseArr,
                password1 = currentPassword1,
                password2 = currentPassword2,
                email = currentInbox,
                scenarioMail = currentSceMails,
                readEmail = readMail,
                readSmail = readSce,
                scenario = scenario,
                scenarioDetail = scenarioDetail
            };
        string json = JsonUtility.ToJson(saveObject, true);
        SaveSystem.Save (json);
    }

    public static void InvokeSaveData()
    {
        int currentBudget = BudgetManager.currentBudget;
        int currentReputation = ReputationManager.currentReputation;
        (int[] currentInbox, int[] currentSceMails) = EmailManager.ReturnInboxIndex();
        (bool[] readMail, bool[] readSce) = EmailManager.ReturnInboxRead();
        string currentPassword1 = PasswordManager.password1;
        string currentPassword2 = PasswordManager.password2;
        scenario = ScenarioManager.onGoingScenario;
        scenarioDetail = ScenarioManager.jsonDetail;
        string[] purchaseArr = ShopManager.Instance.SendSaveData();

        SaveObject saveObject =
            new SaveObject {
                day = days,
                budget = currentBudget,
                reputation = currentReputation,
                purchases = purchaseArr,
                password1 = currentPassword1,
                password2 = currentPassword2,
                email = currentInbox,
                scenarioMail = currentSceMails,
                readEmail = readMail,
                readSmail = readSce,
                scenario = scenario,
                scenarioDetail = scenarioDetail
            };
        string json = JsonUtility.ToJson(saveObject, true);
        SaveSystem.Save (json);
    }

    public void LoadData()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject =
                JsonUtility.FromJson<SaveObject>(saveString);

            SetCurrentDay(saveObject.day);
            budgetManager.SetCurrentBudget(saveObject.budget);
            reputationManager.SetCurrentRep(saveObject.reputation);
            passwordManager
                .SetAllPasswords(saveObject.password1, saveObject.password2);
            emailManager
                .SetPlayerInbox(saveObject.email,
                saveObject.scenarioMail,
                saveObject.readEmail,
                saveObject.readSmail);
            ShopManager.Instance.LoadItemData(saveObject.purchases);
            ScenarioManager
                .Instance
                .SetScenarioState(saveObject.scenario,
                saveObject.scenarioDetail);
        }
        else
        {
            throw new InvalidOperationException("Couldn't load save data, Program terminated");
        }
    }

    private void SetCurrentDay(int day)
    {
        days = day;
    }

    private void SetCurrentTime(float time)
    {
        currentTimer = time;
    }

    public static void BackToMainMenu(bool save)
    {
        if(save) InvokeSaveData();

        var objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach(GameObject o in objects)
        {
            Destroy(o.gameObject);
        }
        SceneManager.LoadScene("Menu");
    }
}
