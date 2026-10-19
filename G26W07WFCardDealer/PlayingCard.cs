namespace G26W07WFCardDealer {

    public struct PlayingCard {
        public string Suit { get; }
        public string Value { get; }
        public PlayingCard(string suit, string value) {
            Suit = suit;
            Value = value;
        }

        public string ResourceName {
            get {
                string suit = Suit;
                string value = Value;

                if (value == "jack" ||
                    value == "queen" ||
                    value == "king") {
                    suit += "2";
                }

                return $"{value}_of_{suit}";
            }
        }
    }
}