﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Реализация контейнера фабрик для xml-вариации представления элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RuleXmlSerializerContainer<T> : RuleSerializerContainerBase<T> where T: RuleElement
    {
        public RuleXmlSerializerContainer(IRuleSerializerRoot unitOfWork) : base(unitOfWork, new XmlDescriptor()) { }

        protected override void LoadFactories()
        {
            /*
             * делаем выборку всех классов - наследников RuleXmlFactoryBase
             * и где T является базовым для реализаций IRuleFactory<T>
             */
            var typesInThisAssembly = GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                //IRuleFactory<T> factory1 = Activator.CreateInstance(type, _unitOfWork);

                var factory = Activator.CreateInstance(type, _unitOfWork) as IRuleSerializer;

                if (factory.ElementNames != null)
                {
                    foreach (string name in factory.ElementNames)
                    {
                        _factories.Add(name, factory);
                    }
                }
            }
        }

        protected virtual IEnumerable<Type> GetTypes()
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                   from z in type.GetInterfaces()
                   where type.IsSubclassOf(typeof(RuleXmlSerializerBase))
                         && !type.IsAbstract
                         && z.Name == typeof(IRuleSerializer<T>).Name
                         && (z.GenericTypeArguments[0].Equals(typeof(T))
                             || z.GenericTypeArguments[0].IsSubclassOf(typeof(T)))
                   select type).Distinct();
        }
    }
}
