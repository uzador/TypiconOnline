﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;
using System.IO;
using TypiconOnline.AppServices.Implementations;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class MikrosEsperinosTest
    {
        [Test]
        public void MikrosEsperinos_Creature()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("MikrosEsperinosTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            MikrosEsperinos element = ser.Deserialize<MikrosEsperinos>(xml);

            Assert.AreEqual(element.Kekragaria.Groups[0].Ymnis.Count, 3);
        }
    }
}
