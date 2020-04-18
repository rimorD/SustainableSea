using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Methods ////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Deck = new List<ICard>();
        DiscardPile = new List<ICard>();
        InitializeDeck();
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        
    }
    
    //---------------------------------------------------------------------------------------------

    public ICard DrawCardFromDeck()
    {
        if(Deck.Count <= 0)
        {
            // Were out of cards, take whats on the discard pile
            Deck = DiscardPile;
            // Clear discard pile
            DiscardPile.Clear();
            // Shuffle
            Shuffle(Deck);
        }
        // Return first card of the deck
        ICard drawnCard = Deck[0];
        Deck.RemoveAt(0);

        return drawnCard;
    }

    //---------------------------------------------------------------------------------------------

    public void Discard(ICard discard)
    {
        DiscardPile.Add(discard);
    }

    //---------------------------------------------------------------------------------------------

    public void InitializeDeck()
    { 
        // Read card proportion file
        string[] lines = System.Text.RegularExpressions.Regex.Split(cardCountFile.text.Trim(), "[\n|\r]+");
        foreach (string line in lines)
        {
            string[] values = line.Split(';');

            int numberOfCopies = (int)(double.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture));

            // Generate cards
            for (int i = 0; i < numberOfCopies; i++)
            {
                // Create card and add it to the deck
                ICard createdCard = CreateCard(values[0], int.Parse(values[1]));
                if(createdCard != null)
                    Deck.Add(createdCard);
            }
        }

        Shuffle(Deck);
    }

    //---------------------------------------------------------------------------------------------

    public static ICard CreateCard(string cardClass, int pgs)
    {
        switch (cardClass)
        {
            case "Voluntariado_Ambiental":
                return new EliminarVertido(pgs, "VolAmb");
            case "Area_Marina_Protegida":
                return new PassiveCard(pgs, 0, "MPA", 2);
            case "Vedado":
                return new PassiveCard(pgs, 0, "Vedado");
            case "Estudio_Genetico":
                return new TirarDado(pgs, "PGS");
            case "Vertido_Crudo":
                return new PassiveCard(pgs, 0, "OilSpill", 0);
            case "Afloramiento_Nutrientes":
                return new PassiveCard(pgs, 2, "Upwelling");
            case "Furtivos":
                return new Furtivos(pgs, "Furtivos");
            case "Fallo_Motor":
                return new PerderTurno(pgs, "FalloMotor");
            case "Emporio_Comercial":
                return new PassiveCard(pgs, 2, "Emporio");
            case "Ciclogenesis_Explosiva":
                return new PerderTurno(pgs, "Ciclogenesis");
            case "Aparejos_Ilegales":
                return new PerderDinero(pgs, "Aparejos");
            default:
                return null;
        }
    }

    //---------------------------------------------------------------------------------------------

    private void Shuffle(List<ICard> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            ICard value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public List<ICard> Deck;
    public List<ICard> DiscardPile;
    public TextAsset cardCountFile;

    public ICard cardPlayed;
}
