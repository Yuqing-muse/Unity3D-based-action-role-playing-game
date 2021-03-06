using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


//关于存货的总管理类
public class InventoryManager : MonoBehaviour
{


    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //下面的代码只会执行一次
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    /// 物品信息的列表（集合）

    private List<Item> itemList;//存储json解析出来的物品列表

    #region ToolTip
    private ToolTip toolTip;//获取ToolTip脚本，方便对其管理

    private bool isToolTipShow = false;//提示框是否在显示状态

    private Vector2 toolTipPosionOffset = new Vector2(10, -10);//设置提示框跟随时与鼠标的偏移
    #endregion

    private Canvas canvas;

    #region PickedItem
    private bool isPickedItem = false;//鼠标是否选中该物品

    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
    }

    private ItemUI pickedItem; //鼠标选中的物品的脚本组件，用于制作拖动功能 

    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }
    #endregion

    void Awake()
    {
        ParseItemJson();//解析物品文件
    }

    void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("backpack").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();//开始为隐藏状态
    }

    void Update()
    {
        if (isPickedItem)
        {
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            Vector2 position;
            //如果RectTransform的平面被命中，则返回true，无论该点是否在矩形内。
            //将屏幕空间点转换为RectTransform的局部空间中位于其矩形平面上的位置。
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }
        else if (isToolTipShow)//控制提示框跟随鼠标移动
        {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocalPotion(position + toolTipPosionOffset);//设置提示框的位置，二维坐标会自动转化为三维坐标但Z坐标为0
        }

        //物品丢弃的处理
        if (isPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }


    // 解析物品信息

    void ParseItemJson()
    {
        itemList = new List<Item>();
        //文本在unity里面是TextAsset类型
        TextAsset itemText = Resources.Load<TextAsset>("Items");//加载json文本
        string itemJson = itemText.text;//得到json文本里面的内容
        //bug.Log(itemJson);
        JSONObject j = new JSONObject(itemJson);
        foreach (var temp in j.list)
        {
            string typeStr = temp["type"].str;
            //物品类型字符串转化为枚举类型
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);
            
            //print(type);
            //下面解析的是物品的共有属性：id，name，quality。。。注：temp["id"].n 的 .n，.str等是json插件的写法，用于解析
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)(temp["capacity"].n);
            int buyPrice = (int)(temp["buyPrice"].n);
            int sellPrice = (int)(temp["sellPrice"].n);
            string sprite = temp["sprite"].str;
           Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = (int)(temp["hp"].n);
                    int mp = (int)(temp["mp"].n);
                    item = new Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case Item.ItemType.Equipment:
                    int strength = (int)(temp["strength"].n);
                    int defense = (int)(temp["defense"].n);
                    int agility = (int)(temp["agility"].n);
                    int stamina = (int)(temp["stamina"].n);
                    Equipment.EquipmentType equiType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].str);
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, defense, agility, stamina, equiType);
                    break;
                case Item.ItemType.Weapon:
                    int damage = (int)(temp["damage"].n);
                    Weapon.WeaponType weaponType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["weaponType"].str);
                    item = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, weaponType);
                    break;
                default:
                    break;
            }
            itemList.Add(item);//把解析到的物品信息加入物品列表里面
            //Debug.Log(item);
        }
    }
    //得到根据 id 得到Item
    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    //显示提示框方法
    public void ShowToolTip(string content)
    {
        if (this.isPickedItem) return;
        isToolTipShow = true;
        toolTip.Show(content);
    }
    //隐藏提示框方法
    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();
    }

    //获取（拾取）物品槽里的指定数量的（amount）物品UI
    public void PickupItem(Item item, int amount)
    {
        PickedItem.SetItem(item, amount);
        isPickedItem = true;

        PickedItem.Show();//获取到物品之后把跟随鼠标的容器（用来盛放捡起的物品的容器）显示出来
        this.toolTip.Hide();
        //如果我们捡起了物品，我们就要让物品跟随鼠标
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        pickedItem.SetLocalPosition(position);//设置容器的位置，二维坐标会自动转化为三维坐标但Z坐标为0
    }


    //从鼠标上减少（移除）指定数量的物品  默认减少一个

    public void RemoveItem(int amount = 1)
    {
        PickedItem.ReduceAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();//如果鼠标上没有物品了那就隐藏了
        }
    }
    //点击保存按钮，保存当前物品信息
    public void SaveInventory()
    {
        Knapsack.Instance.Save();
        Chest.Instance.Save();

        PlayerPrefs.SetInt("CoinAmount", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount);
    }
    //点击加载按钮，加载当前物品
    public void LoadInventory()
    {
        Knapsack.Instance.Load();
        Chest.Instance.Load();
        //加载玩家金币
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CoinAmount = PlayerPrefs.GetInt("CoinAmount");
        }
    }

}

