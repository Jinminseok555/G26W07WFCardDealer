
using System;
using System.Collections.Generic;

namespace G26W07WFCardDealer {
    public class PokerGame {
        private readonly Random random = new Random();

        // 카드 52장 생성
        private List<PlayingCard> CreateDeck() {
            string[] suits =
            {
                "spades", "hearts", "diamonds", "clubs"
            };

            string[] values =
            {
                "ace", "2", "3", "4", "5", "6", "7",
                "8", "9", "10", "jack", "queen", "king"
            };

            List<PlayingCard> deck = new List<PlayingCard>();

            foreach (string suit in suits) {
                foreach (string value in values) {
                    deck.Add(new PlayingCard(suit, value));
                }
            }

            return deck;
        }

        // 카드 섞기
        private void Shuffle(List<PlayingCard> deck) {
            for (int i = deck.Count - 1; i > 0; i--) {
                int j = random.Next(i + 1);

                PlayingCard temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        // 카드 5장 분배
        public List<PlayingCard> DealHand() {
            List<PlayingCard> deck = CreateDeck();

            Shuffle(deck);

            return deck.GetRange(0, 5);
        }
    }
}
