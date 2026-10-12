
using System;
using System.Collections.Generic;
using System.Linq;

namespace G26W07WFCardDealer {
    public class PokerHandEvaluator {
        public PokerHandRank Evaluate(List<PlayingCard> cards) {
            // 카드가 정확히 5장인지 확인
            if (cards == null || cards.Count != 5) {
                throw new ArgumentException(
                    "포커 족보 판별에는 카드 5장이 필요합니다.");
            }

            // 각 숫자가 몇 장씩 있는지 계산
            List<int> counts = cards
                .GroupBy(card => card.Value)
                .Select(group => group.Count())
                .OrderByDescending(count => count)
                .ToList();

            // 카드 숫자를 숫자 값으로 변환
            List<int> values = cards
                .Select(card => GetCardValue(card.Value))
                .OrderBy(value => value)
                .ToList();

            // 5장의 무늬가 모두 같은지 확인
            bool isFlush = cards
                .All(card => card.Suit == cards[0].Suit);

            // 스트레이트인지 확인
            bool isStraight = IsStraight(values);

            // 1. 로열 플러시
            if (isFlush && values.SequenceEqual(
                new List<int> { 1, 10, 11, 12, 13 })) {
                return PokerHandRank.RoyalFlush;
            }

            // 2. 스트레이트 플러시
            if (isFlush && isStraight) {
                return PokerHandRank.StraightFlush;
            }

            // 3. 포카드
            if (counts[0] == 4) {
                return PokerHandRank.FourOfAKind;
            }

            // 4. 풀하우스
            if (counts[0] == 3 && counts[1] == 2) {
                return PokerHandRank.FullHouse;
            }

            // 5. 플러시
            if (isFlush) {
                return PokerHandRank.Flush;
            }

            // 6. 스트레이트
            if (isStraight) {
                return PokerHandRank.Straight;
            }

            // 7. 트리플
            if (counts[0] == 3) {
                return PokerHandRank.ThreeOfAKind;
            }

            // 8. 투 페어
            if (counts[0] == 2 && counts[1] == 2) {
                return PokerHandRank.TwoPair;
            }

            // 9. 원 페어
            if (counts[0] == 2) {
                return PokerHandRank.OnePair;
            }

            // 10. 하이 카드
            return PokerHandRank.HighCard;
        }

        private int GetCardValue(string value) {
            switch (value) {
                case "ace": return 1;
                case "jack": return 11;
                case "queen": return 12;
                case "king": return 13;
                default:
                    return int.Parse(value);
            }
        }

        private bool IsStraight(List<int> values) {
            // A, 2, 3, 4, 5 예외 처리
            if (values.SequenceEqual(
                new List<int> { 1, 2, 3, 4, 5 })) {
                return true;
            }

            // 중복 숫자가 있으면 스트레이트가 아님
            if (values.Distinct().Count() != 5) {
                return false;
            }

            // 가장 큰 숫자와 가장 작은 숫자의 차이가 4인지 확인
            return values[4] - values[0] == 4;
        }
    }
}
