﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Factories
{
    [TestFixture]
    public class RuleSerializerRootTest
    {
        [Test]
        public void RuleSerializerRoot_ExecContainer()
        {
            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var factoryContainer = unitOfWork.Container<ExecContainer>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_RuleElement()
        {
            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var factoryContainer = unitOfWork.Container<RuleElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_If()
        {
            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var factoryContainer = unitOfWork.Container<If>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_TypeTesting()
        {
            Assert.IsTrue(typeof(ExecContainer).IsSubclassOf((typeof(RuleElement))));
        }

        [Test]
        public void RuleSerializerRoot_Additional()
        {
            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var factoryContainer = unitOfWork.Container<RuleExecutable, ICalcStructureElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }
    }
}
