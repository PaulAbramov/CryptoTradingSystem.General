using System;

namespace CryptoTradingSystem.General.Helper
{
    public class ConsoleHelper
    {
        public static void ClearCurrentConsoleLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);

            for (var i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }

            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}