namespace SnakeExample.Tick
{
    internal class GameData
    {
        public float TickSpeedDivider { get; set; }

        public GameData()
        {
            TickSpeedDivider = 1.0f;
        }

        public void Restart()
        {
            TickSpeedDivider = 1.0f;
        }
    }
}