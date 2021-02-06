﻿using System;
using static System.ConsoleColor;
using static Konsole.ControlStatus;

namespace Konsole.Samples
{
    public static class ListViewDemos
    {
        public static void ListViewThemeTest()
        {
            var window = new Window(50, 10, new StyleTheme(Yellow, DarkMagenta).WithTitle(White, Red));
            var incoming = window.SplitLeft("INCOMING");
            var outgoing = window.SplitRight("OUTGOING");

            var view = new ListView<(string Name, int Credits)>(incoming,
                () => new[] { ("Graham", 100), ("Kendall", 250) },
                (u) => new[] { u.Name, u.Credits.ToString("00000") },
                new Column("Name", 0),
                new Column("Credits", 0)
            );
            view.Render();
            Console.ReadKey(true);
        }

        public static void ListViewRenderChessGamesSwapFocus()
        {
            var window = new Window(100, 20, Style.BlueOnWhite);

            int y = 3;

            while (Console.ReadKey(true).KeyChar != 'q')
            {
                RenderGames(window, y + 2, Active);
                RenderUsers(window, y + 2, Inactive);
                Console.ReadKey(true);

                RenderGames(window, y + 2, Inactive);
                RenderUsers(window, y + 2, Active);
                Console.ReadKey(true);
            }

        }


        static void RenderUsers(IConsole console, int sy, ControlStatus status)
        {

            var window = console.OpenBox("players", new WindowSettings { SX = 7, SY = sy, Width = 35, Height = 7 });
            var view = new ListView<(string Name, int Credits, string IPAddress)>(
                window,
                () => new[] {
                    ("Fred",    250,    "80.56.12.11"),
                    ("Sally",   100,    "74.11.23.44"),
                    ("Michael", 0,      "63.11.11.40"),
                    ("Dennis",  0,      "88.22.23.12")
                }, (u) => new[] {
                        u.Name,
                        u.Credits.ToString("00000"),
                        u.IPAddress
                },
                new Column("Name", 0),
                new Column("Credits", 0),
                new Column("IP Address", 0)
            );
            view.Status = status;
            view.Render();
        }

        static void RenderGames(IConsole console, int sy, ControlStatus status)
        {
            var window = console.OpenBox("openings", 50, sy, 35, 12);
            var view = new ListView<(string opening, int moves, string result)>(
                window,
                () => new[] {
                    ("Kings Gambit",        39, "win"),
                    ("Sicilian Defence",    35, "draw"),
                    ("French Defence",      22, "win"),
                    ("Alekhine Defence",    19, "win"),
                    ("Kings Gambit",        33, "win"),
                    ("Kings Indian",        21, "draw"),
                    ("Ruy Lopaz",           82, "lose")
                }, (u) => new[] {
                    u.opening,
                    u.moves.ToString(),
                    u.result,
                },
                new Column("Opening", 0),
                new Column("Moves", 7),
                new Column("Result", 7)
            );
            view.BusinessRuleColors = (i, col) =>
            {
                if (col == 3 && i.result == "lose") return new Colors(White, Red);
                return null;
            };
            view.Status = status;
            view.Render();
        }

    }
}
