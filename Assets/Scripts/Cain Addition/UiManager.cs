using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text Coin_Text;

    // constant variable for bars
    //player tab
    private int i_stamina = 0;
    private int i_endurance = 0;
    private int i_StoneSkin = 0;
    private int i_Jump = 0;
    private int i_wings = 0;
    private int i_slap = 0;
    private int i_punchingBag = 0;



    //slingshot tab
    private int i_DragForce = 0;

    //skill tab
    private int i_Shield = 0;
    private int i_frenzy = 0;



    [SerializeField]
    private Sprite[] ProgressBar;


    [SerializeField]
    private GameObject PauseMenu;
    private bool wasEscPressed = false;



    private Player _player;

  

    //Player tab

    //StaminaBar
    [SerializeField]
    private GameObject StaminaProgressBar;
    [SerializeField]
    private Text Stamina_Cost_Text;
    private int Stamina_bar_initailCost = 10;


    //EnduranceBar
    [SerializeField]
    private GameObject EnduranceProgressBar;
    [SerializeField]
    private Text Endurance_Cost_Text;
    private int Endurance_initailCost = 15;


    //StoneSkinBar
    [SerializeField]
    private GameObject StoneSkinProgressBar;
    [SerializeField]
    private Text StoneSkin_Cost_Text;
    private int StoneSkin_initailCost = 12;


    //JumpBar
    [SerializeField]
    private GameObject JumpProgressBar;
    [SerializeField]
    private Text Jump_Cost_Text;
    private int Jump_initailCost = 12;

    //----Player Tab ends---

    //SlingShot Tab starts

    //DragForce
    [SerializeField]
    private GameObject DragForceBar;
    [SerializeField]
    private Text DragForce_CostText;
    private int Drag_Force_initailCost = 20;

    // --SlingShot Tab ends--

    // Skill start

    //shield
    [SerializeField]
    private GameObject Shield_Bar;
    [SerializeField]
    private Text Shield_CostText;
    private int Shield_initailCost = 20;

    //frenzy
    [SerializeField]
    private GameObject Frenzy_Bar;
    [SerializeField]
    private Text Frenzy_CostText;
    private int Frenzy_initailCost = 9;

    //wings
    [SerializeField]
    private GameObject Wings_Bar;
    [SerializeField]
    private Text Wings_CostText;
    private int Wings_initailCost = 4;

    //Slap
    [SerializeField]
    private GameObject Slap_Bar;
    [SerializeField]
    private Text Slap_CostText;
    private int Slap_initailCost = 12;

    //Punch Bag
    [SerializeField]
    private GameObject PunchBag_Bar;
    [SerializeField]
    private Text PunchBag_CostText;
    private int PunchBag_initailCost = 15;












    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        _player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        Coin_Text.text = "" + _player.Total_Coins;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuu();

        }



    }

    private void PauseMenuu()
    {
        if (wasEscPressed == false)
        {
            PauseMenu.gameObject.SetActive(true);
            wasEscPressed = true;
            if (wasEscPressed == true)
            {
                Time.timeScale = 0;
            }

        }
        else if (wasEscPressed == true)
        {
            PauseMenu.gameObject.SetActive(false);
            wasEscPressed = false;
            if (wasEscPressed == false)
            {
                Time.timeScale = 1;
            }

        }

    }



    //Player Tab Update Section
    public void Stamina_UP()
    {
        Image spriteStamina = StaminaProgressBar.GetComponent<Image>();

        if (i_stamina < 10)
        {
            i_stamina += 1;
            spriteStamina.sprite = ProgressBar[i_stamina];

            switch (i_stamina)
            {
                case 1:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 1.5;
                    break;

                case 2:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 2;
                    break;

                case 3:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 2.5;
                    break;

                case 4:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 3;
                    break;

                case 5:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 3.5;
                    break;

                case 6:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 4;
                    break;

                case 7:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 4.5;
                    break;

                case 8:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 5;
                    break;

                case 9:
                    Stamina_Cost_Text.text = "" + Stamina_bar_initailCost * 5.5;
                    break;

                case 10:
                    Stamina_Cost_Text.text = "" ;
                    break;

            }

        }
    }
    public void Endurance_Up()
    {

        Image spriteEndurance = EnduranceProgressBar.GetComponent<Image>();

        if (i_endurance < 10)
        {
            i_endurance += 2;
            spriteEndurance.sprite = ProgressBar[i_endurance];

            switch (i_endurance)
            {
                
                case 2:
                    Endurance_Cost_Text.text = "" + Endurance_initailCost * 2;
                    break;

                

                case 4:
                    Endurance_Cost_Text.text = "" + Endurance_initailCost * 4;
                    break;

                

                case 6:
                    Endurance_Cost_Text.text = "" + Endurance_initailCost * 6;
                    break;

              

                case 8:
                    Endurance_Cost_Text.text = "" + Endurance_initailCost * 8;
                    break;

               

                case 10:
                    Endurance_Cost_Text.text = "" ;
                    break;

            }
        }

    }
    public void StoneSkin_Up()
    {
        Image spriteStoneSkin = StoneSkinProgressBar.GetComponent<Image>();

        if (i_StoneSkin < 9)
        {
            i_StoneSkin += 3;
            if (i_StoneSkin == 9)
            {
                spriteStoneSkin.sprite = ProgressBar[10];

            }
            else
            {
                spriteStoneSkin.sprite = ProgressBar[i_StoneSkin];

            }
            

            switch (i_StoneSkin)
            {
               

                case 3:
                    StoneSkin_Cost_Text.text = "" + StoneSkin_initailCost * 3;
                    break;

                

                case 6:
                    StoneSkin_Cost_Text.text = "" + StoneSkin_initailCost * 6;
                    break;

               

                case 9:
                    StoneSkin_Cost_Text.text = "" ;
                    break;

               

            }

        }
    }
    public void Jump_Up()
    {
        Image spriteJump = JumpProgressBar.GetComponent<Image>();

        if (i_Jump < 10)
        {

            
            if (i_Jump < 9)
            {
                i_Jump += 3;
                if (i_Jump == 9)
                {
                    spriteJump.sprite = ProgressBar[10];

                }
                else
                {
                    spriteJump.sprite = ProgressBar[i_Jump];

                }

                switch (i_Jump)
                {
                   

                    case 3:
                        Jump_Cost_Text.text = "" + Jump_initailCost * 3;
                        break;

                   

                    case 6:
                        Jump_Cost_Text.text = "" + Jump_initailCost * 6;
                        break;

                   

                    case 9:
                        Jump_Cost_Text.text = "";
                        break;

                    

                }

            }
        }
    }


    //SlingShot Tab Update Section
    public void DragForce_UP()
    {
        Image spriteDragForce = DragForceBar.GetComponent<Image>();

        if (i_DragForce < 10)
        {
            i_DragForce += 2;
            spriteDragForce.sprite = ProgressBar[i_DragForce];
            if (i_DragForce == 10)
            {
                DragForce_CostText.text = "" ;

            }
            else
            {
                DragForce_CostText.text = "" + Drag_Force_initailCost * i_DragForce;
            }
            




        }
    }


    //skills Tab Update Section
    public void Shield_UP()
    {
        Image spriteShieldUp = Shield_Bar.GetComponent<Image>();

        if (i_Shield < 10)
        {
            i_Shield += 5;
            spriteShieldUp.sprite = ProgressBar[i_Shield];
           

            switch (i_Shield)
            {
               

                case 5:
                    Shield_CostText.text = "" + Shield_initailCost * 5;
                    break;

              
                case 10:
                    Shield_CostText.text = "" ;
                    break;

            }




        }
    }

    public void Frenzy_UP()
    {
        Image spriteFrenzyUp = Frenzy_Bar.GetComponent<Image>();

        
            if (i_frenzy < 9)
            {
                i_frenzy += 3;
                if (i_frenzy == 9)
                {
                    spriteFrenzyUp.sprite = ProgressBar[10];

                }
                else
                {
                    spriteFrenzyUp.sprite = ProgressBar[i_frenzy];

                }


                switch (i_frenzy)
                {

                    case 3:
                        Frenzy_CostText.text = "" + Frenzy_initailCost * 3;
                        break;


                    case 6:
                        Frenzy_CostText.text = "" + Frenzy_initailCost * 6;
                        break;



                    case 9:
                        Frenzy_CostText.text = "";
                        break;



                }
            }
        
    }
    public void Wings_UP()
    {
        Image spriteWingsUp = Wings_Bar.GetComponent<Image>();

        if (i_wings < 10)
        {
            i_wings += 5;
            spriteWingsUp.sprite = ProgressBar[i_wings];


            switch (i_wings)
            {
               
               
                case 5:
                    Wings_CostText.text = "" + Wings_initailCost * 5;
                    break;

              

                case 10:
                    Wings_CostText.text = "";
                    break;

            }
        }

    }
    public void Slap_UP()
    {

        Image spriteSlapUp = Slap_Bar.GetComponent<Image>();
       

            if (i_slap < 9)
            {
                spriteSlapUp.sprite = ProgressBar[i_slap];
                i_slap += 3;
                if (i_slap == 9)
                {
                    spriteSlapUp.sprite = ProgressBar[10];

                }
                else
                {
                    spriteSlapUp.sprite = ProgressBar[i_slap];

                }



                switch (i_slap)
                {
                    
                    case 3:
                        Slap_CostText.text = "" + Slap_initailCost * 3;
                        break;


                    case 6:
                        Slap_CostText.text = "" + Slap_initailCost * 6;
                        break;

                   

                    case 9:
                        Slap_CostText.text = "";
                        break;

                   

                }
            

        }
    }
    public void PunchBag_UP()
    {
        Image spritePunchingBagUp = PunchBag_Bar.GetComponent<Image>();

        if (i_punchingBag < 10)
        {
            i_punchingBag += 5;
            spritePunchingBagUp.sprite = ProgressBar[i_punchingBag];


            switch (i_punchingBag)
            {
               

                case 5:
                    PunchBag_CostText.text = "" + PunchBag_initailCost * 5;
                    break;

               
                case 10:
                    PunchBag_CostText.text = "" ;
                    break;

            }
        }

    }



}   
