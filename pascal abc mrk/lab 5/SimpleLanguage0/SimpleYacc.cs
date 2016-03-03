// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.6
// Machine:  SIR2100
// DateTime: 08.12.2015 11:30:31
// UserName: ?????
// Input file <SimpleYacc.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace SimpleParser
{
public enum Tokens {
    error=1,EOF=2,BEGIN=3,END=4,CYCLE=5,INUM=6,
    RNUM=7,ID=8,ASSIGN=9,SEMICOLON=10,WHILE=11,DO=12,
    REPEAT=13,UNTIL=14,FOR=15,TO=16,WRITE=17,OPENBRACKET=18,
    CLOSEBRACKET=19,IF=20,THEN=21,ELSE=22,COMMA=23,VAR=24,
    PLUS=25,SUBS=26,MULT=27,DIV=28};

// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<int,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

public class Parser: ShiftReduceParser<int, LexLocation>
{
  // Verbatim content from SimpleYacc.y
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
  // End verbatim content from SimpleYacc.y

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[35];
  private static State[] states = new State[69];
  private static string[] nonTerms = new string[] {
      "progr", "$accept", "block", "stlist", "statement", "assign", "cycle", 
      "while", "repeat", "for", "write", "if", "var", "ident", "expr", "T", "F", 
      };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-3,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,5,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-25);
    states[7] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-5,8,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[8] = new State(-4);
    states[9] = new State(-5);
    states[10] = new State(new int[]{9,11});
    states[11] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,12,-16,28,-17,27,-14,17});
    states[12] = new State(new int[]{25,13,26,23,4,-15,10,-15,14,-15,22,-15,16,-15});
    states[13] = new State(new int[]{8,18,6,19,18,20},new int[]{-16,14,-17,27,-14,17});
    states[14] = new State(new int[]{27,15,28,25,25,-17,26,-17,4,-17,10,-17,14,-17,22,-17,16,-17,19,-17,8,-17,3,-17,5,-17,11,-17,13,-17,15,-17,17,-17,20,-17,24,-17,12,-17,21,-17});
    states[15] = new State(new int[]{8,18,6,19,18,20},new int[]{-17,16,-14,17});
    states[16] = new State(-20);
    states[17] = new State(-22);
    states[18] = new State(-14);
    states[19] = new State(-23);
    states[20] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,21,-16,28,-17,27,-14,17});
    states[21] = new State(new int[]{19,22,25,13,26,23});
    states[22] = new State(-24);
    states[23] = new State(new int[]{8,18,6,19,18,20},new int[]{-16,24,-17,27,-14,17});
    states[24] = new State(new int[]{27,15,28,25,25,-18,26,-18,4,-18,10,-18,14,-18,22,-18,16,-18,19,-18,8,-18,3,-18,5,-18,11,-18,13,-18,15,-18,17,-18,20,-18,24,-18,12,-18,21,-18});
    states[25] = new State(new int[]{8,18,6,19,18,20},new int[]{-17,26,-14,17});
    states[26] = new State(-21);
    states[27] = new State(-19);
    states[28] = new State(new int[]{27,15,28,25,25,-16,26,-16,4,-16,10,-16,14,-16,22,-16,16,-16,19,-16,8,-16,3,-16,5,-16,11,-16,13,-16,15,-16,17,-16,20,-16,24,-16,12,-16,21,-16});
    states[29] = new State(-6);
    states[30] = new State(-7);
    states[31] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,32,-16,28,-17,27,-14,17});
    states[32] = new State(new int[]{25,13,26,23,8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-5,33,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[33] = new State(-26);
    states[34] = new State(-8);
    states[35] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,36,-16,28,-17,27,-14,17});
    states[36] = new State(new int[]{12,37,25,13,26,23});
    states[37] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,38,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[38] = new State(new int[]{10,7,4,-27,14,-27,22,-27});
    states[39] = new State(-3);
    states[40] = new State(-9);
    states[41] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,42,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[42] = new State(new int[]{14,43,10,7});
    states[43] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,44,-16,28,-17,27,-14,17});
    states[44] = new State(new int[]{25,13,26,23,4,-28,10,-28,14,-28,22,-28});
    states[45] = new State(-10);
    states[46] = new State(new int[]{8,18},new int[]{-6,47,-14,10});
    states[47] = new State(new int[]{16,48});
    states[48] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,49,-16,28,-17,27,-14,17});
    states[49] = new State(new int[]{12,50,25,13,26,23});
    states[50] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,51,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[51] = new State(new int[]{10,7,4,-29,14,-29,22,-29});
    states[52] = new State(-11);
    states[53] = new State(new int[]{18,54});
    states[54] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,55,-16,28,-17,27,-14,17});
    states[55] = new State(new int[]{19,56,25,13,26,23});
    states[56] = new State(-30);
    states[57] = new State(-12);
    states[58] = new State(new int[]{8,18,6,19,18,20},new int[]{-15,59,-16,28,-17,27,-14,17});
    states[59] = new State(new int[]{21,60,25,13,26,23});
    states[60] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,61,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[61] = new State(new int[]{22,62,10,7,4,-31,14,-31});
    states[62] = new State(new int[]{8,18,3,4,5,31,11,35,13,41,15,46,17,53,20,58,24,67},new int[]{-4,63,-5,39,-6,9,-14,10,-3,29,-7,30,-8,34,-9,40,-10,45,-11,52,-12,57,-13,64});
    states[63] = new State(new int[]{10,7,4,-32,14,-32,22,-32});
    states[64] = new State(new int[]{23,65,4,-13,10,-13,14,-13,22,-13});
    states[65] = new State(new int[]{8,18},new int[]{-14,66});
    states[66] = new State(-34);
    states[67] = new State(new int[]{8,18},new int[]{-14,68});
    states[68] = new State(-33);

    rules[1] = new Rule(-2, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-4, new int[]{-5});
    rules[4] = new Rule(-4, new int[]{-4,10,-5});
    rules[5] = new Rule(-5, new int[]{-6});
    rules[6] = new Rule(-5, new int[]{-3});
    rules[7] = new Rule(-5, new int[]{-7});
    rules[8] = new Rule(-5, new int[]{-8});
    rules[9] = new Rule(-5, new int[]{-9});
    rules[10] = new Rule(-5, new int[]{-10});
    rules[11] = new Rule(-5, new int[]{-11});
    rules[12] = new Rule(-5, new int[]{-12});
    rules[13] = new Rule(-5, new int[]{-13});
    rules[14] = new Rule(-14, new int[]{8});
    rules[15] = new Rule(-6, new int[]{-14,9,-15});
    rules[16] = new Rule(-15, new int[]{-16});
    rules[17] = new Rule(-15, new int[]{-15,25,-16});
    rules[18] = new Rule(-15, new int[]{-15,26,-16});
    rules[19] = new Rule(-16, new int[]{-17});
    rules[20] = new Rule(-16, new int[]{-16,27,-17});
    rules[21] = new Rule(-16, new int[]{-16,28,-17});
    rules[22] = new Rule(-17, new int[]{-14});
    rules[23] = new Rule(-17, new int[]{6});
    rules[24] = new Rule(-17, new int[]{18,-15,19});
    rules[25] = new Rule(-3, new int[]{3,-4,4});
    rules[26] = new Rule(-7, new int[]{5,-15,-5});
    rules[27] = new Rule(-8, new int[]{11,-15,12,-4});
    rules[28] = new Rule(-9, new int[]{13,-4,14,-15});
    rules[29] = new Rule(-10, new int[]{15,-6,16,-15,12,-4});
    rules[30] = new Rule(-11, new int[]{17,18,-15,19});
    rules[31] = new Rule(-12, new int[]{20,-15,21,-4});
    rules[32] = new Rule(-12, new int[]{20,-15,21,-4,22,-4});
    rules[33] = new Rule(-13, new int[]{24,-14});
    rules[34] = new Rule(-13, new int[]{-13,23,-14});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}
