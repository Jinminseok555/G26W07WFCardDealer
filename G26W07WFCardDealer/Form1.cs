
using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace G26W07WFCardDealer {
    public partial class Form1 : Form {
        private readonly PokerHandEvaluator evaluator
            = new PokerHandEvaluator();

        private readonly PokerGame game = new PokerGame();

        private PictureBox[] cardBoxes;

        public Form1() {
            InitializeComponent();

            cardBoxes = new PictureBox[]
            {
                Card1, Card2, Card3, Card4, Card5
            };
        }

        private void OnDeal(object sender, EventArgs e) {
            // 1. 카드 5장 분배
            List<PlayingCard> cards = game.DealHand();

            // 2. 카드 이미지 표시
            for (int i = 0; i < cards.Count; i++) {
                string resourceName = cards[i].ResourceName;

                Image image = Properties.Resources.ResourceManager
                    .GetObject(resourceName) as Image;

                if (image == null) {
                    MessageBox.Show(
                        $"이미지를 찾을 수 없습니다: {resourceName}");
                    return;
                }

                cardBoxes[i].Image = image;
            }

            // 3. 족보 판별
            PokerHandRank rank = evaluator.Evaluate(cards);

            // 4. 판별 결과 표시
            MessageBox.Show($"포커 족보: {GetRankName(rank)}");
        }

        private void OnSimulate(object sender, EventArgs e) {
            int totalTrials = 10000;

            // 모든 족보의 카운트를 0으로 초기화
            Dictionary<PokerHandRank, int> counts =
                new Dictionary<PokerHandRank, int>();

            foreach (PokerHandRank rank in
                Enum.GetValues(typeof(PokerHandRank))) {
                counts[rank] = 0;
            }

            // 카드 분배와 족보 판별을 반복
            for (int i = 0; i < totalTrials; i++) {
                List<PlayingCard> cards = game.DealHand();

                PokerHandRank rank = evaluator.Evaluate(cards);

                counts[rank]++;
            }

            // 결과 정리
            string result = $"총 실험 횟수: {totalTrials:N0}회\n\n";

            foreach (PokerHandRank rank in
                Enum.GetValues(typeof(PokerHandRank))) {
                int count = counts[rank];
                double probability =
                    (double)count / totalTrials * 100;

                result += $"{GetRankName(rank),-15} " +
                          $"{count,6:N0}회  " +
                          $"{probability,7:F3}%\n";
            }

            MessageBox.Show(
                result,
                "포커 족보 확률 실험 결과");

            // 출현 횟수가 많은 족보부터 정렬
            foreach (var item in counts.OrderByDescending(x => x.Value)) {
                PokerHandRank rank = item.Key;
                int count = item.Value;

                double probability =
                    (double)count / totalTrials * 100;

                result += $"{GetRankName(rank),-15} " +
                          $"{count,6:N0}회  " +
                          $"{probability,7:F3}%\n";
            }
        }

        private string GetRankName(PokerHandRank rank) {
            switch (rank) {
                case PokerHandRank.RoyalFlush:
                    return "로열 플러시";

                case PokerHandRank.StraightFlush:
                    return "스트레이트 플러시";

                case PokerHandRank.FourOfAKind:
                    return "포카드";

                case PokerHandRank.FullHouse:
                    return "풀하우스";

                case PokerHandRank.Flush:
                    return "플러시";

                case PokerHandRank.Straight:
                    return "스트레이트";

                case PokerHandRank.ThreeOfAKind:
                    return "트리플";

                case PokerHandRank.TwoPair:
                    return "투 페어";

                case PokerHandRank.OnePair:
                    return "원 페어";

                default:
                    return "하이 카드";
            }
        }
    }
}
