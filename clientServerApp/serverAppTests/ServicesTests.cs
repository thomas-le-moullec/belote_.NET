using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using serverApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverApp.Tests
{
    [TestClass()]
    public class ServicesTests
    {
       // Services service = new Services();

        [TestMethod()]
        public void ServicesTest()
        {
           //Assert.Fail();
        }

        [TestMethod()]
        public void DistribCardsTest()
        {
            Player player = new Player();
            List<Card> pick = new List<Card>();

            player.Hand = new List<Card>();
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "7", Points = 0 });
            pick.Add(new Card() { Type = Card.Types.SPADE, Val = "Q", Points = 10 });
            pick.Add(new Card() { Type = Card.Types.HEART, Val = "10", Points = 10 });
            pick.Add(new Card() { Type = Card.Types.CLUB, Val = "7", Points = 0 });

            Services.DistribCards(player, pick, 3);
            Assert.AreEqual(player.Hand.Count(), 3);
            Assert.AreEqual(pick.Count(), 1);
        }

        [TestMethod()]
        public void TrumpGenerationTest()
        {
            Board board = new Board();
            Card card = new Card();

            card.Type = Card.Types.CLUB;
            card.Val = "7";
            card.Points = 0;
            board.Pick = new List<Card>();
            board.Pick.Add(card);
            Services.TrumpGeneration(board);
            Assert.AreEqual(board.Trump.Type, card.Type);
            Assert.AreEqual(board.Trump.Val, card.Val);
            Assert.AreEqual(board.Trump.Points, card.Points);
            Assert.AreEqual(board.Pick.Count(), 0);
        }

        [TestMethod()]
        public void PutCardTest()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            Board board = new Board();

            player1.Hand.Add(new Card() { Type = Card.Types.HEART, Val = "Q", Points = 0 });
            player2.Hand.Add(new Card() { Type = Card.Types.CLUB, Val = "K", Points = 0 });
            Services.PutCard(player1, board, new Card() { Type = Card.Types.HEART, Val = "Q", Points = 0 });
            Services.PutCard(player2, board, new Card() { Type = Card.Types.CLUB, Val = "K", Points = 0 });
            Assert.AreEqual(board.Fold[0].Type, Card.Types.HEART);
            Assert.AreEqual(board.Fold[0].Val, "Q");
            Assert.AreEqual(board.Fold[1].Type, Card.Types.CLUB);
            Assert.AreEqual(board.Fold[1].Val, "K");
        }

        [TestMethod()]
        public void DistribTrumpTest()
        {
            Player player = new Player();
            Board board = new Board();

            board.Trump = new Card() { Type = Card.Types.HEART, Val = "Q", Points = 0 };

            Services.DistribTrump(player, board);
            Assert.AreEqual(player.Hand[0].Type, Card.Types.HEART);
            Assert.AreEqual(player.Hand[0].Val, "Q");
        }

        [TestMethod()]
        public void SetCardPointsTest()
        {
            Room room = new Room();

            room.RoomBoard.Trump = new Card { Type = Card.Types.HEART };
            room.Players = new List<Player>();
            room.Players.Add(new Player());
            room.Players[0].Hand.Add(new Card { Type = Card.Types.HEART, Val = "J", Points = 0 });
            room.Players[0].Hand.Add(new Card { Type = Card.Types.CLUB, Val = "Q", Points = 0 });
            room.Players.Add(new Player());
            room.Players[1].Hand.Add(new Card { Type = Card.Types.HEART, Val = "9", Points = 0 });

            Services.SetCardPoints(room);
            Assert.AreEqual(room.Players[0].Hand[0].Points, 20);
            Assert.AreEqual(room.Players[0].Hand[1].Points, 0);
            Assert.AreEqual(room.Players[1].Hand[0].Points, 14);
        }

        [TestMethod()]
        public void WinFoldTest()
        {
            Board board = new Board();
            board.Fold = new List<Card>();

            board.Trump = new Card() { Type = Card.Types.HEART, Val = "8", Points = 0};
            board.Fold.Add(new Card() { Type = Card.Types.CLUB, Val = "J", Points = 2, IdPlayer = 0});
            board.Fold.Add(new Card() { Type = Card.Types.CLUB, Val = "As", Points = 11, IdPlayer = 1 });
            board.Fold.Add(new Card() { Type = Card.Types.HEART, Val = "8", Points = 0, IdPlayer = 2 });
            board.Fold.Add(new Card() { Type = Card.Types.CLUB, Val = "7", Points = 0, IdPlayer = 3 });

            int id = Services.WinFold(board);
            Assert.AreEqual(id, 2);
            /*fold[2].Val = "8";
            fold[2].Points = 0;
            id = Services.WinFold(fold, Card.Types.HEART.GetType());
            Assert.AreEqual(id, 2);
            fold[2].Type = Card.Types.CLUB;
            id = Services.WinFold(fold, Card.Types.HEART.GetType());
            Assert.AreEqual(id, 1);*/
        }
    }
}