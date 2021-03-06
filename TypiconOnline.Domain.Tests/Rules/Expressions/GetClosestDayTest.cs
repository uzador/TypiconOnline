﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class GetClosestDayTest
    {
        [Test]
        public void WithoutAttributes()
        {
            string xmlString = @"<getclosestday><date>--11-08</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    GetClosestDay element = new GetClosestDay(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void GetClosestDay_WeekCount()
        {
            string xmlString = @"<getclosestday dayofweek=""суббота"" weekcount=""-2""><date>--11-08</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            Assert.IsTrue(element.IsValid);

            //try
            //{
            //    GetClosestDay element = new GetClosestDay(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void GetClosestDay_RightDayOfWeekName()
        {
            string xmlString = @"<getclosestday dayofweek=""воскресенье"" weekcount=""-2""><date>--11-08</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            Assert.Pass("DayOfWeek " + element.DayOfWeek + ", WeekCount " + element.WeekCount);
        }

        [Test]
        public void GetClosestDay_SeekFirstSaturday()
        {
            string xmlString = @"<getclosestday dayofweek=""суббота"" weekcount=""-1""><date>--04-16</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.AreEqual(element.ValueCalculated, new DateTime(2017, 04, 15));
        }

        [Test]
        public void GetClosestDay_SeekSecSunday()
        {
            string xmlString = @"<getclosestday dayofweek=""воскресенье"" weekcount=""-2""><date>--04-16</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.AreEqual(element.ValueCalculated, new DateTime(2017, 04, 02));
        }

        [Test]
        public void GetClosestDay_SeekFirstSunday()
        {
            string xmlString = @"<getclosestday dayofweek=""воскресенье"" weekcount=""1""><date>--04-27</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.AreEqual(element.ValueCalculated, new DateTime(2017, 04, 30));
        }

        [Test]
        public void GetClosestDay_SeekClosestSunday()
        {
            string xmlString = @"<getclosestday dayofweek=""воскресенье"" weekcount=""0""><date>--10-24</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2014, 12, 12)));

            Assert.AreEqual(element.ValueCalculated, new DateTime(2014, 10, 26));
        }

        [Test]
        public void GetClosestDay_SeekClosestSunday2()
        {
            string xmlString = @"<getclosestday dayofweek=""воскресенье"" weekcount=""0""><date>--05-07</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 12, 12)));

            Assert.AreEqual(element.ValueCalculated, new DateTime(2017, 5, 7));
        }

        [Test]
        public void GetClosestDay_SeekClosestMonday()
        {
            string xmlString = @"<getclosestday dayofweek=""понедельник"" weekcount=""0""><date>--05-07</date></getclosestday>";

            var element = TestRuleSerializer.Deserialize<GetClosestDay>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 12, 12)));

            Assert.AreEqual(element.ValueCalculated, new DateTime(2017, 5, 8));
        }
    }
}
