using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wsupp;

namespace UnitTest
{
    [TestClass]
    public class Wsupp_UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            String Result;

            CS_Wsupp Wsupp = new CS_Wsupp();

            #region 対象：評価対象なし
            String KeyWord = @"This is a Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();
            Result = Wsupp.Wbuf;

            Assert.AreEqual(KeyWord, Result, @"Wsupp[This is a Pen.] = [This is a Pen.]");
            #endregion
        }

        [TestMethod]
        public void TestMethod2()
        {
            CS_Wsupp Wsupp = new CS_Wsupp();


            #region 対象：評価対象あり（””）
            String KeyWord = "This is \"a\"  Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \" \"  Pen.", Wsupp.Wbuf, "Wsupp[This is \"a\" Pen.] = [This is \" \" Pen.]");
            #endregion

            #region 対象：評価対象あり（””＊２）
            KeyWord = "This \"is\" a \"Pen\".";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This \"  \" a \"   \".", Wsupp.Wbuf, "Wsupp[This \"is\" a \"Pen\".] = [This \"  \" a \"   \".]");
            #endregion
        }

        [TestMethod]
        public void TestMethod3()
        {
            CS_Wsupp Wsupp = new CS_Wsupp();


            #region 対象：評価対象あり（’’）
            String KeyWord = "This is \'a\' Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \' \' Pen.", Wsupp.Wbuf, "Wsupp[This is \'a\' Pen.] = [This is \' \' Pen.]");
            #endregion

            #region 対象：評価対象あり（’’＊２）
            KeyWord = "This is \'a\' Pen\'.\'";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \' \' Pen\' \'", Wsupp.Wbuf, "Wsupp[This is \'a\' Pen\'.\'] = [This is \' \' Pen\' \']");
            #endregion
        }

        [TestMethod]
        public void TestMethod4()
        {
            CS_Wsupp Wsupp = new CS_Wsupp();


            #region 対象：評価対象あり（’”’確認）
            String KeyWord = "This is \'\"\' Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \' \' Pen.", Wsupp.Wbuf, "Wsupp[This is \'\"\' Pen.] = [This is \' \' Pen.]");
            #endregion

            #region 対象：評価対象あり（”’”確認）
            KeyWord = "This is \"\'\" Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \" \" Pen.", Wsupp.Wbuf, "Wsupp[This is \"\'\" Pen.] = [This is \" \" Pen.]");

            KeyWord = "This is \" \' \" Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \"   \" Pen.", Wsupp.Wbuf, "Wsupp[This is \" \' \" Pen.] = [This is \"   \" Pen.]");
            #endregion
        }

        [TestMethod]
        public void TestMethod5()
        {
            CS_Wsupp Wsupp = new CS_Wsupp();


            #region 対象：評価対象あり（’’””混合１）
            String KeyWord = "This is \'a\' \"Pen\".";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \' \' \"   \".", Wsupp.Wbuf, "Wsupp[This is \'a\' \"Pen\".] = [This is \' \' \"   \".]");
            #endregion

            #region 対象：評価対象あり（’’””混合２）
            KeyWord = "This \"is\" \'a\' Pen.";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This \"  \" \' \' Pen.", Wsupp.Wbuf, "Wsupp[This \"is\" \'a\' Pen.] = [This \"  \" \' \' Pen.]");
            #endregion

            #region 対象：評価対象あり（’’””混合３）
            KeyWord = "This \"is\" \'a\' \"Pen\".";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This \"  \" \' \' \"   \".", Wsupp.Wbuf, "Wsupp[This \"is\" \'a\' \"Pen\".] = [This \"  \" \' \' \"   \".]");
            #endregion

            #region 対象：評価対象あり（’’””混合４）
            KeyWord = "This is \'a\' \"Pen\"\'.\'";
            Wsupp.Clear();
            Wsupp.Wbuf = KeyWord;
            Wsupp.Exec();

            Assert.AreEqual("This is \' \' \"   \"\' \'", Wsupp.Wbuf, "Wsupp[This is \'a\' \"Pen\"\'.\'] = [This is \' \' \"   \"\' \']");
            #endregion
        }
    }
}
