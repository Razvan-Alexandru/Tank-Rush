using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon currentWeapon;

    public Canvas overlay;
    private WeaponOverlay weaponOverlay;

    public GameObject launchParticles;
    public GameObject hitParticles;

    private float weaponSwitchDelay = 1f;
    private bool isSwitchingWeapon = false;

    private int currentWeaponIndex = 0;
    private bool isFiring = false;
    private float machineGunOverheat = 0f;
    private float overHeatDelay = 1f;
    private float overheatTimer = 0f;
    private bool isOVerheated = false;
    void Start() {
        currentWeapon = weapons[currentWeaponIndex];
        weaponOverlay = overlay.GetComponent<WeaponOverlay>();
    }

    void Update() {
        HandleSwitchingWeapon();
        HandleFiring();
        HandleMachineGunCooldown();
        //UpdateCooldownUI();
    }

    private void HandleSwitchingWeapon() {
        if(Input.GetKeyDown(KeyCode.Tab) && !isSwitchingWeapon) {
            StartCoroutine(SwitchWeapon((currentWeaponIndex + 1) % weapons.Length));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponIndex != 0) {
            StartCoroutine(SwitchWeapon(0));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponIndex != 1) {
            StartCoroutine(SwitchWeapon(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeaponIndex != 2) {
            StartCoroutine(SwitchWeapon(2));
        }
    }

    public IEnumerator SwitchWeapon(int newWeaponIndex) {
        //if(isSwitchingWeapon) yield break;
        isSwitchingWeapon = true;

        currentWeaponIndex = newWeaponIndex;
        currentWeapon = weapons[currentWeaponIndex];

        overlay.GetComponent<WeaponOverlay>().SelectWeapon(newWeaponIndex);

        yield return new WaitForSeconds(weaponSwitchDelay);
        isSwitchingWeapon = false;
    }

    private void HandleFiring() {
        if(!isSwitchingWeapon && Input.GetButtonDown("Fire1")) {
            if(currentWeapon.weaponType == Weapon.WeaponType.Cannon && !currentWeapon.isCoolingDown) {
                StartCoroutine(FireCannon());
            }
            else if(currentWeapon.weaponType == Weapon.WeaponType.Missile && !currentWeapon.isCoolingDown) {
                StartCoroutine(FireMissiles());
            }
            else if(currentWeapon.weaponType == Weapon.WeaponType.MachineGun && !currentWeapon.isCoolingDown) {
                FireMachineGun();
            }
        }
    }

    

    // IEnumerator FireWeapon()
    // {
    //     switch (currentWeapon.weaponType)
    //     {
    //         case Weapon.WeaponType.Cannon:
    //             yield return FireCannon();
    //             break;
            
    //         case Weapon.WeaponType.Missile:
    //             yield return FireMissiles();
    //             break;
            
    //         case Weapon.WeaponType.MachineGun:
    //             //yield return FireMachineGun();
    //             break;
    //     }
    // }
    IEnumerator FireCannon() {
        
        currentWeapon.isCoolingDown = true;
        InstantiateProjectile(currentWeapon.bullet, currentWeapon.firePoint.position, currentWeapon.firePoint.forward);
        Camera.main.GetComponent<CameraController>().TriggerShake();

        yield return new WaitForSeconds(currentWeapon.cooldownTime);

        currentWeapon.isCoolingDown = false;


        // currentWeapon.isCoolingDown = true;
        // InstantiateProjectile(currentWeapon.bullet, currentWeapon.firePoint.position, currentWeapon.firePoint.forward, false);
        
        // Camera.main.GetComponent<CameraController>().TriggerShake();

        // float elapsedTime = 0f;


        // while(elapsedTime < currentWeapon.cooldownTime) {
        //     elapsedTime += Time.deltaTime;
        //     float cooldownProgress = 1f - (elapsedTime / currentWeapon.cooldownTime);
        //     weaponOverlay.UpdateCooldown(cooldownProgress, 0);

        //     yield return null;
        // }
        // currentWeapon.isCoolingDown = false;
        // weaponOverlay.UpdateCooldown(0f, 0);
    }

    IEnumerator FireMissiles() {
        
        currentWeapon.isCoolingDown = true;


        for(int rounds = 0; rounds < currentWeapon.roundsPerBurst; rounds++) {
            InstantiateProjectile(currentWeapon.bullet, currentWeapon.firePoint.position, currentWeapon.firePoint.forward, true);
            Camera.main.GetComponent<CameraController>().TriggerShake();
            yield return new WaitForSeconds(currentWeapon.burstDelay);
        }

        yield return new WaitForSeconds(currentWeapon.cooldownTime);

        currentWeapon.isCoolingDown = false;

        // float elapsedTime = 0f;
        // while (elapsedTime < currentWeapon.cooldownTime) {
        //     elapsedTime += Time.deltaTime;
        //     float cooldownProgress = 1f - (elapsedTime / currentWeapon.cooldownTime);
        //     weaponOverlay.UpdateCooldown(cooldownProgress, 1);

        //     yield return null;
        // }
        // currentWeapon.isCoolingDown = false;
        // weaponOverlay.UpdateCooldown(0f, 1);
    }

    void FireMachineGun() {

        if(!isFiring) {
            isFiring = true;
            StartCoroutine(MachineGunFire());
        }
        
        
        
        // currentWeapon.isCoolingDown = false;

        // bool blockWeapon = false;
        
        // while (!currentWeapon.isCoolingDown) {
            
        //     while(Input.GetButton("Fire1")) {
        //         if(blockWeapon) break;

        //         InstantiateProjectile(currentWeapon.bullet, currentWeapon.firePoint.position, currentWeapon.firePoint.forward, false, 0.5f);
        //         currentWeapon.shotCounter++;

        //         float overheatProgress = (float)currentWeapon.shotCounter / currentWeapon.maxRoundsBeforeOverheat;
        //         weaponOverlay.UpdateCooldown(overheatProgress, 2);

        //         if (currentWeapon.shotCounter >= currentWeapon.maxRoundsBeforeOverheat) {

        //             // currentWeapon.isCoolingDown = true;
        //             //yield return new WaitForSeconds(currentWeapon.cooldownTime);
        //             currentWeapon.shotCounter = currentWeapon.maxRoundsBeforeOverheat;
        //             blockWeapon = true;
                    
        //             break;
        //             //currentWeapon.isCoolingDown = false;
                    
        //         }
        //         yield return new WaitForSeconds(60f / currentWeapon.firingRate);
        //     }
            
        //     if(blockWeapon) {
        //         currentWeapon.shotCounter -= Mathf.CeilToInt(Time.deltaTime);

        //         if(currentWeapon.shotCounter <= 0) {
        //             weaponOverlay.UpdateCooldown(0f, 2);
        //             blockWeapon = false;
        //             break;
        //         }

        //         yield return unfill();
        //     }

        //     else if(currentWeapon.shotCounter < currentWeapon.maxRoundsBeforeOverheat) {
        //         currentWeapon.shotCounter -= Mathf.CeilToInt(Time.deltaTime);

        //         if(currentWeapon.shotCounter <= 0) {
        //             weaponOverlay.UpdateCooldown(0f, 2);
        //             break;
        //         }
        //         yield return unfill();
        //     }
        // }
    }

    IEnumerator MachineGunFire() {
        while(Input.GetButton("Fire1") && !isOVerheated) {
            InstantiateProjectile(currentWeapon.bullet, currentWeapon.firePoint.position, currentWeapon.firePoint.forward, false, 0.5f);

            machineGunOverheat += currentWeapon.overheatPerShot;

            if(machineGunOverheat >= currentWeapon.overheatThreshold) {
                isOVerheated = true;
                yield return new WaitForSeconds(currentWeapon.cooldownTime);
                machineGunOverheat = 0;
                isOVerheated = false;
            }

            yield return new WaitForSeconds(60f / currentWeapon.firingRate);
        }

        isFiring = false;
    }

    void HandleMachineGunCooldown() {
        if(!isFiring && machineGunOverheat > 0f) {
            overheatTimer += Time.deltaTime;
            
            if(overheatTimer >= overHeatDelay) {
                machineGunOverheat = Mathf.Max(0, machineGunOverheat - (1 / currentWeapon.cooldownTime * Time.deltaTime));

                if(machineGunOverheat <= 0f) {
                    overheatTimer = 0f;
                }
            }
        }
        else {
            overheatTimer = 0f;
        }
    }

    void UpdateCooldownUI() {
        for(int i = 0; i < weapons.Length; i++) {
            Weapon weapon = weapons[i];

            if(weapon.isCoolingDown) {
                float elapsedCooldown = weapon.cooldownTime - weapon.cooldownRemaining;
                float cooldownProgress = Mathf.Clamp01(elapsedCooldown / weapon.cooldownTime);
                weaponOverlay.UpdateCooldown(cooldownProgress, i);
                weapon.cooldownRemaining -= Time.deltaTime;

                if(weapon.cooldownRemaining <= 0) {
                    weapon.isCoolingDown = false;
                    weapon.cooldownRemaining = 0;
                }
            }
            else if(weapon.weaponType == Weapon.WeaponType.MachineGun && weapon == currentWeapon) {
                float overheatProgress = machineGunOverheat / weapon.overheatThreshold;
                weaponOverlay.UpdateCooldown(overheatProgress, i);
            }
        }
    }

    // IEnumerable unfill() {
    //     float overheatProgress = (float)currentWeapon.shotCounter / currentWeapon.maxRoundsBeforeOverheat;
    //     weaponOverlay.UpdateCooldown(overheatProgress, 2);
    //     yield return new WaitForSeconds(Time.deltaTime);
    // }

    void InstantiateProjectile(GameObject bulletPrefab, Vector3 position, Vector3 direction, bool isAreaDamage=false, float scale=1f)
    {
        GameObject projectile = Instantiate(bulletPrefab, position, Quaternion.LookRotation(-direction));
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = -direction * currentWeapon.bulletSpeed;
        rb.useGravity = true;


        projectile.AddComponent<Projectile>();
        Projectile projectileComponent = projectile.GetComponent<Projectile>();

        projectileComponent.damage = currentWeapon.bulletDamage;
        projectileComponent.isAreaDamage = isAreaDamage;
        projectileComponent.hitParticles = hitParticles;

        ParticleManager.PlayParticles(launchParticles, currentWeapon.firePoint.position, scale);
    }
}