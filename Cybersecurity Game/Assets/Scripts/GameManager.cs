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

    public static float dayTime = 10f; //Time for 1 day in game (seconds)

    public static float currentTimer;

    public static int days;

    public static Scenario scenario;

    public static string scenarioDetail;

    public delegate void DayPassHandler();

    public event DayPassHandler DayPassed;

    void Awake()
    {
        SaveSystem.Init();
        if (SingletonGameManager != null)
        {
            // There is another GameManager already existed
            // Destroy this object
            Destroy(this.gameObject);
            return;
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= dayTime)
        {
            days++;

            // Debug.Log(days + " days have passed");
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
        int[] currentInbox = EmailManager.indexInbox.ToArray();
        int[] currentSceMails = EmailManager.scenarioInbox.ToArray();
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
        int[] currentInbox = EmailManager.indexInbox.ToArray();
        int[] currentSceMails = EmailManager.scenarioInbox.ToArray();
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
                .SetPlayerInbox(saveObject.email, saveObject.scenarioMail);
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
}
