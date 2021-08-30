using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//便于引用相关的UI组件

public class ItemUI : MonoBehaviour {

    //处理UI变化
    public Item Item { get; private set; }//获取 UI上的物品
    public int Amount { get; private set; }//获取UI上的物品个数

    #region UI Component
    private Image itemImage;
    private Text amountText;

    //把物品图片和个数都包装起来，调用的时候就调用大写字母开头的这个
    private Image ItemImage
    {
        get
        {
            if(itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if(amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }
    #endregion

    private float targetScale = 1f;//目标缩放大小
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);
    private float smooth = 4;//动画平滑过渡时间的系数

    void Update()
    {
        if(transform.localScale.x != targetScale)
        {
            //设置动画 缩放效果
            float scale=Mathf.Lerp(transform.localScale.x, targetScale, smooth * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < .02f)
            {//当大小的差可以忽略的时候，就不再平滑变化了，就直接变成目标大小
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }

    }
    //更新UI的图标显示
    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        // update ui 
        this.ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";

    }
    //添加UI数目
    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        //update ui 
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //减少物品数量
    public bool ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;//物品更新时放大UI，用于动画
        this.Amount -= amount;
        //update ui 
        if (Item.Capacity > 1)//更新UI
        {
            AmountText.text = Amount.ToString();
            return true;
        }
        else
        {
            AmountText.text = "";
            return false;
        }
        
    }
    //设置物品个数
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        //update ui 
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //当前物品 跟 另一个物品 交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.Item;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.Item, this.Amount);
        this.SetItem(itemTemp, amountTemp);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

}
