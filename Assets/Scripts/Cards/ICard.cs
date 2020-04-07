using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    void PlayCard(Player player, Tile tile);
    void DrawCard();
    string CardName();
}
