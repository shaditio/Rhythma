using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int combo;
    public int maxCombo;

    public void incrementCombo(){
        combo++;
        checkCombo();
    }

    public void resetCombo(){
        combo = 0;
    }

    public void checkCombo(){
        if(combo >= maxCombo){
            maxCombo = combo;
        }
    }
}
