using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

enum Value
{
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, 
    Jack, Queen, King, Ace
}

enum Color
{
    D, H, C, S
}

class Card: IComparable<Card>
{
    private static Dictionary<string, Value> map = new Dictionary<string, Value> 
    {
        { "2", Value.Two}, { "3", Value.Three}, { "4", Value.Four},{ "5", Value.Five},
        { "6", Value.Six}, { "7", Value.Seven}, { "8", Value.Eight},{ "9", Value.Nine},{ "10", Value.Ten},
        { "J", Value.Jack}, { "Q", Value.Queen}, { "K", Value.King}, { "A", Value.Ace},
    };
    private static Dictionary<string, Color> colorMap = new Dictionary<string, Color>
    {
        {"D", Color.D},
        {"H", Color.H},
        {"C", Color.C},
        {"S", Color.S},
    };

    private readonly Color _color;
    private readonly Value _value;
    private readonly string _txtValue;
    public Card(string txt)
    {
        var length = txt.Length;
        var txtColor = txt.Substring(length - 1, 1);
        _txtValue = txt.Substring(0, length - 1);

        _color = colorMap[txtColor];
        _value = map[_txtValue];
    }

    public int CompareTo(Card otherCard)
    {
        if(this._value < otherCard._value)
            return -1;
        else if(this._value > otherCard._value)
            return 1;
        else
        {
            return 0;
        }
    }

    public override string ToString()
    {
        return $"{_txtValue}{_color}";
    }
}

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{

    static void Debug(string message, List<Card> cards)
    {
        Debug(message + " "  + string.Join(" ", cards.Select(c => c.ToString()).ToArray()));
    }
    

    static void Debug(string message)
    {
        Console.Error.WriteLine(message);
    }

    static void Main(string[] args)
    {
        var player1Cards = new List<Card>();
        var player2Cards = new List<Card>();

        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        for (int i = 0; i < n; i++)
        {
            string cardp1 = Console.ReadLine(); // the n cards of player 1
            player1Cards.Add(new Card(cardp1));
        }
        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        for (int i = 0; i < m; i++)
        {
            string cardp2 = Console.ReadLine(); // the m cards of player 2
            player2Cards.Add(new Card(cardp2));
        }
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Debug("Player1:", player1Cards);
        Debug("Player2:", player2Cards);

        var turns = 0;
        while(player1Cards.Count > 0 && player2Cards.Count > 0)
        {
            turns++;
            
            int cardIndex = 0;

            while(player1Cards[cardIndex].CompareTo( player2Cards[cardIndex]) == 0)
            {
                cardIndex += 4;

                if(cardIndex >= player1Cards.Count || cardIndex >= player2Cards.Count)
                {
                    //Stop the game
                    Console.WriteLine("PAT");
                    return;
                }
            }

            var player1IsWinner = player1Cards[cardIndex].CompareTo(player2Cards[cardIndex]) > 0;
            var winnerList = player1IsWinner ? player1Cards : player2Cards;

            for(int i=0; i <= cardIndex; i++)
            {
                var wonCard = player1Cards[0];
                player1Cards.Remove(wonCard);
                winnerList.Add(wonCard);
            }

            for(int i=0; i <= cardIndex; i++)
            {
                var wonCard = player2Cards[0];
                player2Cards.Remove(wonCard);
                winnerList.Add(wonCard);
            }

            Debug("");
            Debug($"Turns: {turns}");
            Debug("Player1:", player1Cards);
            Debug("Player2:", player2Cards);
        }

        var winner = player1Cards.Count == 0 ? 2 : 1;

        Console.WriteLine($"{winner.ToString()} {turns.ToString()}");
    }
}