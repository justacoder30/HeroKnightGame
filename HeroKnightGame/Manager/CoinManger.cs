using System.Collections.Generic;

namespace HeroKnightGame.Manager
{
    public class CoinManger
    {
        private List<Coin> coins;

        //Khoi tao Dong Xu
        public CoinManger()
        {
            coins = new List<Coin>();   

            foreach(var _pos in Map.GetCoinPosition())
            {
                Coin coin = new Coin(_pos);

                coins.Add(coin);
            }
        }

        //Cap nhap Dong xu
        public void Update(Player player)
        {
            //Kiem tra Dong xu co bi nhan vat dung vao khong
            for(int i = 0; i < coins.Count; i++)
            {
                coins[i].Update(player);
                //Neu co thi loai bo khoi phan Display
                if (coins[i].IsRemoved)
                {
                    SoundManager.PlaySound("Coin_sound");
                    Score.IncreaseScore();
                    coins.RemoveAt(i);
                    i--;
                }
            }
        }

        //Ve dong xu len man hinh
        public void Draw()
        {
            foreach(Coin coin in coins)
            {
                coin.Draw();
            }
        }
    }
}
