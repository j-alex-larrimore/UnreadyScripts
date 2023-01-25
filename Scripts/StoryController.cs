using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {

    public static StoryController Instance;

    public GameObject storyScreen;
    //public Sprite[] storyPics;
    //public GameObject storyImage;
    //private GameObject textPanel;
    public Text storyText;
    //private Text taglineText;
    public GameObject[] storyPanels;
    /*public GameObject storyPanelIntro;
    public GameObject storyPanel1Intro;
    public GameObject storyPanel2;
    public GameObject storyPanel3;
    public GameObject storyPanel4;
    public GameObject storyPanel5;
    public GameObject storyPanel6;
    public GameObject storyPanel7;
    public GameObject storyPanel8;*/
    private double story = 0;
    public bool storyShown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        
    }

    // Use this for initialization
    void Start()
    {
        /*storyText = GameObject.Find("StoryText").GetComponent<Text>();
        storyScreen = GameObject.Find("StoryScreen");
        storyPanel1 = GameObject.Find("Story1");
        storyPanel2 = GameObject.Find("Story2");
        storyPanel3 = GameObject.Find("Story3");
        storyPanel4 = GameObject.Find("Story4");
        storyPanel5 = GameObject.Find("Story5");
        storyPanel6 = GameObject.Find("Story6");
        storyPanel7 = GameObject.Find("Story7");
        storyPanel8 = GameObject.Find("Story8");
        storyPanel2.SetActive(false);
        storyPanel3.SetActive(false);
        storyPanel4.SetActive(false);
        storyPanel5.SetActive(false);
        storyPanel6.SetActive(false);
        storyPanel7.SetActive(false);
        storyPanel8.SetActive(false);*/
        //storyImage = GameObject.Find("StoryImage");
        storyScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            MoveStory();
        }
       if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseStory();
        }*/
    }

    public void ShowStory(int storyNum)
    {
        /*if (GameController.Instance.gameLoadedMidLevel)
        {
            return;
        }*/

        storyScreen.SetActive(true);
        HideButtons();
        BattleController.Instance.PauseBattle();
        GameController.Instance.ArmyPause();

        switch (storyNum)
        {
            case 0:
                Story0A();
                break;
            case 1:
                Story1A();
                break;
            case 2:
                Story2A();
                break;
            case 3:
                Story3A();
                break;
            case 4:
                Story4A();
                break;
            case 5:
                Story5A();
                break;
            case 6:
                Story6A();
                break;
            case 7:
                Story7A();
                break;
            case 8:
                Story8A();
                break;
            case 9:
                Story9A();
                break;
            case 10:
                Story10A();
                break;
            case 11:
                Story11A();
                break;
            case 12:
                InterludeOneA();
                break;
            case 13:
                Story13A();
                break;
            case 14:
                Story14A();
                break;
            case 15:
                Story15A();
                break;
            case 16:
                InterludeTwoA();
                break;
            case 17:
                Story17A();
                break;
            case 18:
                Story18A();
                break;
            case 19:
                Story19A();
                break;
            case 20:
                Story20A();
                break;
            case 21:
                Story21A();
                break;
            case 22:
                Story22A();
                break;
            case 23:
                Story23A();
                break;
            case 24:
                Story24A();
                break;
            case 25:
                Story25A();
                break;
            case 26:
                Story26A();
                break;
            case 27:
                Story27A();
                break;
            case 28:
                Story28A();
                break;
            case 29:
                Story29A();
                break;
            case 30:
                Story30A();
                break;
            /*case 31:
                InterludeOneA();
                break;
            case 32:
                InterludeTwoA();
                break;*/
            case 34:
                Story1C();
                break;
            case 35:
                Story1D();
                break;
            case 36:
                Story2B();
                break;
            case 37:
                Story3C();
                break;
            case 38:
                Story3D();
                break;
            case 39:
                Story4D();
                break;
            case 40:
                Story5E();
                break;
            case 41:
                Story6C();
                break;
            case 42:
                Story7D();
                break;
            case 43:
                Story8H();
                break;
            case 44:
                Story9E();
                break;
            case 45:
                Story10D();
                break;
            case 46:
                Story11C();
                break;
            case 47:
                Story12D();
                break;
            case 48:
                Story13D();
                break;
            case 49:
                Story14D();
                break;
            case 50:
                Story15D();
                break;
            case 51:
                Story16C();
                break;
            case 52:
                Story17C();
                break;
            case 53:
                Story18C();
                break;
            case 54:
                Story19C();
                break;
            case 55:
                Story20D();
                break;
            case 56:
                Story21C();
                break;
            case 57:
                Story22B();
                break;
            case 58:
                Story23D();
                break;
            case 59:
                Story24C();
                break;
            case 60:
                Story25C();
                break;
            case 61:
                Story26D();
                break;
            case 62:
                Story27C();
                break;
            case 63:
                Story28C();
                break;
            case 64:
                Story29D();
                break;
            case 65:
                Story30C();
                break;
            default:
                break;
        }
        
    }

    public void CloseStory()
    {
        BattleController.Instance.UnpauseBattle();
        GameController.Instance.ArmyUnpause();

        ClosePanels();
        ShowButtons();
        storyScreen.SetActive(false);
    }

    public void ClosePanels()
    {
        foreach (GameObject panel in storyPanels)
        {
            panel.SetActive(false);
        }
    }

    public void SetBackground(int backNum)
    {
        //storyImage.GetComponent<Image> ().sprite = storyPics [backNum];
        //string panelName = "storyPanel" + backNum + 1;

    }

    public void MoveStory()
    {
        if (story == 0.0)
        {
            Story0B();
        }
        else if (story == 0.1)
        {
            Story0C();
        }
        else if (story == 0.2)
        {
            Story0D();
        }
        else if (story == 0.3)
        {
            Story0E();
        }
        else if (story == 0.4)
        {
            Story0F();
        }
        else if (story == 1.0)
        {
            Story1B();
        }
        else if (story == 1.4)
        {
            Story1E();
        }
        else if (story == 1.5)
        {
            Story1F();
        }
        else if (story == 1.6)
        {
            Story1G();
        }
        else if (story == 1.7)
        {
            Story1H();
        }
        else if (story == 1.03)
        {
            Story1I();
        }else if (story == 2.02)
        {
            Story2I();
        }
        else if (story == 3.0)
        {
            Story3B();
        }
        else if (story == 3.3)
        {
            Story3E();
        }
        else if (story == 3.4)
        {
            Story3F();
        }
        else if (story == 3.5)
        {
            Story3G();
        }
        else if (story == 3.6)
        {
            Story3H();
        }
        else if (story == 4.0)
        {
            Story4B();
        }
        else if (story == 4.1)
        {
            Story4C();
        }
        else if (story == 4.3)
        {
            Story4E();
        }
        else if (story == 5.0)
        {
            Story5B();
        }
        else if (story == 5.1)
        {
            Story5C();
        }
        else if (story == 5.2)
        {
            Story5D();
        }
        else if (story == 5.4)
        {
            Story5F();
        }
        else if (story == 5.5)
        {
            Story5G();
        }
        else if (story == 6.0)
        {
            Story6B();
        }
        else if (story == 6.2)
        {
            Story6D();
        }
        else if (story == 6.3)
        {
            Story6E();
        }
        else if (story == 6.4)
        {
            Story6F();
        }
        else if (story == 6.5)
        {
            Story6G();
        }
        else if (story == 7.0)
        {
            Story7B();
        }
        else if (story == 7.1)
        {
            Story7C();
        }
        else if (story == 7.3)
        {
            Story7E();
        }
        else if (story == 7.4)
        {
            Story7F();
        }
        else if (story == 7.5)
        {
            Story7G();
        }
        else if (story == 7.6)
        {
            Story7H();
        }
        else if (story == 7.7)
        {
            Story7I();
        }
        else if (story == 7.8)
        {
            Story7J();
        }
        else if (story == 7.9)
        {
            Story7K();
        }
        else if (story == 8.0)
        {
            Story8B();
        }
        else if (story == 8.1)
        {
            Story8C();
        }
        else if (story == 8.2)
        {
            Story8D();
        }
        else if (story == 8.3)
        {
            Story8E();
        }
        else if (story == 8.4)
        {
            Story8F();
        }
        else if (story == 8.5)
        {
            Story8G();
        }
        else if (story == 8.7)
        {
            Story8I();
        }
        else if (story == 8.8)
        {
            Story8J();
        }
        else if (story == 8.9)
        {
            Story8K();
        }
        else if (story == 9.0)
        {
            Story9B();
        }
        else if (story == 9.1)
        {
            Story9C();
        }
        else if (story == 9.2)
        {
            Story9D();
        }
        else if (story == 9.4)
        {
            Story9F();
        }
        else if (story == 9.5)
        {
            Story9G();
        }
        else if (story == 9.6)
        {
            Story9H();
        }
        else if (story == 10.0)
        {
            Story10B();
        }
        else if (story == 10.1)
        {
            Story10C();
        }
        else if (story == 11.0)
        {
            Story11B();
        }
        else if (story == 31.14)
        {
            Story12A();
        }
        else if (story == 12.0)
        {
            Story12B();
        }
        else if (story == 12.1)
        {
            Story12C();
        }
        else if (story == 13.0)
        {
            Story13B();
        }
        else if (story == 13.1)
        {
            Story13C();
        }
        else if (story == 13.3)
        {
            Story13E();
        }
        else if (story == 14.0)
        {
            Story14B();
        }
        else if (story == 14.1)
        {
            Story14C();
        }
        else if (story == 14.3)
        {
            Story14E();
        }
        else if (story == 15.0)
        {
            Story15B();
        }
        else if (story == 15.1)
        {
            Story15C();
        }
        else if (story == 32.6)
        {
            Story16A();
        }
        else if (story == 16.0)
        {
            Story16B();
        }
        else if (story == 16.2)
        {
            Story16D();
        }
        else if (story == 17.0)
        {
            Story17B();
        }
        else if (story == 18.0)
        {
            Story18B();
        }
        else if (story == 18.2)
        {
            Story18D();
        }
        else if (story == 19.0)
        {
            Story19B();
        }
        else if (story == 19.2)
        {
            Story19D();
        }
        else if (story == 20.0)
        {
            Story20B();
        }
        else if (story == 20.1)
        {
            Story20C();
        }
        else if (story == 20.3)
        {
            Story20E();
        }
        else if (story == 20.4)
        {
            Story20F();
        }
        else if (story == 20.5)
        {
            Story20G();
        }
        else if (story == 21.0)
        {
            Story21B();
        }
        else if (story == 21.2)
        {
            Story21D();
        }
        else if (story == 21.3)
        {
            Story21E();
        }
        else if (story == 22.1)
        {
            Story22C();
        }
        else if (story == 22.2)
        {
            Story22D();
        }
        else if (story == 22.3)
        {
            Story22E();
        }
        else if (story == 23.0)
        {
            Story23B();
        }
        else if (story == 23.1)
        {
            Story23C();
        }
        else if (story == 24.0)
        {
            Story24B();
        }
        else if (story == 25.0)
        {
            Story25B();
        }
        else if (story == 25.2)
        {
            Story25D();
        }
        else if (story == 25.3)
        {
            Story25E();
        }
        else if (story == 25.4)
        {
            Story25F();
        }
        else if (story == 25.5)
        {
            Story25G();
        }
        else if (story == 25.6)
        {
            Story25H();
        }
        else if (story == 25.7)
        {
            Story25I();
        }
        else if (story == 25.8)
        {
            Story25J();
        }
        else if (story == 25.9)
        {
            Story25K();
        }
        else if (story == 25.01)
        {
            Story25L();
        }
        else if (story == 26.0)
        {
            Story26B();
        }
        else if (story == 26.1)
        {
            Story26C();
        }
        else if (story == 26.3)
        {
            Story26E();
        }
        else if (story == 26.4)
        {
            Story26F();
        }
        else if (story == 27.0)
        {
            Story27B();
        }
        else if (story == 27.2)
        {
            Story27D();
        }
        else if (story == 27.3)
        {
            Story27E();
        }
        else if (story == 28.0)
        {
            Story28B();
        }
        else if (story == 29.0)
        {
            Story29B();
        }
        else if (story == 29.1)
        {
            Story29C();
        }
        else if (story == 29.3)
        {
            Story29E();
        }
        else if (story == 29.4)
        {
            Story29F();
        }
        else if (story == 29.5)
        {
            Story29G();
        }
        else if (story == 30.0)
        {
            Story30B();
        }
        else if (story == 30.2)
        {
            Story30D();
        }
        else if (story == 30.3)
        {
            Story30E();
        }
        else if (story == 30.4)
        {
            Story30F();
        }else if(story == 31.0)
        {
            InterludeOneB();
        }
        else if (story == 31.1)
        {
            InterludeOneC();
        }
        else if (story == 31.2)
        {
            InterludeOneD();
        }
        else if (story == 31.3)
        {
            InterludeOneE();
        }
        else if (story == 31.4)
        {
            InterludeOneF();
        }
        else if (story == 31.5)
        {
            InterludeOneG();
        }
        else if (story == 31.6)
        {
            InterludeOneH();
        }
        else if (story == 31.7)
        {
            InterludeOneI();
        }
        else if (story == 31.8)
        {
            InterludeOneJ();
        }
        else if (story == 31.9)
        {
            InterludeOneK();
        }
        else if (story == 31.01)
        {
            InterludeOneL();
        }
        else if (story == 31.11)
        {
            InterludeOneM();
        }
        else if (story == 31.12)
        {
            InterludeOneN();
        }
        else if (story == 31.13)
        {
            InterludeOneO();
        }
        else if (story == 32.0)
        {
            InterludeTwoB();
        }
        else if (story == 32.1)
        {
            InterludeTwoC();
        }
        else if (story == 32.2)
        {
            InterludeTwoD();
        }
        else if (story == 32.3)
        {
            InterludeTwoE();
        }
        else if (story == 32.4)
        {
            InterludeTwoF();
        }
        else if (story == 32.5)
        {
            InterludeTwoG();
        }
        else
        {
            CloseStory();
        }
    }

    public void Story0A()
    {
        storyText.text = "General Imperius: Well, young " + ArmyData.Instance.GetHeroName() + ", congratulations on being this year’s valedictorian. We expect great things from you! " +
            "I was in your shoes not all that many years ago. As you know this means you are the only Captain from your graduating class. You will be given a company to command within Colonel Ignavum’s battalion. ";
        story = 0.0;
        storyPanels[0].SetActive(false);
        storyPanels[1].SetActive(true);
    }

    public void Story0B()
    {
        storyText.text = "General Imperius: I debated sending you out west to the mountains to guard our borders against the Malum Empire, but given the current peace that seems like a waste of your potential.";
        story = 0.1;
        storyPanels[1].SetActive(false);
        storyPanels[2].SetActive(true);
    }

    public void Story0C()
    {
        storyText.text = "General Imperius: I also considered putting you to work in the southern plains outside of Newton to put down this supposed rebellion but decided that would be a tough job to start your career.";
        story = 0.2;
        storyPanels[2].SetActive(false);
        storyPanels[3].SetActive(true);
    }

    public void Story0D()
    {
        storyText.text = "General Imperius: I’m not positive I believe all the reports coming from the Obsideor forests in the north either and don’t want to send you on a wild goose chase.";
        story = 0.3;
        storyPanels[3].SetActive(false);
        storyPanels[4].SetActive(true);
    }

    public void Story0E()
    {
        storyText.text = "General Imperius: Instead, you’ll be protecting locals in the Effera wilds to the east. The beasts are supposedly acting up this year and Colonel Ignavum is being dispatched to clear the area.";
        story = 0.4;
        storyPanels[4].SetActive(false);
        storyPanels[5].SetActive(true);
    }

    public void Story0F()
    {
        storyText.text = "General Imperius: I’m giving you the best role I could think of. Don’t waste it and don’t let me down. I want to hear glowing reports when I check on you.";
        story = 0.5;
        storyPanels[5].SetActive(false);
        storyPanels[6].SetActive(true);
    }

    public void Story1A()
    {
        storyText.text = "Colonel Ignavum: Well Captain " + ArmyData.Instance.GetHeroName() + " I hear I was lucky enough to get the best of the rookies. " +
            "Today’s an exploratory mission so I don’t expect much trouble. You should take the advice of Lieutenant Vafer and Sergeant Ferox who I have assigned to your company. " +
            "They both have a lot of experience in the area.";
        story = 1.1;
        storyPanels[6].SetActive(false);
        storyPanels[7].SetActive(true);
    }

    public void Story1B()
    {
        storyText.text = "Colonel Ignavum: We’ll be moving through the Points carefully. Captain’s Bryant, Grace, Banks and I will be clearing key points as we go. " +
            "You’ll be protecting the fort and staying ready in case I need to send you as a reserve if we actually run into trouble.";
        story = 1.2;
        storyPanels[6].SetActive(false);
        storyPanels[7].SetActive(true);
    }

    public void Story1C()
    {
        storyText.text = "Lieutenant Vafer: Captain! The Colonel is abandoning the field! We are on our own!";
        story = 1.03;
    }

    public void Story1D()
    {
        storyText.text = "Lieutenant Vafer: There’s no sign of Colonel Ignavum. If his company made it out alive they must have been unable to make it back to the Northern Outpost.";
        story = 1.4;
        storyPanels[7].SetActive(false);
        storyPanels[8].SetActive(true);
    }

    public void Story1E()
    {
        storyText.text = "Sergeant Ferox: I was there and I still don’t believe it. Animals fighting alongside people? And what in the world were those things that hit the other companies?";
        story = 1.5;
        storyPanels[8].SetActive(false);
        storyPanels[9].SetActive(true);
    }

    public void Story1F()
    {
        storyText.text = "Lieutenant Vafer: No idea. What do we do now Captain? We’ve been tasked with protecting the citizens out here but all we have left is a bunch of rookies.";
        story = 1.6;
        storyPanels[9].SetActive(false);
        storyPanels[10].SetActive(true);
    }

    public void Story1G()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We certainly have a better shot than the civilians do. Hopefully the Colonel took care of some of the more serious problems but we better start recruiting.";
        story = 1.7;
        storyPanels[10].SetActive(false);
        storyPanels[11].SetActive(true);
    }

    public void Story1H()
    {
        CloseStory();
        TutorialController.Instance.ShowTutorial(1);
    }

    public void Story1I()
    {
        CloseStory();
        story = 1.3;
        TutorialController.Instance.ShowTutorial(0);
    }

    public void Story2A()
    {
        storyText.text = "Lieutenant Vafer: Alright Captain, we are as ready as we are likely to be. it’s your show now. Let’s talk about how to manage our battalion.";
        story = 2.02;
        storyPanels[11].SetActive(false);
        storyPanels[12].SetActive(true);
    }

    public void Story2B()
    {
        storyText.text = "Lieutenant Vafer: Whew, we did it! Congratulations Captain! We should restock and think about our next move. ";
        story = 2.2;
        storyPanels[12].SetActive(false);
        storyPanels[13].SetActive(true);
    }

    public void Story2I()
    {
        CloseStory();
        story = 2.1;
        TutorialController.Instance.ShowTutorial(2);
    }

    public void Story3A()
    {
        storyText.text = "Colonel Ignavum: General! We had a disaster in the east. Nobody survived. You won’t believe what we ran into…";
        story = 3.0;
        storyPanels[13].SetActive(false);
        storyPanels[14].SetActive(true);
    }

    public void Story3B()
    {
        storyText.text = "Lieutenant Vafer: This is the Eastern Points, the most populated area around. " +
            "Good choice on helping them secure the area. We need to clear the fields carefully as there might be beasts hiding anywhere. " +
            "I recommend we use the farmsteads to the east as forward bases when we get to them.";
        story = 3.1;
        storyPanels[14].SetActive(false);
        storyPanels[15].SetActive(true);
    }

    public void Story3C()
    {
        storyText.text = "Bandit Leader: You think you can survive against us? This forest is under our protection. Now you will feel our power.";
        story = 3.2;
        storyPanels[15].SetActive(false);
        //storyPanels[16].SetActive(true);
    }

    public void Story3D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Although we won I don’t think we can protect all of these farmsteaders. " +
            "Tell everyone to take what they still have and move back to Fort Spear. " +
            "We’ll keep clearing the area but I’m not invading a foreign power without direct orders to do so.";
        story = 3.3;
        storyPanels[16].SetActive(false);
        storyPanels[17].SetActive(true);
    }

    public void Story3E()
    {
        storyText.text = "Sergeant Ferox: Hey, I found a friend! Can I keep him?";
        story = 3.4;
        storyPanels[17].SetActive(false);
        storyPanels[18].SetActive(true);
    }

    public void Story3F()
    {
        storyText.text = "Lieutenant Vafer: What in the… That’s a wolf! You can’t keep a wolf!";
        story = 3.6;
        storyPanels[18].SetActive(false);
        storyPanels[19].SetActive(true);
    }

    public void Story3G()
    {
        storyText.text = "Sergeant Ferox: He gets me! Don’t ruin our moment.";
        story = 3.7;
        storyPanels[19].SetActive(false);
        storyPanels[20].SetActive(true);
    }

    public void Story3H()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I’m not sure we’re in a position to turn away any help we can get. " +
            "If you think you can work with him he’s your responsibility. Also, send two runners back to the capital to make sure they know we are out here. " +
            "I figured we’d receive more support or at least orders by now.";
        story = 3.8;
        storyPanels[20].SetActive(false);
        storyPanels[21].SetActive(true);
    }

    public void Story4A()
    {
        storyText.text = "Lieutenant Vafer: This should be the last of the homesteaders. Our scouts saw a good amount of movement in the trees to the east so we will need to move quickly if we want to evacuate everyone and avoid a fight.";
        story = 4.0;
        storyPanels[21].SetActive(false);
        storyPanels[22].SetActive(true);
    }

    public void Story4B()
    {
        storyText.text = "Sergeant Ferox: Avoid a fight? We’ve been kicking their butts up and down this forest. Let’s teach them who we are until they learn.";
        story = 4.1;
        storyPanels[22].SetActive(false);
        storyPanels[23].SetActive(true);
    }

    public void Story4C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Not with civilians in the mix. Let’s move.";
        story = 4.2;
        storyPanels[23].SetActive(false);
        storyPanels[24].SetActive(true);
    }

    public void Story4D()
    {
        storyText.text = "Lieutenant Vafer: Ok that should be everyone. What next?";
        story = 4.3;
        storyPanels[24].SetActive(false);
        storyPanels[25].SetActive(true);
    }

    public void Story4E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We’re going to head back to the fort and defend the area until we hear otherwise.";
        story = 4.4;
        storyPanels[25].SetActive(false);
        storyPanels[26].SetActive(true);
    }

    public void Story5A()
    {
        storyText.text = "Runner: Message for you sir!";
        story = 5.0;
        storyPanels[26].SetActive(false);
        storyPanels[27].SetActive(true);
    }

    public void Story5B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Looks like we're to hold the area until more senior officers arrive. Good to know " +
            "we're doing what we should be.";
        story = 5.1;
        storyPanels[27].SetActive(false);
        storyPanels[28].SetActive(true);
    }

    public void Story5C()
    {
        storyText.text = "Lieutenant Vafer: What do they mean by the area? We could head out and keep the farmsteads clear or just protect the fort itself. " +
            "I'm pretty sure giving up land to the enemy is frowned upon but it might be hard to protect everywhere at once.";
        story = 5.2;
        storyPanels[28].SetActive(false);
        storyPanels[29].SetActive(true);
    }

    public void Story5D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Our main duty is to protect our people. We'll hold the fort. If there are consequences for " +
            "that decision, so be it.";
        story = 5.3;
        storyPanels[29].SetActive(false);
        storyPanels[30].SetActive(true);
    }

    public void Story5E()
    {
        storyText.text = "Rebel: I may have lost but you can't hold forever! You have no idea what you are up against.";
        story = 5.4;
        storyPanels[30].SetActive(false);
        storyPanels[31].SetActive(true);
    }

    public void Story5F()
    {
        storyText.text = "Sergeant Ferox: Well, that's disturbing.";
        story = 5.5;
        storyPanels[31].SetActive(false);
        storyPanels[32].SetActive(true);
    }

    public void Story5G()
    {
        storyText.text = "Lieutenant Vafer: Good call on staying put. No way the fort would have held without us.";
        story = 5.6;
        storyPanels[32].SetActive(false);
        storyPanels[33].SetActive(true);
    }

    public void Story6A()
    {
        storyText.text = "Lieutenant Vafer: Looks like that dead rebel was right. Lots of bad guys are coming.";
        story = 6.0;
        storyPanels[33].SetActive(false);
        storyPanels[34].SetActive(true);
    }

    public void Story6B()
    {
        storyText.text = "Sergeant Ferox: The troops are ready. Bring them on!";
        story = 6.1;
        storyPanels[34].SetActive(false);
        storyPanels[35].SetActive(true);
    }

    public void Story6C()
    {
        storyText.text = "Sergeant Ferox: Sergeant Ferox: Captain! News is spreading all over the town. East Boarsville has been taken over by rebels.";
        story = 6.2;
        storyPanels[35].SetActive(false);
        storyPanels[36].SetActive(true);
    }

    public void Story6D()
    {
        storyText.text = "Lieutenant Vafer: What should we do? We can't let a rebellion stand but someone needs to hold the fort here.";
        story = 6.3;
        storyPanels[36].SetActive(false);
        storyPanels[37].SetActive(true);
    }

    public void Story6E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Lieutenant, you are in charge of the fort defenses. Take whatever soldiers you need. " +
            "Any other officers ready for a promotion?";
        story = 6.4;
        storyPanels[37].SetActive(false);
        storyPanels[38].SetActive(true);
    }

    public void Story6F()
    {
        storyText.text = "Lieutenant Vafer: Well, Second-Lieutenant Vappa is the next in the chain of command....";
        story = 6.5;
        storyPanels[38].SetActive(false);
        storyPanels[39].SetActive(true);
    }

    public void Story6G()
    {
        storyText.text = "Sergeant Ferox: Right. Perfect. Lieutenant Vappa. Wonderful.";
        story = 6.6;
        storyPanels[39].SetActive(false);
        storyPanels[40].SetActive(true);
    }

    public void Story7A()
    {
        storyText.text = "Sergeant Ferox: Ok we've got a lot of land to cover. We'll need to make sure nobody gets back to " +
            "the town with news of our arrival or we'll lose the element of surprise.";
        story = 7.0;
        storyPanels[40].SetActive(false);
        storyPanels[41].SetActive(true);
    }

    public void Story7B()
    {
        storyText.text = "Lieutenant Vappa: Surprise? We don't need surprise for this rabble. If they hear we're coming they'll run off in fear. These " +
            "are just peasants with pitchforks who don't appreciate everything Deductivus has given them.";
        story = 7.1;
        storyPanels[41].SetActive(false);
        storyPanels[42].SetActive(true);
    }

    public void Story7C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Right. Perfect. Wonderful. We're going to go ahead and take Sergeant Ferox's " +
            "advice here. Lieutenant, you are going to stay with me this battle.";
        story = 7.2;
        storyPanels[42].SetActive(false);
        storyPanels[43].SetActive(true);
    }

    public void Story7D()
    {
        storyText.text = "Rebel: You can't keep us down forever! People will only let you push them so far!";
        story = 7.3;
        storyPanels[43].SetActive(false);
        storyPanels[44].SetActive(true);
    }

    public void Story7E()
    {
        storyText.text = "Lieutenant Vappa: Don't expect us to give you handouts so you can be lazy.";
        story = 7.4;
        storyPanels[44].SetActive(false);
        storyPanels[45].SetActive(true);
    }

    public void Story7F()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": ....";
        story = 7.5;
        storyPanels[45].SetActive(false);
        storyPanels[46].SetActive(true);
    }

    public void Story7G()
    {
        storyText.text = "Sergeant Ferox: The rebellion is apparently taking over the entire south. I think we should keep moving west and clearing.";
        story = 7.6;
        storyPanels[46].SetActive(false);
        storyPanels[47].SetActive(true);
    }

    public void Story7H()
    {
        storyText.text = "Lieutenant Vappa: You think these peons could take Newton? Oh Sergeant, that's adorable.";
        story = 7.7;
        storyPanels[47].SetActive(false);
        storyPanels[48].SetActive(true);
    }

    public void Story7I()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Well, we're going to go take a look. If the Sergeant's right I'll expect you " +
            "to apologize for the condescension.";
        story = 7.8;
        storyPanels[48].SetActive(false);
        storyPanels[49].SetActive(true);
    }

    public void Story7J()
    {
        storyText.text = "Lieutenant Vappa: Hahaha I appreciate your wit Captain.";
        story = 7.9;
        storyPanels[49].SetActive(false);
        storyPanels[50].SetActive(true);
    }

    public void Story7K()
    {
        storyText.text = "Sergeant Ferox sighs...";
        story = 7.01;
        storyPanels[50].SetActive(false);
        storyPanels[51].SetActive(true);
    }

    public void Story8A()
    {
        storyText.text = "Sergeant Ferox: Looks like the rumors were true. All of the Deductivus flags are gone.";
        story = 8.0;
        storyPanels[51].SetActive(false);
        storyPanels[52].SetActive(true);
    }

    public void Story8B()
    {
        storyText.text = "Lieutenant Vappa: This is an outrage! We need to make an example of these rebels.";
        story = 8.1;
        storyPanels[52].SetActive(false);
        storyPanels[53].SetActive(true);
    }

    public void Story8C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We'll take the town back. I'll lead the assault. Ferox, you are in charge of the" +
            " camp in my absence. Vappa, I want you to observe Ferox for this battle. When I come back I want to hear what you learned.";
        story = 8.2;
        storyPanels[53].SetActive(false);
        storyPanels[54].SetActive(true);
    }

    public void Story8D()
    {
        storyText.text = "Lieutenant Vappa: Sir, I'm an officer I should be in charge!";
        story = 8.3;
        storyPanels[54].SetActive(false);
        storyPanels[55].SetActive(true);
    }

    public void Story8E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Sergeant Ferox has decades of experience in the field. Do you believe " +
            "there is nothing you can learn from him?";
        story = 8.4;
        storyPanels[55].SetActive(false);
        storyPanels[56].SetActive(true);
    }

    public void Story8F()
    {
        storyText.text = "Lieutenant Vappa: Regardless, I will look like a fool!";
        story = 8.5;
        storyPanels[56].SetActive(false);
        storyPanels[57].SetActive(true);
    }

    public void Story8G()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": No, you'll look wise enough to want to avoid being found a fool when " +
            "you are actually in charge.";
        story = 8.6;
        storyPanels[57].SetActive(false);
        storyPanels[58].SetActive(true);
    }

    public void Story8H()
    {
        storyText.text = "Rebel: Where did you come from? Were were told there wasn't any military presence to the east....";
        story = 8.7;
        storyPanels[58].SetActive(false);
        storyPanels[59].SetActive(true);
    }

    public void Story8I()
    {
        storyText.text = "Lieutenant Vappa: Sir, you can't just leave this town standing. These people neither fought nor fled when the rebels took over the town.";
        story = 8.8;
        storyPanels[59].SetActive(false);
        storyPanels[60].SetActive(true);
    }

    public void Story8J()
    {
        storyText.text = "Sergeant Ferox: As much as I hate to say it, if you don't make an example of some of these people you may have more explaining to do when we get back to the General.";
        story = 8.9;
        storyPanels[60].SetActive(false);
        storyPanels[61].SetActive(true);
    }

    public void Story8K()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I'll deal with that when I get there. Out job is to deal with military threats. " +
            "Let's keep moving. Ferox, send scouts up to Anville, Dyers, Pleasanton and all the way back to Vanedon to see what we are dealing with.";
        ///8.10 Treated the same as 8.1!!
        story = 8.01;
        storyPanels[61].SetActive(false);
        storyPanels[62].SetActive(true);
    }

    public void Story9A()
    {
        storyText.text = "Sergeant Ferox: As the scouts said, everything east of Vanedon has gone over to the rebels.";
        story = 9.0;
        storyPanels[62].SetActive(false);
        storyPanels[63].SetActive(true);
    }

    public void Story9B()
    {
        storyText.text = "Lieutenant Vappa: Sir, I request the honor of leading the assault this time."; 
        story = 9.1;
        storyPanels[63].SetActive(false);
        storyPanels[64].SetActive(true);
    }

    public void Story9C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I appreciate the dedication but that's my responsibility. You can stick right with me " +
            "for this battle.";
        story = 9.2;
        storyPanels[64].SetActive(false);
        storyPanels[65].SetActive(true);
    }

    public void Story9D()
    {
        storyText.text = "Lieutenant Vappa: Very good sir. Let's set this town straight.";
        story = 9.3;
        storyPanels[65].SetActive(false);
        storyPanels[66].SetActive(true);
    }

    public void Story9E()
    {
        storyText.text = "Rebel: Why are you supporting this corrupt empire? They tax us to the point of breaking our backs. We'd rather die as free men than suffer under your reign of terror.";
        story = 9.4;
        storyPanels[66].SetActive(false);
        storyPanels[67].SetActive(true);
    }

    public void Story9F()
    {
        storyText.text = "Lieutenant Vappa: It's your duty to support our empire!";
        story = 9.5;
        storyPanels[67].SetActive(false);
        storyPanels[68].SetActive(true);
    }

    public void Story9G()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": How bad could things be for the rebellion to have spread so widely?";
        story = 9.6;
        storyPanels[68].SetActive(false);
        storyPanels[69].SetActive(true);
    }

    public void Story9H()
    {
        storyText.text = "Sergeant Ferox: To be honest it's not that great out amongst the people. Taxes are high and people are being pressed into " +
            "the military. They are hungry and not seeing the benefits of all their sacrifices.";
        story = 9.7;
        storyPanels[69].SetActive(false);
        storyPanels[70].SetActive(true);
    }

    public void Story10A()
    {
        storyText.text = "Lieutenant Vappa: Another set of ants to crush beneath our boots.";
        story = 10.0;
        storyPanels[70].SetActive(false);
        storyPanels[71].SetActive(true);
    }

    public void Story10B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's do our duty and get moving.";
        story = 10.1;
        storyPanels[71].SetActive(false);
        storyPanels[72].SetActive(true);
    }

    public void Story10C()
    {
        storyText.text = "Lieutenant Vappa: You don't seem excited sir. We have an opportunity to strike another blow for Deductivus!";
        story = 10.2;
        storyPanels[72].SetActive(false);
        storyPanels[73].SetActive(true);
    }

    public void Story10D()
    {
        storyText.text = "Rebel: I have failed....";
        story = 10.3;
        storyPanels[73].SetActive(false);
        storyPanels[74].SetActive(true);
    }

    public void Story11A()
    {
        storyText.text = "Sergeant Ferox: Last town we have to deal with before we're back in Vanedon sir.";
        story = 11.0;
        storyPanels[74].SetActive(false);
        storyPanels[75].SetActive(true);
    }

    public void Story11B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Alright. Get the men in position.";
        story = 11.1;
        storyPanels[75].SetActive(false);
        storyPanels[76].SetActive(true);
    }

    public void Story11C()
    {
        storyText.text = "Rebel: Free at last....";
        story = 11.2;
        storyPanels[76].SetActive(false);
        storyPanels[77].SetActive(true);
    }

    public void InterludeOneA()
    {
        storyText.text = "General Imperius: Young " + ArmyData.Instance.GetHeroName() + "! You've made quite a name for yourself. Holding the eastern frontier and then putting down " +
            "rebellions across the country! The people are already turning the man into a myth.";
        story = 31.0;
        storyPanels[77].SetActive(false);
        storyPanels[78].SetActive(true);
    }

    public void InterludeOneB()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Thank you sir.";
        story = 31.1;
        storyPanels[78].SetActive(false);
        storyPanels[79].SetActive(true);
    }

    public void InterludeOneC()
    {
        storyText.text = "General Imperius: As you have been acting on your own a promotion is clearly in order. You are now the youngest" +
            " colonel in our great nation's history. Do you have any recommendations for additional promotions?";
        story = 31.2;
        storyPanels[79].SetActive(false);
        storyPanels[195].SetActive(true);
    }

    public void InterludeOneD()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I am honored sir. I left Lieutenant Vafer in charge at Fort Spear. He is eminently " +
            "capable. Additionally, Sergeant Ferox has made our success possible. I have leaned on his experience and insight the entire way.";
        story = 31.3;
        storyPanels[195].SetActive(false);
        storyPanels[80].SetActive(true);
    }

    public void InterludeOneE()
    {
        storyText.text = "General Imperius: Vafer is it? He'll be made a captain immediately. Ferox has already reached the peak of advancement for those " +
            "without noble blood. However, given the extraodinary accomplishments of your company we will make an exception just this once. Maybe it will help calm all " +
            "those peasants down. Get some rest, you deserve it.";
        story = 31.4;
        storyPanels[80].SetActive(false);
        storyPanels[81].SetActive(true);
    }

    public void InterludeOneF()
    {
        storyText.text = "Lieutenant Vappa: Sir, I believe I have information you need to know.";
        story = 31.5;
        storyPanels[81].SetActive(false);
        storyPanels[82].SetActive(true);
    }

    public void InterludeOneG()
    {
        storyText.text = "General Imperius: And who are you again?";
        story = 31.6;
        storyPanels[82].SetActive(false);
        storyPanels[83].SetActive(true);
    }

    public void InterludeOneH()
    {
        storyText.text = "Lieutenant Vappa: Lieutenant Vappa sir. Colonel " + ArmyData.Instance.GetHeroName() + "'s seconnd in command.";
        story = 31.7;
        storyPanels[83].SetActive(false);
        storyPanels[84].SetActive(true);
    }

    public void InterludeOneI()
    {
        storyText.text = "General Imperius: Interesting that he didn't mention you amongst those deserving of a promotion.";
        story = 31.8;
        storyPanels[84].SetActive(false);
        storyPanels[85].SetActive(true);
    }

    public void InterludeOneJ()
    {
        storyText.text = "Lieutenant Vappa: I believe that's because I was pushing him to treat the rebels with the strong arm that they deserved. " +
            "Both Colonel " + ArmyData.Instance.GetHeroName() + " and Ferox sympathized with the rebels and took every opportunity to show mercy. None of the" +
            " civilians in the cities to the east were punished for supporting rebellion.";
        story = 31.9;
        storyPanels[85].SetActive(false);
        storyPanels[86].SetActive(true);
    }

    public void InterludeOneK()
    {
        storyText.text = "General Imperius: Those are serious allegations. You suggest that the hero who destroyed the eastern wing of the rebellion " +
            "is actually an ally of the rebels?";
        story = 31.01;
        storyPanels[86].SetActive(false);
        storyPanels[87].SetActive(true);
    }

    public void InterludeOneL()
    {
        storyText.text = "Lieutenant Vappa: I wouldn't say all exactly sir. He is just soft on them. I believe if you send units to the east you'll notice a lot of " +
            "rebel sympathizers continuing to prosper.";
        story = 31.11;
        storyPanels[87].SetActive(false);
        storyPanels[88].SetActive(true);
    }

    public void InterludeOneM()
    {
        storyText.text = "General Imperius: Alright Lieutenant, I'll look into it. Your battalion will be sent back into the field soon and I want to hear more " +
            "concerns when you return. It wouldn't do to have our young hero foment rebellion.";
        story = 31.12;
        storyPanels[88].SetActive(false);
        storyPanels[89].SetActive(true);
    }

    public void InterludeOneN()
    {
        storyText.text = "General Imperius: Colonel " + ArmyData.Instance.GetHeroName() + ", now that you have subdued the east I'm going to send you on a different type of mission. There continue " +
            "to be rumors of strange monsters to the north. Head up through Forest Edge and check in on our northern forts. Exterminate any threats.";
        story = 31.13;
        storyPanels[89].SetActive(false);
        storyPanels[90].SetActive(true);
    }

    public void InterludeOneO()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Thank you sir. We will leave first thing in the morning.";
        story = 31.14;
        storyPanels[90].SetActive(false);
        storyPanels[91].SetActive(true);
        GlobalMapController.Instance.storyACompleted = true;
    }

    public void Story12A()
    {
        storyText.text = "Lieutenant Ferox: Sir, it looks like these are the beasts we saw that trounced Colonel Ignavum's battalion. I recommend " +
            "extreme caution.";
        story = 12.0;
        storyPanels[91].SetActive(false);
        storyPanels[92].SetActive(true);
    }

    public void Story12B()
    {
        storyText.text = "Lieutenant Vappa: They are beasts Lieutenant. Be not afraid.";
        story = 12.1;
        storyPanels[92].SetActive(false);
        storyPanels[93].SetActive(true);
    }

    public void Story12C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We'll move carefully. We've seen the devastation these monsters can cause.";
        story = 12.2;
        storyPanels[93].SetActive(false);
        storyPanels[94].SetActive(true);
    }

    public void Story12D()
    {
        storyText.text = "Lieutenant Vappa: What WERE those things.";
        story = 12.3;
        storyPanels[94].SetActive(false);
        storyPanels[95].SetActive(true);
    }

    public void Story13A()
    {
        storyText.text = "Lieutenant Ferox: Nobody's here.";
        story = 13.0;
        storyPanels[95].SetActive(false);
        storyPanels[96].SetActive(true);
    }

    public void Story13B()
    {
        storyText.text = "Lieutenant Vappa: Where's our army? Maybe they're out hunting down monsters?";
        story = 13.1;
        storyPanels[96].SetActive(false);
        storyPanels[97].SetActive(true);
    }

    public void Story13C()
    {
        storyText.text = "Lieutenant Ferox: They would have left a force behind to defend the fort. I think we might be alone out here.";
        story = 13.2;
        storyPanels[97].SetActive(false);
        storyPanels[98].SetActive(true);
    }

    public void Story13D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We don't have the manpower to hold this fort. We'll have to report what we found and let someone else " +
            "repair and protect the area.";
        story = 13.3;
        storyPanels[98].SetActive(false);
        storyPanels[99].SetActive(true);
    }

    public void Story13E()
    {
        storyText.text = "Lieutenant Ferox: Hopefully Fort Oak has fared better than Redwood did.";
        story = 13.4;
        storyPanels[99].SetActive(false);
        storyPanels[100].SetActive(true);
    }

    public void Story14A()
    {
        storyText.text = "Lieutenant Vappa: This is like a nightmare. Everyone's gone again.";
        story = 14.0;
        storyPanels[100].SetActive(false);
        storyPanels[101].SetActive(true);
    }

    public void Story14B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's clear the area and get back to Portsmouth.";
        story = 14.1;
        storyPanels[101].SetActive(false);
        storyPanels[102].SetActive(true);
    }

    public void Story14C()
    {
        storyText.text = "Lieutenant Vappa: I can't wait to get back across the river and fighting humans again.";
        story = 14.2;
        storyPanels[102].SetActive(false);
        storyPanels[103].SetActive(true);
    }

    public void Story14D()
    {
        storyText.text =  "Lieutenant Ferox: Well the good news is the area is secure. The bad news is that we are still missing a scout and the others all " +
            "report that it is creepy as hell out there.";
        story = 14.3;
        storyPanels[103].SetActive(false);
        storyPanels[104].SetActive(true);
    }

    public void Story14E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Get everyone moving right now. I don't want to spend a second more than we have to " +
            "out here.";
        story = 14.4;
        storyPanels[104].SetActive(false);
        storyPanels[105].SetActive(true);
    }

    public void Story15A()
    {
        storyText.text = "Lieutenant Vappa: I can't believe how far these monsters have spread.";
        story = 15.0;
        storyPanels[105].SetActive(false);
        storyPanels[106].SetActive(true);
    }

    public void Story15B()
    {
        storyText.text = "Lieutenant Ferox: If they're taking out villages on this side of the river they could be moving towards Vanedon itself.";
        story = 15.1;
        storyPanels[106].SetActive(false);
        storyPanels[107].SetActive(true);
    }

    public void Story15C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's do our part to clear the area and then let the capitol know what we're dealing with.";
        story = 15.2;
        storyPanels[107].SetActive(false);
        storyPanels[108].SetActive(true);
    }

    public void Story15D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I'd love to pretend that this is the last one of those things we'll deal with. Time to report " +
            "back to Vanedon.";
        story = 15.3;
        storyPanels[108].SetActive(false);
        storyPanels[109].SetActive(true);
    }

    public void InterludeTwoA()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": General, we found that the rumors were not only true but possibly understated. We ran into " +
            "unusual beasts starting at Forest's Edge. Forts Redwood and Oak are both emptied and mostly destroyed, Portsmouth is not much better.";
        story = 32.0;
        storyPanels[109].SetActive(false);
        storyPanels[110].SetActive(true);
    }

    public void InterludeTwoB()
    {
        storyText.text = "General Imperius: So in addition to the rebellion, we are dealing with an unknown force invading our lands from the north. That might be part of why the " +
            "Malum Empire has broken our peace accords. We're now dealing with a war on all fronts. Colonel, I'll discuss plans with our war council and give you new orders shortly.";
        story = 32.1;
        storyPanels[110].SetActive(false);
        storyPanels[111].SetActive(true);
    }

    public void InterludeTwoC()
    {
        storyText.text = "General Imperius: Any additional concerns Lieutenant?";
        story = 32.2;
        storyPanels[111].SetActive(false);
        storyPanels[112].SetActive(true);
    }

    public void InterludeTwoD()
    {
        storyText.text = "Lieutenant Vappa: No sir, things awere as Colonel " + ArmyData.Instance.GetHeroName() + " stated. It was a nightmare out there.";
        story = 32.3;
        storyPanels[112].SetActive(false);
        storyPanels[113].SetActive(true);
    }

    public void InterludeTwoE()
    {
        storyText.text = "General Imperius: Well at least that's one less concern. Dismissed.";
        story = 32.4;
        storyPanels[113].SetActive(false);
        storyPanels[114].SetActive(true);
    }

    public void InterludeTwoF()
    {
        storyText.text = "General Imperius: Our most urgent threat at this point is the Malus Empire. Your mission is to sneak into the Empire and strike at their " +
            "capitol while their armies are massed at our border. You have exceeded my expectations at every turn, find a way to do so again.";
        story = 32.5;
        storyPanels[114].SetActive(false);
        storyPanels[115].SetActive(true);
    }

    public void InterludeTwoG()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I will do my best sir.";
        story = 32.6;
        storyPanels[115].SetActive(false);
        storyPanels[116].SetActive(true);
        GlobalMapController.Instance.storyBCompleted = true;
    }

    public void Story16A()
    {
        storyText.text = "Lieutenant Vappa: Is this really the best way through? There is no glory in tromping through swamps and jungles.";
        story = 16.0;
        storyPanels[116].SetActive(false);
        storyPanels[117].SetActive(true);
    }

    public void Story16B()
    {
        storyText.text = "Lieutenant Ferox: The work comes first, the glory comes later.";
        story = 16.1;
        storyPanels[117].SetActive(false);
        storyPanels[118].SetActive(true);
    }

    public void Story16C()
    {
        storyText.text = "Lieutenant Ferox: Well that was unexpected. I can't wait to get through this mess.";
        story = 16.2;
        storyPanels[118].SetActive(false);
        storyPanels[119].SetActive(true);
    }

    public void Story16D()
    {
        storyText.text = "Lieutenant Vappa: For once, Ferox, we agree.";
        story = 16.3;
        storyPanels[119].SetActive(false);
        storyPanels[120].SetActive(true);
    }

    public void Story17A()
    {
        storyText.text = "Lieutenant Ferox: What is this place?";
        story = 17.0;
        storyPanels[120].SetActive(false);
        storyPanels[121].SetActive(true);
    }

    public void Story17B()
    {
        storyText.text = "Lieutenant Vappa: As creepy as it is here, at least we're out of the woods. Hopefully that means " +
            "we are done with all the beasts and monsters?";
        story = 17.1;
        storyPanels[121].SetActive(false);
        storyPanels[122].SetActive(true);
    }

    public void Story17C()
    {
        storyText.text = "Lieutenant Vappa: Ferox, how much work until we get to that glory you spke of? This is the worst.";
        story = 17.2;
        storyPanels[122].SetActive(false);
        storyPanels[123].SetActive(true);
    }

    public void Story18A()
    {
        storyText.text = "Lieutenant Vappa: Finally, civilization! Maybe they'll welcome us with open arms.";
        story = 18.0;
        storyPanels[123].SetActive(false);
        storyPanels[124].SetActive(true);
    }

    public void Story18B()
    {
        storyText.text = "Lieutenant Ferox: I'm going to send the scouts out just in case they don't.";
        story = 18.1;
        storyPanels[124].SetActive(false);
        storyPanels[125].SetActive(true);
    }

    public void Story18C()
    {
        storyText.text = "Malum warrior: Where did you even come from?";
        story = 18.2;
        storyPanels[125].SetActive(false);
        storyPanels[126].SetActive(true);
    }

    public void Story18D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We've got surprise on our side and we're deep in Malus territory. Unless you guys want to make another " +
            "run through the Kuric Woods we need to make sure we don't get spotted.";
        story = 18.3;
        storyPanels[126].SetActive(false);
        storyPanels[127].SetActive(true);
    }

    public void Story19A()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Vappa, lead a company around to the south and make sure you pick up any runners trying to bring word " +
            "of our approach to Hyozura. If anyone gets through this has all been a waste of time. I'm counting on you. Ferox, get the men ready for an assault.";
        story = 19.0;
        storyPanels[127].SetActive(false);
        storyPanels[128].SetActive(true);
    }

    public void Story19B()
    {
        storyText.text = "Lieutenant Vappa: Colonel, I won't let you down sir!";
        story = 19.1;
        storyPanels[128].SetActive(false);
        storyPanels[129].SetActive(true);
    }

    public void Story19C()
    {
        storyText.text = "Malum warrior: The Malus Empire can't be defeated!";
        story = 19.2;
        storyPanels[129].SetActive(false);
        storyPanels[130].SetActive(true);
    }

    public void Story19D()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Well done team. On to the capitol.";
        story = 19.3;
        storyPanels[130].SetActive(false);
        storyPanels[131].SetActive(true);
    }

    public void Story20A()
    {
        storyText.text = "Lieutenant Ferox: Vappa, here's the glory you've been waiting for. The Malus Capitol is as undefended as it's ever been with " +
            "their army moving into our country. However, we are still outnumbered and in a weaker position. I'm not sure this siege is even possible.";
        story = 20.0;
        storyPanels[131].SetActive(false);
        storyPanels[132].SetActive(true);
    }

    public void Story20B()
    {
        storyText.text = "Lieutenant Vappa: Our battalion has crushed rebellions, fought monsters and struck blows against the Malus Empire already. " +
            "There is no battalion that has accomplished more since our nation's founding. If anyone can figure out how to win this fight it is " +
            "Colonel " + ArmyData.Instance.GetHeroName();
        story = 20.1;
        storyPanels[132].SetActive(false);
        storyPanels[133].SetActive(true);
    }

    public void Story20C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Who are you and what have you done with Lieutenant Vappa? Alright, here's the plan...";
        story = 20.2;
        storyPanels[133].SetActive(false);
        storyPanels[134].SetActive(true);
    }

    public void Story20D()
    {
        storyText.text = "Malum warrior: What? We lost? How...";
        story = 20.3;
        storyPanels[134].SetActive(false);
        storyPanels[135].SetActive(true);
    }

    public void Story20E()
    {
        storyText.text = "Lieutenant Vappa: I'm so happy I could cry. We will be legends in Deductivus forever."; 
        story = 20.4;
        storyPanels[135].SetActive(false);
        storyPanels[136].SetActive(true);
    }

    public void Story20F()
    {
        storyText.text = "Lieutenant Ferox: The story only gets told if we make it back home. We're still on the wrong side of the Malum army.";
        story = 20.5;
        storyPanels[136].SetActive(false);
        storyPanels[137].SetActive(true);
    }

    public void Story20G()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's move slowly and carefully. They'll know the land much better than we will " +
            "and we need to avoid an ambush. Send two runners back to Vanedon so General Imperius hears the news.";
        story = 20.6;
        storyPanels[137].SetActive(false);
        storyPanels[138].SetActive(true);
    }

    public void Story21A()
    {
        storyText.text = "Lieutenant Ferox: It's pretty clear they know we're coming. We're all out of surprises. Looks like this will be an " +
            "old fashioned head banger.";
        story = 21.0;
        storyPanels[138].SetActive(false);
        storyPanels[139].SetActive(true);
    }

    public void Story21B()
    {
        storyText.text = "Lieutenant Vappa: We're unstoppable.";
        story = 21.1;
        storyPanels[139].SetActive(false);
        storyPanels[140].SetActive(true);
    }

    public void Story21C()
    {
        storyText.text = "Malum warrior: Your luck will run out eventually!";
        story = 21.2;
        storyPanels[140].SetActive(false);
        storyPanels[141].SetActive(true);
    }

    public void Story21D()
    {
        storyText.text = "Lieutenant Ferox: That was a hard one. We are running low on men and everyone's exhausted. We need to rest.";
        story = 21.3;
        storyPanels[141].SetActive(false);
        storyPanels[142].SetActive(true);
    }

    public void Story21E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's spend a night or two here and then move on. Send out patrols to make sure we don't get stuck here.";
        story = 21.4;
        storyPanels[142].SetActive(false);
        storyPanels[143].SetActive(true);
    }

    public void Story22A()
    {
        storyText.text = "Lieutenant Ferox: Thankfully Hamza's not as fortified as Akurio. We are running low on men though, we can't take too many casualties if we " +
            "still want to call ourselves a battalion.";
        story = 22.0;
        storyPanels[143].SetActive(false);
        storyPanels[144].SetActive(true);
    }

    public void Story22B()
    {
        storyText.text = "Malum warrior: I can't have lost...";
        story = 22.1;
        storyPanels[144].SetActive(false);
        storyPanels[145].SetActive(true);
    }

    public void Story22C()
    {
        storyText.text = "Lieutenant Vappa: Colonel, we are just about out of steam. I know our men aren't weak but even they have breaking points. " +
            "Can we avoid any more fights?";
        story = 22.2;
        storyPanels[145].SetActive(false);
        storyPanels[146].SetActive(true);
    }

    public void Story22D()
    {
        storyText.text = "Lieutenant Ferox: Unfortunately not. The Malum army seems to be mostly camped down at the Foothill but they have patrols wandering all around Fort Stone. " +
            "We could skip Kuvara and push directly for the fort but if we get caught we'll be stuck in the middle of two forces.";
        story = 22.3;
        storyPanels[146].SetActive(false);
        storyPanels[147].SetActive(true);
    }

    public void Story22E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We'll spend a night before moving on to Kuvara.";
        story = 22.4;
        storyPanels[147].SetActive(false);
        storyPanels[148].SetActive(true);
    }

    public void Story23A()
    {
        storyText.text = "Lieutenant Ferox: It seems pretty undefended. I wonder if they just abandoned the town?";
        story = 23.0;
        storyPanels[148].SetActive(false);
        storyPanels[149].SetActive(true);
    }

    public void Story23B()
    {
        storyText.text = "Lieutenant Vappa: Oh no. I recognize those tracks. Looks like more monsters!";
        story = 23.1;
        storyPanels[149].SetActive(false);
        storyPanels[150].SetActive(true);
    }

    public void Story23C()
    {
        storyText.text = ArmyData.Instance.GetHeroName() +  ": We'll make a run for it. Hopefully the monsters are fighting the Malum warriors.";
        story = 23.2;
        storyPanels[150].SetActive(false);
        storyPanels[151].SetActive(true);
    }

    public void Story23D()
    {
        storyText.text = "Lieutenant Vappa: What are you standing around for? Keep running!";
        story = 23.3;
        storyPanels[151].SetActive(false);
        storyPanels[152].SetActive(true);
    }

    public void Story24A()
    {
        storyText.text = "Lieutenant Ferox: Looks like the defenders and the monsters are still working together.";
        story = 24.0;
        storyPanels[152].SetActive(false);
        storyPanels[153].SetActive(true);
    }

    public void Story24B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We're almost home. If we get through here we can meet up with our army in Foothill.";
        story = 24.1;
        storyPanels[153].SetActive(false);
        storyPanels[154].SetActive(true);
    }

    public void Story24C()
    {
        storyText.text = "Lieutenant Ferox: Apparently we don't get to rest. We just received a runner requesting that we participate in a joint attack " +
            "with the main Deductivus and rebel armies who have joined together against the Malus empire. While they draw the attention of their main " +
            "forces we are supposed to clear Foothill and disrupt their supply lines.";
        story = 24.2;
        storyPanels[154].SetActive(false);
        storyPanels[155].SetActive(true);
    }

    public void Story25A()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Let's finish this.";
        story = 25.0;
        storyPanels[155].SetActive(false);
        storyPanels[156].SetActive(true);
    }

    public void Story25B()
    {
        storyText.text = "Lieutenant Vappa: Yes, I'm ready to lounge around Vanedon and be celebrated as a hero for the rest of my life.";
        story = 25.1;
        storyPanels[156].SetActive(false);
        storyPanels[157].SetActive(true);
    }

    public void Story25C()
    {
        storyText.text = "Lieutenant Vappa: Colonel, we did it! We single-handedly cleared the Malus Empire and were even a major part of destroying their mighty army. I " +
            "am so proud to have been a part of your command.";
        story = 25.2;
        storyPanels[157].SetActive(false);
        storyPanels[158].SetActive(true);
    }

    public void Story25D()
    {
        storyText.text = "General Imperiues: Colonel " + ArmyData.Instance.GetHeroName() + "! Congratulations on your victories.";
        story = 25.3;
        storyPanels[158].SetActive(false);
        storyPanels[159].SetActive(true);
    }

    public void Story25E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Thank you General. Congratulations on finding peace with the rebels and your triumph over the Malum army.";
        story = 25.4;
        storyPanels[159].SetActive(false);
        storyPanels[160].SetActive(true);
    }

    public void Story25F()
    {
        storyText.text = "General Imperius: Thank you. Although, we have a truce, not a peace. Now that the Malus Empire has been dealt with we just need to remove the threat " +
            "of the rebels and we'll be able to move to the north to handle whatever threat is coming.";
        story = 25.5;
        storyPanels[160].SetActive(false);
        storyPanels[161].SetActive(true);
    }

    public void Story25G()
    {
        storyText.text = "General Imperius: We are going to hit the rebel army tonight while they are still celebrating and solve two of our problems in this one outing. Take an hour " +
            "and get your troops ready.";
        story = 25.6;
        storyPanels[161].SetActive(false);
        storyPanels[162].SetActive(true);
    }

    public void Story25H()
    {
        storyText.text = "Lieutenant Ferox: I don't think I can do this Colonel.";
        story = 25.7;
        storyPanels[162].SetActive(false);
        storyPanels[163].SetActive(true);
    }

    public void Story25I()
    {
        storyText.text = "Lieutenant Vappa: We've been given a direct order. The rebels are traitors and need to be dealt with. It's not our place to question the general.";
        story = 25.8;
        storyPanels[163].SetActive(false);
        storyPanels[164].SetActive(true);
    }

    public void Story25J()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": Ferox is right Vappa. The rebels didn't need to help protect us against the Malus Empire but they put our nation ahead " +
            "of their own interests. We can't reward that with betrayal.";
        story = 25.9;
        storyPanels[164].SetActive(false);
        storyPanels[165].SetActive(true);
    }

    public void Story25K()
    {
        storyText.text = "General Imperius: Do I need to worry about our dear Colonel?";
        story = 25.01;
        storyPanels[165].SetActive(false);
        storyPanels[166].SetActive(true);
    }

    public void Story25L()
    {
        storyText.text = "Lieutenant Vappa: General, he is certainly not happy about breaking a truce. However, the Colonel is a war hero. He'll do what needs to be done.";
        story = 25.11;
        storyPanels[166].SetActive(false);
        storyPanels[167].SetActive(true);
    }

    public void Story26A()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I know none of us like this situation. If either of you needs to stay back or resign from my command I " +
            "understand but need to know now.";
        story = 26.0;
        storyPanels[167].SetActive(false);
        storyPanels[168].SetActive(true);
    }

    public void Story26B()
    {
        storyText.text = "Lieutenant Ferox: I'm with you sir.";
        story = 26.1;
        storyPanels[168].SetActive(false);
        storyPanels[169].SetActive(true);
    }

    public void Story26C()
    {
        storyText.text = "Lieutenant Vappa: Same. We've come this far together.";
        story = 26.2;
        storyPanels[169].SetActive(false);
        storyPanels[170].SetActive(true);
    }

    public void Story26D()
    {
        storyText.text = "General Imperiues: Traitors! Your names will not even be remembered. We will destroy you and erase all mention " +
            "of you. Nobody will even know you existed.";
        story = 26.3;
        storyPanels[170].SetActive(false);
        storyPanels[171].SetActive(true);
    }

    public void Story26E()
    {
        storyText.text = "Lieutenant Vappa: Haven't you noticed General? We can't be stopped. We are not afraid.";
        story = 26.4;
        storyPanels[171].SetActive(false);
        storyPanels[172].SetActive(true);
    }

    public void Story26F()
    {
        storyText.text = "Lieutenant Ferox: I like the new Vappa a lot more than the old one.";
        story = 26.5;
        storyPanels[172].SetActive(false);
        storyPanels[173].SetActive(true);
    }

    public void Story27A()
    {
        storyText.text = "Lieutenant Ferox: It's only a token force. Our army is retreating towards Vanedon and has only left enough to slow us down.";
        story = 27.0;
        storyPanels[173].SetActive(false);
        storyPanels[174].SetActive(true);
    }

    public void Story27B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": I'm not happy about fighting our own soldiers but we must capture the General if we want to avoid being " +
            "hunted for the rest of our lives.";
        story = 27.1;
        storyPanels[174].SetActive(false);
        storyPanels[175].SetActive(true);
    }

    public void Story27C()
    {
        storyText.text = "Deductivian warrior: I survived the eastern wilds just to go down like this?";
        story = 27.2;
        storyPanels[175].SetActive(false);
        storyPanels[176].SetActive(true);
    }

    public void Story27D()
    {
        storyText.text = "Lieutenant Ferox: The General has moved towards Cunningfield, the rebel's main base.";
        story = 27.3;
        storyPanels[176].SetActive(false);
        storyPanels[177].SetActive(true);
    }

    public void Story27E()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": If we can catch him in Cunningfield we won't have to fight through Vanedon. Let's move.";
        story = 27.4;
        storyPanels[177].SetActive(false);
        storyPanels[178].SetActive(true);
    }

    public void Story28A()
    {
        storyText.text = "General Imperius: Still trying to protect these rebels? I figured you would've run away by now.";
        story = 28.0;
        storyPanels[178].SetActive(false);
        storyPanels[179].SetActive(true);
    }

    public void Story28B()
    {
        storyText.text = "Lieutenant Vappa: Have you not followed our progress this year? We don't run away from anything.";
        story = 28.1;
        storyPanels[179].SetActive(false);
        storyPanels[180].SetActive(true);
    }

    public void Story28C()
    {
        storyText.text = "General Imperius: Blast, I'll deal with these rebels another time. Ignavum, hold " + ArmyData.Instance.GetHeroName() + " here while I get reinforcements from Vanedon.";
        story = 28.2;
        storyPanels[180].SetActive(false);
        storyPanels[181].SetActive(true);
    }

    public void Story29A()
    {
        storyText.text = "Colonel Ignavum: " + ArmyData.Instance.GetHeroName() + ", I have no idea how you've survived for so long but it'll be my pleasure to solve that problem.";
        story = 29.0;
        storyPanels[181].SetActive(false);
        storyPanels[182].SetActive(true);
    }

    public void Story29B()
    {
        storyText.text = "Lieutenant Ferox: The last time we saw you it was when you were running screaming into the woods.";
        story = 29.1;
        storyPanels[182].SetActive(false);
        storyPanels[183].SetActive(true);
    }

    public void Story29C()
    {
        storyText.text = "Colonel Ignavum: After I kill you I'm going to turn you into slippers for that comment.";
        story = 29.2;
        storyPanels[183].SetActive(false);
        storyPanels[184].SetActive(true);
    }

    public void Story29D()
    {
        storyText.text = "Ignavum: What? No, I don't understand...";
        story = 29.3;
        storyPanels[184].SetActive(false);
        storyPanels[185].SetActive(true);
    }

    public void Story29E()
    {
        storyText.text = "Lieutenant Ferox: If we want to deal with the General we'll need to march into Vanedon. Are we sure we want to invade our own " +
            "capitol?";
        story = 29.4;
        storyPanels[185].SetActive(false);
        storyPanels[186].SetActive(true);
    }

    public void Story29F()
    {
        storyText.text = "Lieutenant Vappa: It's not our capitol while we are declared traitors. I want my glory back.";
        story = 29.5;
        storyPanels[186].SetActive(false);
        storyPanels[187].SetActive(true);
    }

    public void Story29G()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We need to put down the General and push for negotiations between the nobility and the rebels.";
        story = 29.6;
        storyPanels[187].SetActive(false);
        storyPanels[188].SetActive(true);
    }

    public void Story30A()
    {
        storyText.text = "General Imperius: You attack your own capitol?";
        story = 30.0;
        storyPanels[188].SetActive(false);
        storyPanels[189].SetActive(true);
    }

    public void Story30B()
    {
        storyText.text = ArmyData.Instance.GetHeroName() + ": We're not here to attack the capitol. We're just here for you.";
        story = 30.1;
        storyPanels[189].SetActive(false);
        storyPanels[190].SetActive(true);
    }

    public void Story30C()
    {
        storyText.text = "Imperius: My empire is crumbling!";
        story = 30.2;
        storyPanels[190].SetActive(false);
        storyPanels[191].SetActive(true);
    }

    public void Story30D()
    {
        storyText.text = "Once General Imperius was defeated, the Deductivus nobility had no choice but to meet with " + ArmyData.Instance.GetHeroName() + " and the rebellion. A new government was established that " +
            "involved representatives from the nobility, the rebellion and the common folk. The remaining military might was put under the authority of the only officer supported by all " +
            "parties, the newly appointed General Vafer.";
        story = 30.3;
        storyPanels[191].SetActive(false);
        storyPanels[192].SetActive(true);
    }

    public void Story30E()
    {
        storyText.text = "Lieutenant Vappa was promoted to Captain Vappa and given the desk job of his dreams. He is frequently found at taverns telling war stories and accepting free drinks.";
        story = 30.4;
        storyPanels[192].SetActive(false);
        storyPanels[193].SetActive(true);
    }

    public void Story30F()
    {
        storyText.text = "Lieutenant Ferox was promoted to Captain as well. He currently assists General " + ArmyData.Instance.GetHeroName() + " in his assault into the northern wilds.";
        story = 30.5;
        storyPanels[193].SetActive(false);
        storyPanels[194].SetActive(true);
    }

    private void HideButtons()
    {
        CanvasController.Instance.globalMenuButton.SetActive(false);
        CanvasController.Instance.battleReadyButton.SetActive(false);
        CanvasController.Instance.battleMenuButton.SetActive(false);
    }

    private void ShowButtons()
    {
        if (GameController.Instance.levelsCompleted > 0)
        {
            CanvasController.Instance.globalMenuButton.SetActive(true);
        }

        CanvasController.Instance.battleReadyButton.SetActive(true);
        CanvasController.Instance.battleMenuButton.SetActive(true);
    }
}
