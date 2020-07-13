# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== BanFileEntries ====\n");









    }




        private sealed class BanFileEntry
        {
            public BanFileEntry(string text, TextSpan span, SourceText sourceText, string path)
            {
                // Split the text on semicolon into declaration ID and message
                var index = text.IndexOf(';');

                if (index == -1)
                {
                    DeclarationId = text.Trim();
                    Message = string.Empty;
                }
                else if (index == text.Length - 1)
                {
                    DeclarationId = text.Substring(0, text.Length - 1).Trim();
                    Message = string.Empty;
                }
                else
                {
                    DeclarationId = text.Substring(0, index).Trim();
                    Message = text.Substring(index + 1).Trim();
                }

                Span = span;
                SourceText = sourceText;
                Path = path;
            }

            public TextSpan Span { get; }

            public SourceText SourceText { get; }

            public string Path { get; }

            public string DeclarationId { get; }

            public string Message { get; }

            public Location Location => Location.Create(Path, Span, SourceText.Lines.GetLinePositionSpan(Span));
        }






}
# endif