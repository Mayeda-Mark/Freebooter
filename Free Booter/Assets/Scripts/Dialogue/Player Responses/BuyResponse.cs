using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response-Buy", menuName = "Response-Buy")]
public class BuyResponse : PlayerResponse
{
    public override void ResponseButton()
    {
        FindObjectOfType<MerchantMenu>().SetUpBuy();
    }
}
