using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class HUDController : MonoBehaviour
{
    [Header("HEALTH")]
    public Image healthFill;
    public TMP_Text healthText;


    [Header("HEALTH COLORS")]
    public Color normalHealthColor = new Color32(255, 92, 168, 255);   //Pink
    public Color damageHealthColor = new Color32(255, 184, 107, 255);    // Peach
    public Color criticalHealthColor = new Color32(255, 60, 80, 255); //Red
    public Color healingHealthColor = new Color32(0, 240, 255, 255);  //Cyan


    [Header("STAMINA")]
    public Image staminaFill;
    public TMP_Text staminaText;

    [Header("AMMO")]
    public TMP_Text currentAmmoText;
    public TMP_Text reserveAmmoText;
    public TMP_Text weaponNameText;
    public TMP_Text reloadingText;

    [Header("AMMO COLORS")]
    public Color normalAmmoColor = new Color32(0, 240, 255, 255);   // Cyan
    public Color lowAmmoColor = new Color32(255, 184, 107, 255);    // Peach
    public Color emptyAmmoColor = new Color32(255, 92, 168, 255);   // Pink

    // TEST VALUES
    private float maxHealth = 100f;
    private float currentHealth = 100f;

    private float maxStamina = 100f;
    private float currentStamina = 100f;

    private int magazineSize = 12;
    private int currentAmmo = 12;
    private int reserveAmmo = 48;

    private bool isReloading = false;
    private float reloadTime = 1.5f;

    void Start()
    {
        weaponNameText.text = "VX-9";

        reloadingText.gameObject.SetActive(false);

        UpdateHUD();
    }

    void Update()
    {
        // H = Take damage
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }

        // J = Heal
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Heal(10f);
        }

        // Hold Space = Use stamina
        if (Keyboard.current.spaceKey.isPressed)
        {
            currentStamina -= 30f * Time.deltaTime;
        }
        else
        {
            currentStamina += 20f * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(
            currentStamina,
            0f,
            maxStamina
        );

        // Left Mouse = Shoot
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

        // R = Reload
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Reload();
        }

        UpdateHUD();
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );
    }

    void Heal(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );
    }

    void Shoot()
    {
        if (isReloading)
            return;

        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }

    void Reload()
    {
        if (isReloading)
          return;

        if (currentAmmo == magazineSize)
         return;

        if (reserveAmmo <= 0)
         return;

        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;

        // Show RELOADING and hide ammo
        reloadingText.gameObject.SetActive(true);
        currentAmmoText.gameObject.SetActive(false);
        reserveAmmoText.gameObject.SetActive(false);

        // Wait 1.5 seconds
        yield return new WaitForSeconds(reloadTime);

        int ammoNeeded = magazineSize - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;

        // Hide RELOADING and show ammo again
        reloadingText.gameObject.SetActive(false);
        currentAmmoText.gameObject.SetActive(true);
        reserveAmmoText.gameObject.SetActive(true);

        isReloading = false;

        UpdateHUD();
    }

    void UpdateHUD()
    {
        // HEALTH
        healthFill.fillAmount =
            currentHealth / maxHealth;

        healthText.text =
            "HP // " + Mathf.RoundToInt(currentHealth);

        // STAMINA
        staminaFill.fillAmount =
            currentStamina / maxStamina;

        staminaText.text =
            "STM // " + Mathf.RoundToInt(currentStamina);

        // AMMO NUMBERS
        if (isReloading)
        {
            currentAmmoText.text = "RELOADING...";
            reserveAmmoText.text = "";
        }
        else
        {
            currentAmmoText.text =
                currentAmmo.ToString("00");

            reserveAmmoText.text =
                "/ " + reserveAmmo.ToString("00");
        }

        // AMMO COLOR
        if (isReloading)
        {
            currentAmmoText.color = normalAmmoColor;
        }
        else if (currentAmmo == 0)
        {
            currentAmmoText.color = emptyAmmoColor;
        }
        else if (currentAmmo <= 3)
        {
            currentAmmoText.color = lowAmmoColor;
        }
        else
        {
            currentAmmoText.color = normalAmmoColor;
        }
    }
}