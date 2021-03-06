﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;

namespace TypiconOnline.AppServices.Tests.Common
{
    [TestFixture]
    public class IntCsTest
    {
        [TestCase("№", 1)]
        [TestCase("а", 1)]
        [TestCase("а7", 1)]
        [TestCase("в", 2)]
        [TestCase("G", 3)]
        [TestCase("г", 3)]
        [TestCase("д", 4)]
        [TestCase("є", 5)]
        [TestCase("ѕ", 6)]
        [TestCase("з", 7)]
        [TestCase("рн7ѕ", 156)]
        [TestCase("сг7i", 213)]
        [TestCase(@"т\f", 309)]
        [TestCase(@"µк\}", 428)]
        [TestCase(@"фл\и", 538)]
        [TestCase(@"хм\f", 649)]
        [TestCase(@"pн\д", 754)]
        [TestCase(@"tx\є", 865)]
        [TestCase(@"ц\в", 902)]
        [TestCase(@"¤єсп\з", 5287)]
        [TestCase(@"¤к\", 20000)]
        [TestCase(@"¤ксп\з", 20000)]
        public void IntCs_Parse(string str, int expected)
        {
            int value = IntCs.Parse(str);
            Assert.AreEqual(expected, value);
        }
    }
}
