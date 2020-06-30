using System.Collections;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            // Start a new game with 2 players
            StateManager.NumberOfPlayers = 2;
            SceneManager.LoadScene("Scenes/EspacioJuego");
        }

        // Check player initialization
        [UnityTest]
        public IEnumerator PlayerInitialization()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Assert.IsTrue(stateManager.Players.Count == 2);

            Player currentPlayer = stateManager.CurrentPlayer();
            Assert.IsTrue(currentPlayer.PlayerId == stateManager.CurrentPlayerId && currentPlayer.PlayerId == 0);

            Assert.IsTrue(currentPlayer.Money == 0);
            Assert.IsTrue(currentPlayer.PGS == 0);
            Assert.IsFalse(currentPlayer.LostTurn);
            Assert.IsFalse(currentPlayer.CrossedInitialTile);

            // Boat check
            Assert.IsTrue(currentPlayer.boats.Count == 1);
            GameObject newBoat = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BarcoArtesanal"));
            currentPlayer.AddBoat(newBoat.GetComponent<Boat>());
            Assert.IsTrue(currentPlayer.boats.Count == 2);

            // Card inventory check
            ICard testCard = new PerderDinero(-1, "Prueba");
            Assert.IsTrue(currentPlayer.cards.Count == 0);
            currentPlayer.AddCard(testCard);
            Assert.IsTrue(currentPlayer.cards.Count == 1);
            currentPlayer.RemoveCard(testCard);
            Assert.IsTrue(currentPlayer.cards.Count == 0);
        }

        // Check tile initialization
        [UnityTest]
        public IEnumerator TileInitialization()
        {
            // Wait for scene to load
            yield return null;

            Tile[] allTiles = GameObject.FindObjectsOfType<Tile>();
            Assert.IsTrue(allTiles.Where(tile => tile.isCorner).Count() == 4); // Check there are 4 corners

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Tile initialTile = allTiles.First(tile => tile.isInitialTile);
            Assert.IsTrue(allTiles.Where(tile => tile.isInitialTile).Count() == 1); // Check there is only one initial tile and weve got it saved
            Assert.IsTrue(stateManager.InitialTile == initialTile);
            
            // Check the initial boats are in the initial tile
            Assert.IsTrue(initialTile.trailBoatsOnTile == 0);
            Assert.IsTrue(initialTile.artisanalBoatsOnTile == 2);
            Assert.IsTrue(initialTile.boatsInTile.Count == 2);

            // Check all tiles have their attributes correctly initiated
            Assert.IsTrue(allTiles.All(tile => !tile.overexploited));
            Assert.IsTrue(allTiles.All(tile => !tile.furtives));
            Assert.IsTrue(allTiles.All(tile => tile.resources > 0));
            Assert.IsTrue(allTiles.All(tile => tile.nextTile != null));
            Assert.IsTrue(allTiles.All(tile => tile.previousTile != null));

            // Overexploit check
            Assert.IsTrue(initialTile.GetResources() > 0);
            initialTile.MarkAsOverexploited();
            Assert.IsTrue(initialTile.GetResources() == 0);
            initialTile.overexploited = false;

            // Furtives check
            Assert.IsTrue(initialTile.GetResources() > 0);
            initialTile.furtives = true;
            Assert.IsTrue(initialTile.GetResources() == 0);
            initialTile.furtives = false;

            // Boat check
            Assert.IsFalse(initialTile.BoatsOfBothTypesOnTile());
            Boat trailBoat = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BarcoArrastre").GetComponent<Boat>());
            trailBoat.currentTile = initialTile;
            trailBoat.ArriveAtTile();
            Assert.IsTrue(initialTile.BoatsOfBothTypesOnTile());
        }

        // Check boat initialization
        [UnityTest]
        public IEnumerator BoatInitialization()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();

            Boat[] allBoats = GameObject.FindObjectsOfType<Boat>();
            Assert.IsTrue(allBoats.Count() == 2); // Check there are 2 boats initially
            Assert.IsTrue(allBoats.All(boat => boat.currentTile == stateManager.InitialTile)); // Check theyre both at initial tile

            // Check resource recolection
            Assert.IsTrue(allBoats.All(boat => boat.Owner.Money == 0));
            foreach(Boat boat in allBoats)
            {
                boat.CollectResources();
            }
            Assert.IsTrue(allBoats.All(boat => boat.Owner.Money == stateManager.InitialTile.GetResources()));

            // Check boat arrival/departure from tiles
            Assert.IsFalse(stateManager.InitialTile.BoatsOfBothTypesOnTile());
            Assert.IsTrue(stateManager.InitialTile.artisanalBoatsOnTile == 2);
            Assert.IsTrue(stateManager.InitialTile.boatsInTile.Count == 2);
            Assert.IsTrue(stateManager.InitialTile.trailBoatsOnTile == 0);

            foreach (Boat boat in allBoats)
            {
                boat.LeaveTile();
            }
            Assert.IsFalse(stateManager.InitialTile.BoatsOfBothTypesOnTile());
            Assert.IsTrue(stateManager.InitialTile.artisanalBoatsOnTile == 0);
            Assert.IsTrue(stateManager.InitialTile.boatsInTile.Count == 0);
            Assert.IsTrue(stateManager.InitialTile.trailBoatsOnTile == 0);

            foreach (Boat boat in allBoats)
            {
                boat.ArriveAtTile();
            }
            Assert.IsFalse(stateManager.InitialTile.BoatsOfBothTypesOnTile());
            Assert.IsTrue(stateManager.InitialTile.artisanalBoatsOnTile == 2);
            Assert.IsTrue(stateManager.InitialTile.boatsInTile.Count == 2);
            Assert.IsTrue(stateManager.InitialTile.trailBoatsOnTile == 0);
        }

        // Check dice is able to roll
        [UnityTest]
        public IEnumerator DiceCheck()
        {
            // Wait for scene to load
            yield return null;

            // Roll the dice once
            DiceRoller diceRoller = GameObject.FindObjectOfType<DiceRoller>();
            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            diceRoller.Roll();
            yield return new WaitForSeconds(6);

            // Check the result is collected correctly
            Assert.IsTrue(stateManager.LastRollResult >= 1);
        }

        // Move player boat
        [UnityTest]
        public IEnumerator MovePlayerBoat()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();

            // Get current player boat
            Boat currentBoat = GameObject.FindObjectsOfType<Boat>().First(boat => boat.Owner == stateManager.CurrentPlayer());
            // Check theyre at initial tile
            Assert.IsTrue(currentBoat.currentTile == stateManager.InitialTile);
            // fake roll
            stateManager.LastRollResult = 1;
            stateManager.currentState = WaitingForClick.GetInstance();
            // Move boat
            stateManager.currentState.BoatOnClick(stateManager, null, currentBoat);
            // Check that were moving
            Assert.IsTrue(currentBoat.isAnimating);
            Assert.IsTrue(stateManager.currentState == WaitingForAnimation.GetInstance());
            Assert.IsTrue(currentBoat.currentTile == stateManager.InitialTile.nextTile);
            Assert.IsTrue(currentBoat.pendingMovements.Count > 0);
        }

        // Check you can only move your boats
        [UnityTest]
        public IEnumerator CantMovePlayerBoatIfNotYours()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            // Get other player boat
            Boat notMyBoat = GameObject.FindObjectsOfType<Boat>().First(boat => boat.Owner != stateManager.CurrentPlayer());
            // Check were at initial tile
            Assert.IsTrue(notMyBoat.currentTile == stateManager.InitialTile);
            // Fake roll
            stateManager.LastRollResult = 1;
            stateManager.currentState = WaitingForClick.GetInstance();
            // We cant move
            stateManager.currentState.BoatOnClick(stateManager, null, notMyBoat);
            Assert.IsFalse(notMyBoat.isAnimating);
            Assert.IsFalse(stateManager.currentState == WaitingForAnimation.GetInstance());
            Assert.IsFalse(notMyBoat.currentTile == stateManager.InitialTile.nextTile);
            Assert.IsFalse(notMyBoat.pendingMovements.Count > 0);
        }

        // Check turn system
        [UnityTest]
        public IEnumerator TurnSystemWorks()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            // Check were player 1
            Assert.IsTrue(stateManager.CurrentPlayerId == 0);
            stateManager.NewTurn();
            // Check were player 2
            Assert.IsTrue(stateManager.CurrentPlayerId == 1);
            stateManager.NewTurn();
            // Check were player 1
            Assert.IsTrue(stateManager.CurrentPlayerId == 0);
            // Make player 2 lose its turn
            stateManager.Players[1].LostTurn = true;
            stateManager.NewTurn();
            // Check were player 1
            Assert.IsTrue(stateManager.CurrentPlayerId == 0);
        }

        // Passive card with adyacent bonus
        [UnityTest]
        public IEnumerator PassiveCard()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Tile tile = stateManager.InitialTile;
            // Check our tile has no card
            Assert.IsTrue(tile.passiveCard == null);

            // New card, doesnt affect adyacent tiles
            PassiveCard testCard = new PassiveCard(1, 2, "OilSpill");

            Assert.IsFalse(testCard.affectsAdyacent);
            Assert.IsTrue(testCard.CardName() == "OilSpill");
            Assert.IsTrue(testCard.PlayableInTile(tile));
            Assert.IsFalse(testCard.PlayableInPlayer(stateManager.CurrentPlayer()));
            
            // Check the card affects resource collection in the tile
            Assert.IsTrue(tile.GetResources() == tile.resources);

            testCard.PlayCard(stateManager.CurrentPlayer(), tile);

            Assert.IsTrue(stateManager.CurrentPlayer().PGS == 1);
            Assert.IsTrue(tile.GetResources() == tile.resources * testCard.resourceMultiplier);
        }

        // Passive card with no adyacent bonus
        [UnityTest]
        public IEnumerator PassiveCardWithAdyacentBonus()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Tile tile = stateManager.InitialTile;
            // Check our tile has no card
            Assert.IsTrue(tile.passiveCard == null);

            // New card, it affects adyacent tiles
            PassiveCard testCard = new PassiveCard(1, 2, "OilSpill", 0);

            Assert.IsTrue(testCard.affectsAdyacent);
            Assert.IsTrue(testCard.CardName() == "OilSpill");
            Assert.IsTrue(testCard.PlayableInTile(tile));
            Assert.IsFalse(testCard.PlayableInPlayer(stateManager.CurrentPlayer()));

            Assert.IsTrue(tile.GetResources() == tile.resources);
            Assert.IsTrue(tile.nextTile.passiveCard == null);
            Assert.IsTrue(tile.previousTile.passiveCard == null);

            // Check the card affects resource collection in the tile and the adyacent tiles
            testCard.PlayCard(stateManager.CurrentPlayer(), tile);

            Assert.IsTrue(stateManager.CurrentPlayer().PGS == 1);
            Assert.IsTrue(tile.GetResources() == tile.resources * testCard.resourceMultiplier);
            Assert.IsTrue(tile.nextTile.GetResources() == 0);
            Assert.IsTrue(tile.nextTile.passiveCard != null && tile.nextTile.passiveCard.isCloneFromAdyacent);
            Assert.IsTrue(tile.previousTile.GetResources() == 0);
            Assert.IsTrue(tile.previousTile.passiveCard != null && tile.nextTile.passiveCard.isCloneFromAdyacent);
        }

        // Check card is given on 6
        [UnityTest]
        public IEnumerator CardIsGivenWhenRolled6()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            // Get current player boat
            Boat currentBoat = GameObject.FindObjectsOfType<Boat>().First(boat => boat.Owner == stateManager.CurrentPlayer());
            // Fake roll
            stateManager.LastRollResult = 6;
            stateManager.currentState = WaitingForClick.GetInstance();

            // Check we receive a card, whatever card
            Assert.IsTrue(stateManager.CurrentPlayer().cards.Count == 0);
            stateManager.currentState.BoatOnClick(stateManager, null, currentBoat);
            Assert.IsTrue(stateManager.CurrentPlayer().cards.Count == 1);
        }

        // Check you can buy cards
        [UnityTest]
        public IEnumerator CardShopping()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            CardManager cardManager = GameObject.FindObjectOfType<CardManager>();
            stateManager.OpenCardsMenu(true);
            stateManager.CurrentPlayer().Money = Definitions.PRECIO_COMPRA_CARTAS;

            GameObject.FindObjectOfType<CardMenu>().BuyCard();
            GameObject.FindObjectOfType<ConfirmDialog>().Accept();

            Assert.IsTrue(stateManager.CurrentPlayer().Money == 0);
            Assert.IsTrue(stateManager.CurrentPlayer().cards.Count == 1);
        }

        // Check furtives move on 1
        [UnityTest]
        public IEnumerator FurtiveBoatMoves()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            // Get current player boat
            Boat currentBoat = GameObject.FindObjectsOfType<Boat>().First(boat => boat.Owner == stateManager.CurrentPlayer());
            // Fake roll
            stateManager.LastRollResult = 1;
            stateManager.currentState = WaitingForClick.GetInstance();

            // Check the furtives have moved to our tile
            Assert.IsFalse(currentBoat.currentTile.nextTile.furtives);
            stateManager.currentState.BoatOnClick(stateManager, null, currentBoat);
            Assert.IsTrue(currentBoat.currentTile.furtives);
        }

        // Eliminar vertido
        [UnityTest]
        public IEnumerator EliminarVertido()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Tile tile = stateManager.InitialTile;
            EliminarVertido cartaPrueba = new EliminarVertido(2, "EliminarVertido");
            Assert.IsTrue(tile.passiveCard == null);
            Assert.IsFalse(cartaPrueba.PlayableInTile(tile));
            PassiveCard vertido = new PassiveCard(1, 2, "OilSpill");
            vertido.PlayCard(stateManager.CurrentPlayer(), tile);
            Assert.IsTrue(tile.passiveCard == vertido);

            Assert.IsTrue(cartaPrueba.PlayableInTile(tile));
            Assert.IsFalse(cartaPrueba.PlayableInPlayer(stateManager.CurrentPlayer()));
            cartaPrueba.PlayCard(stateManager.CurrentPlayer(), tile);
            Assert.IsTrue(tile.passiveCard == null);
            Assert.IsFalse(cartaPrueba.PlayableInTile(tile));
        }

        // Mover furtivos
        [UnityTest]
        public IEnumerator MoverFurtivos()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            Furtivos cartaPrueba = new Furtivos(0, "Furtivos");
            Assert.IsFalse(stateManager.InitialTile.furtives);
            Assert.IsTrue(cartaPrueba.PlayableInTile(stateManager.InitialTile));
            Assert.IsFalse(cartaPrueba.PlayableInPlayer(stateManager.CurrentPlayer()));
            cartaPrueba.PlayCard(stateManager.CurrentPlayer(), stateManager.InitialTile);
            Assert.IsTrue(stateManager.InitialTile.furtives);
            Assert.IsFalse(cartaPrueba.PlayableInTile(stateManager.InitialTile));
        }

        // Perder dinero
        [UnityTest]
        public IEnumerator PerderDinero()
        {
            // Wait for scene to load
            yield return null;
            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();

            Assert.IsTrue(stateManager.Players[1].Money == 0);

            PerderDinero cartaPrueba = new PerderDinero(0, "Multa");

            Assert.IsFalse(cartaPrueba.PlayableInTile(stateManager.InitialTile));

            Assert.IsFalse(cartaPrueba.PlayableInPlayer(stateManager.Players[1]));
            stateManager.Players[1].Money = 1;
            Assert.IsTrue(cartaPrueba.PlayableInPlayer(stateManager.Players[1]));

            cartaPrueba.PlayCard(stateManager.Players[1], null);

            Assert.IsTrue(stateManager.Players[1].Money == 0);

            stateManager.Players[1].Money = Definitions.CANTIDAD_A_PERDER_MULTA;
            cartaPrueba.PlayCard(stateManager.Players[1], null);
            Assert.IsTrue(stateManager.Players[1].Money == 0);
        }

        // Perder turno
        [UnityTest]
        public IEnumerator PerderTurno()
        {
            // Wait for scene to load
            yield return null;
            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();

            Assert.IsTrue(stateManager.CurrentPlayerId == 0);
            Assert.IsFalse(stateManager.Players[1].LostTurn);

            PerderTurno cartaPrueba = new PerderTurno(0, "Ciclogenesis");
            Assert.IsTrue(cartaPrueba.PlayableInPlayer(stateManager.Players[1]));
            Assert.IsFalse(cartaPrueba.PlayableInTile(stateManager.InitialTile));
            
            cartaPrueba.PlayCard(stateManager.Players[1], null);

            Assert.IsTrue(stateManager.Players[1].LostTurn);
            stateManager.NewTurn();
            Assert.IsTrue(stateManager.CurrentPlayerId == 0);
        }

        // Check you can buy boats
        [UnityTest]
        public IEnumerator BoatShopping()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();
            stateManager.CurrentPlayer().Money = Definitions.PRECIO_COMPRA_ARTESANAL;

            GameObject.FindObjectOfType<BoatShop>().BuyArtisanalBoat();
            GameObject.FindObjectOfType<ConfirmDialog>().Accept();

            Assert.IsTrue(stateManager.CurrentPlayer().Money == 0);
            Assert.IsTrue(stateManager.CurrentPlayer().boats.Count == 2);

            stateManager.CurrentPlayer().Money = Definitions.PRECIO_COMPRA_ARRASTRE;
            GameObject.FindObjectOfType<BoatShop>().BuyTrailBoat();
            GameObject.FindObjectOfType<ConfirmDialog>().Accept();

            Assert.IsTrue(stateManager.CurrentPlayer().Money == 0);
            Assert.IsTrue(stateManager.CurrentPlayer().boats.Count == 3);
            Assert.IsTrue(stateManager.CurrentPlayer().boats.Where(boat => boat.boatType == Boat.BoatType.TRAIL).Count() == 1);
            Assert.IsTrue(stateManager.CurrentPlayer().boats.Where(boat => boat.boatType == Boat.BoatType.ARTISANAL).Count() == 2);
        }

        // Check trail boats overexploit tiles
        [UnityTest]
        public IEnumerator TrailBoatOverexploitsTiles()
        {
            // Wait for scene to load
            yield return null;

            StateManager stateManager = GameObject.FindObjectOfType<StateManager>();

            Assert.IsFalse(stateManager.InitialTile.overexploited);
            Assert.IsFalse(stateManager.InitialTile.BoatsOfBothTypesOnTile());
            Assert.IsTrue(stateManager.InitialTile.trailBoatsOnTile == 0);

            Boat trailBoat = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BarcoArrastre").GetComponent<Boat>());
            stateManager.CurrentPlayer().AddBoat(trailBoat);
            trailBoat.currentTile = stateManager.InitialTile;
            trailBoat.ArriveAtTile();

            Assert.IsTrue(stateManager.CurrentPlayer().Money == 0);
            trailBoat.CollectResources();
            Assert.IsTrue(stateManager.CurrentPlayer().Money == stateManager.InitialTile.resources * 5);
            Assert.IsTrue(stateManager.InitialTile.overexploited);
            Assert.IsTrue(stateManager.InitialTile.BoatsOfBothTypesOnTile());
            Assert.IsTrue(stateManager.InitialTile.trailBoatsOnTile == 1);
        }
    }
}
