public class Weapon {
    private float strengthMultiplier;
    private float baseStrength;

    private WeaponType weaponType;

    public Weapon(float strengthMultiplier, float baseStrength, WeaponType weaponType) {
        this.strengthMultiplier = strengthMultiplier;
        this.baseStrength = baseStrength;
        this.weaponType = weaponType;
    }
}