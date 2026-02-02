using System;

[System.Serializable]  // This makes the class serializable for JSON
public class WeaponSaveData
{
    public int barrelIndex;        // Index of the current barrel attachment
    public int barrelGuardIndex;   // Index of the current barrel guard attachment
    public bool isExtendedMag;     // State of the extended magazine

    // Constructor to easily create a WeaponSaveData instance
    public WeaponSaveData(int barrelIndex, int barrelGuardIndex, bool isExtendedMag)
    {
        this.barrelIndex = barrelIndex;
        this.barrelGuardIndex = barrelGuardIndex;
        this.isExtendedMag = isExtendedMag;
    }
}


//This class will store the index of the barrel, the barrel guard, and the magazine state (whether it’s extended or not).
