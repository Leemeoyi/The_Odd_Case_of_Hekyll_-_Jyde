using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CoreStateManager))]
public class CoreManager : MonoBehaviour {
    public static CoreManager instance;
    
    [Header("UI")]
    public Text policeText;
    public Text folkText;

    // Skill
    public Slider skillSlider;
    public Image cooldownImg;
    public Text cooldownText;

    [Header("Events")]
    public UnityEvent gameOverEvents;

    public static float playTime;

    // private variable
    CoreStateManager state;
    int police  {
        get { return state.police; }
        set {
            state.police = value;
            policeText.text = value.ToString();
        }
    }
    int folk {
        get { return state.folk; }
        set {
            state.folk = value;
            folkText.text = value.ToString();
        }
    }
    int skillCount {
        get { return state.skillCount; }
        set {
            state.skillCount = value;
            skillSlider.value = value;
        }
    }
    float skillCountdown = 0f;
    IEnumerator cooldownCoroutine;

    bool isStop = false;

    // method
    void Awake() {
        instance = CoreManager();
        state = GetComponent<CoreStateManager>();
        playTime = 0f;
    }

    void Start() {
        policeText.text = police.ToString();
        folkText.text = folk.ToString();
        skillSlider.maxValue = skillCount;
        skillSlider.value = skillCount;
    }

    //#region Skill
    [ContextMenu("Runtime Only/Trigger Skill")]
    public void triggerSkill() {
        if (isStop) {
            return;
        }

        if (skillCount > 0) {
            if (skillCountdown <= 0f) {
                skillCount -= 1;

                cooldownCoroutine = CooldownSkill();
                StartCoroutine(cooldownCoroutine);
            }
        }
    }

    // Might migrate to actual skill handling script
    IEnumerator CooldownSkill() {
        skillSlider.gameObject.SetActive(false);
        cooldownImg.gameObject.SetActive(true);

        skillCountdown = state.skillTime;
        while (skillCountdown > 0f) {
            skillCountdown -= 1.0f;

            cooldownImg.fillAmount = skillCountdown / state.skillTime;;
            cooldownText.text = Mathf.Abs(skillCountdown).ToString();

            yield return new WaitForSeconds(1.0f);
        }
        
        skillSlider.gameObject.SetActive(true);
        cooldownImg.gameObject.SetActive(false);
    }
    //#endregion

    [ContextMenu("Runtime Only/Kill")]
    public void kill() {
        if (isStop) {
            return;
        }

        folk -= 1;
        police += 1;

        if (folk <= 0) {
            gameOver();
        }
    }

    [ContextMenu("Runtime Only/Game Over")]
    public void gameOver() {
        if (isStop) {
            return;
        }

        isStop = true;
        gameOverEvents.Invoke();
        if (cooldownCoroutine != null) {
            StopCoroutine(cooldownCoroutine);
        }
    }
}
