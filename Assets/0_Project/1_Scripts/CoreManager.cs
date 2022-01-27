using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NaughtyAttributes;

public class CoreManager : MonoBehaviour
{
    public static CoreManager instance;

    [Header("UI")]
    public Text policeText;
    public Text folkText;
    public Text gameStateText;

    // Skill
    public Text skillNumText;
    public Image cooldownImg;

    [Header("Game State")]
    [Multiline]
    public string winText;
    [Multiline]
    public string lossText;

    [Header("Events")]
    public UnityEvent gameOverEvents;

    public static float playTime;

    // private variable
    int police => TowniesManager.instance.Polices.Count;
    int folk => TowniesManager.instance.currentFolkCount;
    int potionCount => PlayerCore.instance.potionNum;
    float potionTotalCooldownTime => PlayerCore.instance.PotionTimer;
    float potionRemainingCooldownTime => PlayerCore.instance.Timer;

    // local variable
    float initialTimeScale;
    IEnumerator cooldownCoroutine;

    // method
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        playTime = 0f;
        initialTimeScale = Time.timeScale;
    }

    void Start()
    {
        policeText.text = police.ToString();
        folkText.text = folk.ToString();
        skillNumText.text = potionCount.ToString();
    }

    void Update()
    {
        CoreManager.playTime += Time.deltaTime;
    }

    [Button]
    public void Resume()
    {
        Time.timeScale = initialTimeScale;
    }

    [Button]
    public void Pause()
    {
        Time.timeScale = 0;
    }

    //#region Skill
    [Button]
    public void DrinkPotion()
    {
        skillNumText.text = potionCount.ToString();
        cooldownCoroutine = PotionCooldown();
        StartCoroutine(cooldownCoroutine);
    }

    // Might migrate to actual skill handling script
    IEnumerator PotionCooldown()
    {
        cooldownImg.gameObject.SetActive(true);

        while (potionRemainingCooldownTime > 0f)
        {
            cooldownImg.fillAmount = potionRemainingCooldownTime / potionTotalCooldownTime;

            yield return new WaitForEndOfFrame();
        }

        cooldownImg.gameObject.SetActive(false);
    }
    //#endregion

    [Button]
    public void Kill()
    {
        folkText.text = folk.ToString();
        policeText.text = police.ToString();
        if (folk <= 0)
        {
            GameOver();
        }
    }

    [Button]
    public void GameOver()
    {
        Pause();
        gameOverEvents.Invoke();

        if (folk > 0)
        {
            Loss();
        }
        else
        {
            Win();
        }


        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
    }

    void Win()
    {
        gameStateText.text = winText;
    }

    void Loss()
    {
        gameStateText.text = lossText;
    }
}
