using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region basic property
    private int basicStrength = 10;
    //private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;
    private int basicDefense = 10;

    public int BasicStrength
    {
        get
        {
            return basicStrength;
        }
    }
    public int BasicDefense
    {
        get
        {
            return basicDefense;
        }
    }

    //敏捷度
    public int BasicAgility
    {
        get
        {
            return basicAgility;
        }
    }
    //体力
    public int BasicStamina
    {
        get
        {
            return basicStamina;
        }
    }
    //攻击力
    public int BasicDamage
    {
        get
        {
            return basicDamage;
        }
    }
    #endregion

    private int coinAmount = 100000;

    private Text coinText;

    public int CoinAmount
    {
        get
        {
            return coinAmount;
        }
        set
        {
            coinAmount = value;
            coinText.text = coinAmount.ToString();
        }
    }

    void Start()
    {
        coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
        coinText.text = coinAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*//G 随机得到一个物品放到背包里面
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 18);
            Knapsack.Instance.StoreItem(id);
        }

      //T 控制背包的显示和隐藏
        if (Input.GetKeyDown(KeyCode.T))
        {
            Knapsack.Instance.display();
        }*/

        //Y 控制箱子的显示和隐藏
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Chest.Instance.display();
        }
        /*//按下V键控制角色面板的显示和隐藏
        if (Input.GetKeyDown(KeyCode.V))
        {
            Character.Panel.display();
        }*/
        //I 控制商店显示和隐藏 
        if (Input.GetKeyDown(KeyCode.I))
        {
            Vendor.Instance.display();
        }
        System.GC.Collect();
    }


    // 消费

    public bool ConsumeCoin(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            coinText.text = coinAmount.ToString();
            return true;
        }
        return false;
    }


    // 赚取金币

    public void EarnCoin(int amount)
    {
        this.coinAmount += amount;
        coinText.text = coinAmount.ToString();
    }
}
