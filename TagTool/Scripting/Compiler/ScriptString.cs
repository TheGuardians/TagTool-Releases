﻿namespace TagTool.Scripting.Compiler
{
    public class ScriptString : IScriptSyntax
    {
        public string Value;

        public int Line { get; set; }
    }
}