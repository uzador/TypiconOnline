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
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsCelebratingTest
    {
        [Test]
        public void IsCelebrating_Test()
        {
            //находим первый попавшийся MenologyRule
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            BookStorage.Instance = BookStorageFactory.Create();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;

            ServiceSequenceHandler handler = new ServiceSequenceHandler() { Settings = new RuleHandlerSettings() { Language = "cs-ru" } };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.GetXml("IsCelebrating_Simple.xml");

            //Ektenis element = RuleFactory.CreateElement(xml) as Ektenis;


            //находим Праздничное правило 
            MenologyRule rule = typiconEntity.GetMenologyRule(new DateTime(2017, 09, 28));
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayServices = rule.DayServices;

            rule.Rule.Interpret(DateTime.Today, handler);

            EktenisViewModel model = (rule.Rule as Ektenis).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(3, model.ChildElements.Count);

            //а теперь находим правило НЕ праздничное
            rule = typiconEntity.GetMenologyRule(new DateTime(2017, 10, 15));
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayServices = rule.DayServices;

            rule.Rule.Interpret(DateTime.Today, handler);

            model = (rule.Rule as Ektenis).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.ChildElements.Count);
        }
    }
}
