
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace G26W07WFCardDealer {
    public partial class Form1 : Form {
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
            List<PlayingCard> cards = game.DealHand();

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
        }
    }
}
