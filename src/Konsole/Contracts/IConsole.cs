﻿namespace Konsole
{
    // begin-snippet: IConsole
    public interface IConsole : IPrintAtColor, IConsoleState, IWriteColor, IScrollingWindow, ITheme, IConsoleApplication
    { 
        
    }
    // end-snippet
}   
