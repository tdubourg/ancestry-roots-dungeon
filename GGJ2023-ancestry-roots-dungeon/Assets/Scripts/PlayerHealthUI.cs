using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System;


public class PlayerHealthUI : MonoBehaviour {
    public float healthUIProgressSpeed = 0.2f;
    private float healthUI;

    public Image Healthbar;
    static private PlayerHealthUI instance;

    private AttackEntity attackEntity;

    static public PlayerHealthUI GetInstance() {
        if (null == instance) {
            Debug.Log("PlayerHealthUI.GetInstance() called before init");
        }
        return instance;
    }

    void Awake() {
        if (null != instance) {
            return;
        }
        instance = this;
    }

    void Start() {
        attackEntity = this.gameObject.GetComponent<AttackEntity>();
        healthUI = attackEntity.MAX_HEALTH;
    }


    void Update() {
        if (Math.Abs(healthUI - attackEntity.CurrentHealth) > 0.01) {
            // all the extra variables are when we need debug logs
            var sign = (attackEntity.CurrentHealth - healthUI) / Math.Abs(healthUI - attackEntity.CurrentHealth);
            var healthUIChange = healthUIProgressSpeed * sign;
            var newHealthUI = healthUI + healthUIChange;
            healthUI = Math.Max(newHealthUI, attackEntity.CurrentHealth);
            Healthbar.fillAmount = healthUI / attackEntity.MAX_HEALTH;
        }
    }

}