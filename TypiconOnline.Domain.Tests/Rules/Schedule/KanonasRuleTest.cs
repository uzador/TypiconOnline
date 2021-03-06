﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KanonasRuleTest
    {
        [Test]
        public void KanonasRule_FromDB()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            //BookStorage.Instance = BookStorageFactory.Create();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru") }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("KanonasRuleTest.xml");

            DateTime date = new DateTime(2017, 11, 11);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            var bookStorage = BookStorageFactory.Create();

            OktoikhDay oktoikhDay = bookStorage.Oktoikh.Get(date);
            handler.Settings.OktoikhDay = oktoikhDay;

            //rule.GetRule(TestRuleSerializer.Root).Interpret(handler);

            handler.ClearResult();
            KanonasRule kanonasRule = rule.GetRule<KanonasRule>(TestRuleSerializer.Root);
            kanonasRule.Interpret(handler);

            Assert.AreEqual(4, kanonasRule.Kanones.Count());
            //Assert.IsNotNull(kanonasRule.Sedalen);
        }
    }
}
