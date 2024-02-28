
internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new HeroKnightGame.KnightGame();
        game.Run();
    }
}