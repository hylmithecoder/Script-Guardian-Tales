// Now Sepuh do create a Save System
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public float health;
    public float[] position;

    public PlayerData (statprototype player)
    {
        level = player.level;
        health = player.maxHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
[System.Serializable]
public class StaminaData
{
    public int amount;
    public int maxStamina;

    public StaminaData (ItemTerpenting stamina)
    {
        amount = stamina.jumlah;
        maxStamina = stamina.maxStamina;
    }
}