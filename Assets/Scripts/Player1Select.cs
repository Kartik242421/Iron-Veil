using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class P1Select : MonoBehaviour
{
    public int MaxIcons = 6;
    public int IconsPerRow = 3;
    public int MaxRows = 2;

    public GameObject EveP1;
    public GameObject MorakP1;
    public GameObject MariaP1;
    public GameObject ElyP1;
    public GameObject SynthP1;
    public GameObject VanguardP1;

    public GameObject EveP1Text;
    public GameObject MorakP1Text;
    public GameObject MariaP1Text;
    public GameObject ElyP1Text;
    public GameObject SynthP1Text;
    public GameObject VanguardP1Text;

    public Text Player1Name;
    

    public string CharacterSelectionP1;     

    private int IconNumber = 1;
    private int RowNumber = 1;
    private float PauseTime = 1.0f;
    public float TimerMax = 0.6f;
    private bool TimeCountDown = false;
    private bool ChangeCharacter = false;
    private AudioSource MyPlayer;

    [Header("Mobile UI")]
    public Text MobileP1;
    public GameObject P1Panel;
    public GameObject P2Panel;

    // Start is called before the first frame update
    void Start()
    {
        SaveHealthData.Round = 0;
        SaveHealthData.Player1Wins = 0;
        SaveHealthData.Player2Wins = 0;
        ChangeCharacter = true;
        Time.timeScale = 0.6f;
        MyPlayer = GetComponent<AudioSource>();
        PauseTime = TimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SaveHealthData.P1Select);
        if (ChangeCharacter == true)
        {
            if (IconNumber == 1)
            {
                SwitchOff();
                EveP1.gameObject.SetActive(true);
                EveP1Text.gameObject.SetActive(true);
                Player1Name.text = "Eve";
                CharacterSelectionP1 = "EveP1";
                ChangeCharacter = false;
            }
            if (IconNumber == 4)
            {
                SwitchOff();
                MorakP1.gameObject.SetActive(true);
                MorakP1Text.gameObject.SetActive(true);
                Player1Name.text = "Morak";
                CharacterSelectionP1 = "MorakP1";
                ChangeCharacter = false;
            }
            if (IconNumber == 3)
            {
                SwitchOff();
                MariaP1.gameObject.SetActive(true);
                MariaP1Text.gameObject.SetActive(true);
                Player1Name.text = "Maria";
                CharacterSelectionP1 = "MariaP1";
                ChangeCharacter = false;
            }
            if (IconNumber == 6)
            {
                SwitchOff();
                ElyP1.gameObject.SetActive(true);
                ElyP1Text.gameObject.SetActive(true);
                Player1Name.text = "Ely";
                CharacterSelectionP1 = "ElyP1";
                ChangeCharacter = false;
            }
            if (IconNumber == 5)
            {
                SwitchOff();
                SynthP1.gameObject.SetActive(true);
                SynthP1Text.gameObject.SetActive(true);
                Player1Name.text = "Synth";
                CharacterSelectionP1 = "SynthP1";
                ChangeCharacter = false;
            }
            if (IconNumber == 2)
            {
                SwitchOff();
                VanguardP1.gameObject.SetActive(true);
                VanguardP1Text.gameObject.SetActive(true);
                Player1Name.text = "Vanguard";
                CharacterSelectionP1 = "VanguardP1";
                ChangeCharacter = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            SaveHealthData.P1Select = CharacterSelectionP1;
            MyPlayer.Play();
            NextPlayer();
        }

        if (TimeCountDown == true)
        {
            if (PauseTime > 0.1f)
            {
                PauseTime -= 1.0f * Time.deltaTime;
            }
            if (PauseTime <= 0.1f)
            {
                PauseTime = TimerMax;
                TimeCountDown = false;
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (PauseTime == TimerMax)
            {
                if (IconNumber < IconsPerRow * RowNumber)
                {
                    IconNumber++;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (PauseTime == TimerMax)
            {
                if (IconNumber > IconsPerRow * (RowNumber - 1) + 1)
                {
                    IconNumber--;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (PauseTime == TimerMax)
            {
                if (RowNumber < MaxRows)
                {
                    IconNumber += IconsPerRow;
                    RowNumber++;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            if (PauseTime == TimerMax)
            {
                if (RowNumber > 1)
                {
                    IconNumber -= IconsPerRow;
                    RowNumber--;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
    }

    void SwitchOff()
    {
        EveP1.gameObject.SetActive(false);
        MorakP1.gameObject.SetActive(false);
        MariaP1.gameObject.SetActive(false);
        ElyP1.gameObject.SetActive(false);
        SynthP1.gameObject.SetActive(false);
        VanguardP1.gameObject.SetActive(false);
        EveP1Text.gameObject.SetActive(false);
        MorakP1Text.gameObject.SetActive(false);
        MariaP1Text.gameObject.SetActive(false);
        ElyP1Text.gameObject.SetActive(false);
        SynthP1Text.gameObject.SetActive(false);
        VanguardP1Text.gameObject.SetActive(false);
    }

    void NextPlayer()
    {
        EveP1Text.gameObject.SetActive(false);
        MorakP1Text.gameObject.SetActive(false);
        MariaP1Text.gameObject.SetActive(false);
        ElyP1Text.gameObject.SetActive(false);
        SynthP1Text.gameObject.SetActive(false);
        VanguardP1Text.gameObject.SetActive(false);

        if (SaveHealthData.Player1Mode == true)
        {
            this.gameObject.GetComponent<CPUSelect>().enabled = true;
            this.gameObject.GetComponent<P1Select>().enabled = false;
        }
        if (SaveHealthData.Player1Mode == false)
        {
            this.gameObject.GetComponent<P2Select>().enabled = true;
            this.gameObject.GetComponent<P1Select>().enabled = false;
        }
    }
    //Mobile Controls
   public void Eve()
    {
        SwitchOff();
        EveP1.gameObject.SetActive(true);
        EveP1Text.gameObject.SetActive(true);
        MobileP1.text = "Eve";
        CharacterSelectionP1 = "EveP1";
        ChangeCharacter = false;
    }
    public void Ely()
    {
        SwitchOff();
        ElyP1.gameObject.SetActive(true);
        ElyP1Text.gameObject.SetActive(true);
        MobileP1.text = "Ely";
        CharacterSelectionP1 = "ElyP1";
        ChangeCharacter = false;
    }
    public void Morak()
    {
        SwitchOff();
        MorakP1.gameObject.SetActive(true);
        MorakP1Text.gameObject.SetActive(true);
        MobileP1.text = "Morak";
        CharacterSelectionP1 = "MorakP1";
        ChangeCharacter = false;
    }
    public void Synth()
    {
        SwitchOff();
        SynthP1.gameObject.SetActive(true);
        SynthP1Text.gameObject.SetActive(true);
        MobileP1.text = "Synth";
        CharacterSelectionP1 = "SynthP1";
        ChangeCharacter = false;
    }
    public void Marie()
    {
        SwitchOff();
        MariaP1.gameObject.SetActive(true);
        MariaP1Text.gameObject.SetActive(true);
        MobileP1.text = "Maria";
        CharacterSelectionP1 = "MariaP1";
        ChangeCharacter = false;
    }
    public void Vanguard()
    {
        SwitchOff();
        VanguardP1.gameObject.SetActive(true);
        VanguardP1Text.gameObject.SetActive(true);
        MobileP1.text = "Vanguard";
        CharacterSelectionP1 = "VanguardP1";
        ChangeCharacter = false;
    }
    public void choose()
    {
        SaveHealthData.P1Select = CharacterSelectionP1;
        MyPlayer.Play();
        NextPlayer();
        P1Panel.gameObject.SetActive(false);
        P2Panel.gameObject.SetActive(true);
    }
}
