using System.Collections;
using System.Collections.Generic;

public interface ICard
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    void PlayCard(Player player, Tile tile);
    string CardName();
    bool PlayableInTile(Tile targetTile);
    bool PlayableInPlayer(Player targetPlayer);
    bool MarkedForSelling();
    void MarkForSell(bool mark);
}
