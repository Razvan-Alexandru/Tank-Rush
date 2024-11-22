using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOverlay : MonoBehaviour
{

    [System.Serializable]
    public class WeaponUI {
        public Image weaponImage; 
        public Image cooldownFill;
        public Image selectedTint;
        public Text text;
    }

    public WeaponUI cannonUI;
    public WeaponUI missileUI;
    public WeaponUI machineGunUI;
    
    private int currentWeaponIndex = 0;


    void Start() {
        UpdateWeaponUI();
    }
 
    void Update() {
        // if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        // if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        // if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
    }

    public void SelectWeapon(int weaponIndex)
    {
        currentWeaponIndex = weaponIndex;
        UpdateWeaponUI();

    }

    void UpdateWeaponUI() {
        cannonUI.weaponImage.color = Color.white;
        missileUI.weaponImage.color = Color.white;
        machineGunUI.weaponImage.color = Color.white;

        cannonUI.text.color = Color.black;
        missileUI.text.color = Color.black;
        machineGunUI.text.color = Color.black;

        cannonUI.selectedTint.enabled = false;
        missileUI.selectedTint.enabled = false;
        machineGunUI.selectedTint.enabled = false;

        switch (currentWeaponIndex)
        {
            case 0:
                cannonUI.weaponImage.color = Color.yellow;
                cannonUI.text.color = Color.red;
                cannonUI.selectedTint.enabled = true;
                break;
            case 1:
                missileUI.weaponImage.color = Color.yellow;
                missileUI.text.color = Color.red;
                missileUI.selectedTint.enabled = true;
                break;
            case 2:
                machineGunUI.weaponImage.color = Color.yellow;
                machineGunUI.text.color = Color.red;
                machineGunUI.selectedTint.enabled = true;
                break;
        }
    }

    public void Fill(float time, int weaponIndex) {
        float timer = 0;
        while(timer <= time) {
            timer += Time.deltaTime;
            switch (weaponIndex)
            {
                case 0:
                    cannonUI.cooldownFill.fillAmount = timer/time;
                    break;
                case 1:
                    missileUI.cooldownFill.fillAmount = timer/time;
                    break;
                case 2:
                    machineGunUI.cooldownFill.fillAmount = timer/time;
                    break;
            }
        }
    }

    public IEnumerator UnFill(float time, int weaponIndex) {
        float timer = time;
        while(timer >= 0) {
            timer -= Time.deltaTime;
            switch (weaponIndex)
            {
                case 0:
                    cannonUI.cooldownFill.fillAmount = timer/time;
                    break;
                case 1:
                    missileUI.cooldownFill.fillAmount = timer/time;
                    break;
                case 2:
                    machineGunUI.cooldownFill.fillAmount = timer/time;
                    break;
            }
            yield return 0;
        }
    }

    public void UpdateCooldown(float cooldownProgress, int weaponIndex)
    {
        // Update the fill amount for the currently selected weapon based on cooldown progress (0 to 1)
        switch (weaponIndex)
        {
            case 0:
                cannonUI.cooldownFill.fillAmount = cooldownProgress;
                break;
            case 1:
                missileUI.cooldownFill.fillAmount = cooldownProgress;
                break;
            case 2:
                machineGunUI.cooldownFill.fillAmount = cooldownProgress;
                break;
        }
    }
}
