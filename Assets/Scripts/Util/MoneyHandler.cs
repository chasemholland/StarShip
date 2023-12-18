using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles money conversion to reasonable length
/// </summary>
public static class MoneyHandler
{
    // string to hold abreviated money
    static string moneyString;

    /// <summary>
    /// Abreviates a given amount of money
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public static string ConvertMoney(float money)
    {
        if (money < 1000)
        {
            moneyString = money.ToString();
        }
        else if (money >= 1000 && money < 1000000)
        {
            moneyString = MathF.Round((money / 1000), 2).ToString() + AbreviationName.K.ToString();
        }
        else if (money >= 1000000 && money < 1000000000)
        {
            moneyString = MathF.Round((money / 1000000), 2).ToString() + AbreviationName.M.ToString();
        }
        else if (money >= 1000000000 && money < 1000000000000)
        {
            moneyString = MathF.Round((money / 1000000000), 2).ToString() + AbreviationName.B.ToString();
        }
        else if (money >= 1000000000000 && money < 1000000000000000)
        {
            moneyString = MathF.Round((money / 1000000000000), 2).ToString() + AbreviationName.T.ToString();
        }
        else if (money >= 1000000000000000 && money < 1000000000000000000)
        {
            moneyString = MathF.Round((money / 1000000000000000), 2).ToString() + AbreviationName.QD.ToString();
        }
        else if (money >= 1000000000000000 && money < 1000000000000000000000f)
        {
            moneyString = MathF.Round((money / 1000000000000000000), 2).ToString() + AbreviationName.QT.ToString();
        }
        else if (money >= 1000000000000000000 && money < 1000000000000000000000000f)
        {
            moneyString = MathF.Round((money / 1000000000000000000000f), 2).ToString() + AbreviationName.ST.ToString();
        }
        else if (money >= 1000000000000000000000f && money < 1000000000000000000000000000f)
        {
            moneyString = MathF.Round((money / 1000000000000000000000000f), 2).ToString() + AbreviationName.SP.ToString();
        }
        else if (money >= 1000000000000000000000000f && money < 1000000000000000000000000000000f)
        {
            moneyString = MathF.Round((money / 1000000000000000000000000000f), 2).ToString() + AbreviationName.OT.ToString();
        }
        else
        {
            moneyString = "NULL";
        }

        return moneyString;
    }
}
