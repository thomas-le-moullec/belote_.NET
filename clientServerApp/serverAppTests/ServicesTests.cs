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
        Services service = new Services();

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

            Service.DistribCards(player, pick, 3);
            Assert.AreEqual(player.Hand.Count(), 3);
            Assert.AreEqual(pick.Count(), 1);
        }

        [TestMethod()]
        public void TrumpGenerationTest()
        {
            // public void TrumpGeneration(Board board)
            Board board = new Board();
            Card card = new Card();

            card.Type = Card.Types.CLUB;
            card.Val = "7";
            card.Points = 0;
            board.Pick = new List<Card>();
            board.Pick.Add(card);
            Service.TrumpGeneration(board);
            Assert.AreEqual(board.Trump.Type, card.Type);
            Assert.AreEqual(board.Trump.Val, card.Val);
            Assert.AreEqual(board.Trump.Points, card.Points);
            Assert.AreEqual(board.Pick.Count(), 0);
        }

        [TestMethod()]
        public void VerifCardTest()
        {
            Player player = new Player();
            Card card1 = new Card();
            String cardStr;
            String cardStr2;

            cardStr = "Q:HEART";
            cardStr2 = "K:HEART";
            player.Hand.Add(new Card() { Type = Card.Types.HEART, Val = "Q", Points = 0});
            player.Hand.Add(new Card() { Type = Card.Types.CLUB, Val = "K", Points = 0});
            player.Hand.Add(new Card() { Type = Card.Types.CLUB, Val = "10", Points = 0});
            player.Hand.Add(new Card() { Type = Card.Types.SPADE, Val = "As", Points = 0});
            try
            {
                card1 = Service.VerifCard(player, cardStr);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(card1.Type, player.Hand[0].Type);
            Assert.AreEqual(card1.Val, player.Hand[0].Val);
            Assert.AreEqual(card1.Points, player.Hand[0].Points);
            try
            {
                Service.VerifCard(player, cardStr2);
            }
            catch
            {
                return ;
            }
            Assert.Fail();
            return ;
        }

        [TestMethod()]
        public void PutCardTest()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            Board board = new Board();
            String cardStr1;
            String cardStr2;

            cardStr1 = "Q:HEART";
            cardStr2 = "K:CLUB";
            player1.Hand.Add(new Card() { Type = Card.Types.HEART, Val = "Q", Points = 0 });
            player2.Hand.Add(new Card() { Type = Card.Types.CLUB, Val = "K", Points = 0 });
            Service.PutCard(player1, board, cardStr1);
            Service.PutCard(player2, board, cardStr2);
            Assert.AreEqual(board.Fold[0].Type, Card.Types.HEART);
            Assert.AreEqual(board.Fold[0].Val, "Q");
            Assert.AreEqual(board.Fold[1].Type, Card.Types.CLUB);
            Assert.AreEqual(board.Fold[1].Val, "K");
        }

        public Services Service
        {
            get { return service; }
            set { service = value; }
        }
    }
}