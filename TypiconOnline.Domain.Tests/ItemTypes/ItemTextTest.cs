﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextTest
    {
        [Test]
        public void ItemText_Right()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.IsFalse(element.IsEmpty);
        }

        [Test]
        public void ItemText_WrongLanguage()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-c1s"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.Pass(element.StringExpression);
        }

        [Test]
        public void ItemText_Deserialize()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            TypiconSerializer ser = new TypiconSerializer();
            ItemText element = ser.Deserialize<ItemText>(xmlString);

            Assert.IsNotNull(element);
            Assert.IsFalse(element.IsEmpty);
        }

        [Test]
        public void ItemText_Serialize()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";
            TypiconSerializer ser = new TypiconSerializer();
            ItemText element = ser.Deserialize<ItemText>(xmlString);

            element["cs-cs"] = "cs-cs Текст измененный";

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }

        //[Test]
        //public void ItemText_Serialize_wrongstring()
        //{
        //    string xmlString = @"some string";
        //    TypiconSerializer ser = new TypiconSerializer();
        //    ItemText element = ser.Deserialize<ItemText>(xmlString);

        //    Assert.IsNull(element);
        //}

        //[Test]
        //public void ItemText_StringExpression_wrongstring()
        //{
        //    string xmlString = @"some string";
        //    ItemText element = new ItemText()
        //    {
        //        StringExpression = xmlString
        //    };

        //    Assert.IsNotNull(element);
        //}

        [Test]
        public void ItemText_StringExpression()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText
            {
                StringExpression = xmlString
            };

            Assert.IsFalse(element.IsEmpty);
        }

        
    }
}
