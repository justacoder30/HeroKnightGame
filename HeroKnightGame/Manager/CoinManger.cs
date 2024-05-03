using System.Collections.Generic;

namespace HeroKnightGame.Manager
{
    public class CoinManger
    {
        private List<Coin> coins;
        public CoinManger()
        {
            coins = new List<Coin>();   

            foreach(var _pos in Map.GetCoinPosition())
            {
                Coin coin = new Coin(_pos);

                coins.Add(coin);
            }
        }

        public void Update(Player player)
        {
            for(int i = 0; i < coins.Count; i++)
            {
                coins[i].Update(player);
                if (coins[i].IsRemoved)
                {
                    SoundManager.PlaySound("Coin_sound");
                    Score.IncreaseScore();
                    coins.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw()
        {
            foreach(Coin coin in coins)
            {
                coin.Draw();
            }
        }
    }
}
