﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Serialization
{
    public abstract class RuleXmlSerializerBase : IRuleSerializer
    {
        public RuleXmlSerializerBase(IRuleSerializerRoot root)
        {
            SerializerRoot = root ?? throw new ArgumentNullException("RuleSerializerRoot");
        }

        protected IRuleSerializerRoot SerializerRoot { get; }

        public IEnumerable<string> ElementNames { get; protected set; }

        public virtual RuleElement Deserialize(IDescriptor descriptor)
        {
            RuleElement element = null;

            if (descriptor is XmlDescriptor d)
            {
                element = CreateObject(d);

                FillObject(d, element);
            }

            return element;
        }

        protected abstract RuleElement CreateObject(XmlDescriptor d);
        protected abstract void FillObject(XmlDescriptor d, RuleElement element);

        public abstract string Serialize(RuleElement element);
    }
}
