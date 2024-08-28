public class PlayerStats 
{
    //Allgemeine Stats
    public float movementSpeed = 2.0f; //Bewegungsgeschwindigkeit - noch nicht eingebaut
    public float castingTime = 1.8f; //Noch nicht eingebaut
    public float castingCap = 1.0f; // Cap für CastingTime, die nicht unterschritten werden darf
    private int maxHealth = 100; //Maximale Lebenspunkte (Cap) - wird bei LvlUp erhoeht
    private int health = 100; //Derzeitige Lebenspunkte
    private int maxMana = 80; //Maximale Manapunkte (Cap) - wird bei LvlUp erhoeht
    private int currentMana = 80; //Derzeitiges Mana
////////////////////////////////////////////////////////////////////////////////////////////////////
    //Erfahrungs- & Skillsystem
    private int experienze = 0; //Gesammelte Erfahrungspunkte
    private int level = 1; //Spielerstufe
    private int skillPoints = 0;
    private int spInHealth = 0;
    private int spInMana = 0;
    public int skillPointsPerLvl = 1; //Menge an Skillpunkten pro LevelUp
    public int capIncPerLvl = 5; //Cap um diese Zahl erhoehen
    public int regenPerTick = 5;
////////////////////////////////////////////////////////////////////////////////////////////////////
    public string warningBuffer; //String als buffer, um Nachrichten wie "Mana zu niedrig" zu uebergeben
////////////////////////////////////////////////////////////////////////////////////////////////////
    //Fibonacci Variablen
    //Fibonacci verwendet die Zahl vor der letzten und addiert diese 2 Zahlen zusammen. i.e. 0,1,1,2,3,5,8,13,21,34 usw.
    private int fibOne = 0;
    private int fibTwo = 1;
    private int fibSum = 1;

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////
    public void LevelUp()
    {
        //Allgemeines
        //CastingTime verringern
        if (castingTime > castingCap) castingTime = castingTime - 0.1f;
        level++;
        skillPoints += skillPointsPerLvl;
        ////////////////////////////////////////////////////////
        //Vorerst automatische Verteilung der Skillpunkte - Health oder Mana Cap erhoehen
        if (spInHealth > spInMana) {
            spInMana++;
            skillPoints--;
        }else {
            spInHealth++;
            skillPoints--;
        }
        ////////////////////////////////////////////////////////
        maxHealth = 100 + (spInHealth * capIncPerLvl);
        maxMana = 80 + (spInMana * capIncPerLvl);
        resetHealth();
        resetMana();
    }
    public void GainXp(int expGained)
    {
        //Code wenn Erfahrung gesammelt wurde
        this.experienze += expGained;
        if(this.experienze >= fibSum){
            LevelUp();
            this.experienze -= fibSum;
            fibOne = fibTwo;
            fibTwo = fibSum;
            fibSum = fibOne + fibTwo;
        }
    }
    public void regenerateHealth () {
		if (health <= (maxHealth-regenPerTick))
        this.health += regenPerTick;
	}

 ///////////////////////////////////////////////////////
    //Resetten aktuelles Mana und Health auf Maximalwert
    public void resetHealth () {
        //Auf Standardwert bei Spielstart zurücksetzen
        health = maxHealth;
    }
    public void resetMana () {
        //Auf Standardwert bei Spielstart zurücksetzen
        currentMana = maxMana;
    }
 ///////////////////////////////////////////////////////
    //Getter
    public int getMaxHealth () {
        return maxHealth;
    }
    public int getHealth () {
        return health;
    }
    public int getMaxMana () {
        return maxMana;
    }
    public int getCurrentMana () {
        return currentMana;
    }
    public float getMovementSpeed () {
        return movementSpeed;
    }
    public float getCastingTime () {
        return castingTime;
    }
    public int getFibSum () {
        return fibSum;
    }
    public int getLevel () {
        return this.level;
    }
    public int getExperienze () {
        return this.experienze;
    }
    public string getWarning() {
        return this.warningBuffer;
    }
///////////////////////////////////////////////////////
    //Setter
    public void setHealth (int mh) {
        health = mh;
    }
    public void setMaxMana (int mm) {
        maxMana = mm;
    }
    public void setMovementSpeed (float ms) {
       movementSpeed = ms;
    }
    public void setCastingTime (float ct) {
       castingTime = ct;
    }
    public void setCurrentMana (int cm) {
        currentMana = cm;
    }
///////////////////////////////////////////////////////
}