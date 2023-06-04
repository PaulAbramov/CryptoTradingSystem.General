using System;
using System.Collections;
using System.Text;

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
        
        public static void HandleArrowKey(ConsoleKey key, ICollection? entries, ref int cursorPosition)
        {
            if (entries == null)
            {
                return;
            }
            
            Console.WriteLine($"index: {cursorPosition}");
            cursorPosition = key switch
            {
                ConsoleKey.UpArrow => Math.Max(cursorPosition - 1, 0),
                ConsoleKey.DownArrow => Math.Min(cursorPosition + 1, entries.Count - 1),
                _ => cursorPosition
            };
            Console.WriteLine($"new index: {cursorPosition}");

        }
    }
}