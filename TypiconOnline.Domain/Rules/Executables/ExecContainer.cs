﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ExecContainer : RuleExecutable
    {
        public ExecContainer() { }

        public ExecContainer(string name) : base(name) { }

        #region Properties

        //public RuleElement ParentElement { get; set; }

        public virtual List<RuleElement> ChildElements { get; set; } = new List<RuleElement>();

        #endregion

        #region Methods

        protected override void InnerInterpret(IRuleHandler handler)
        {
            foreach (RuleElement el in ChildElements)
            {
                el.Interpret(handler);
            }
        }

        protected override void Validate()
        {
            if (ChildElements.Count == 0)
            {
                //HACK: убрана проверка на обязательное наличие дочерних элементов. В дальнейшем надо ее вернуть, когда будут заполнены все правила
                AddBrokenConstraint(ExecContainerBusinessConstraint.ExecContainerChildrenRequired);
            }
            else
            {
                foreach (RuleElement element in ChildElements)
                {
                    if (element == null)
                    {
                        AddBrokenConstraint(ExecContainerBusinessConstraint.InvalidChild); 
                    }
                    //добавляем ломаные правила к родителю
                    else if (!element.IsValid)
                    {
                        AppendAllBrokenConstraints(element);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает все дочерние элементы согласно введенного generic типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected ExecContainer GetChildElements<T>(IRuleHandler handler) //where T : RuleExecutable, ICustomInterpreted
        {
            //используем специальный обработчик
            //чтобы создать список источников канонов на обработку
            var childrenHandler = new CollectorRuleHandler<T>() { Settings = handler.Settings };

            foreach (RuleElement elem in ChildElements)
            {
                elem.Interpret(childrenHandler);
            }

            return childrenHandler.GetResult();
        }

        #endregion
    }
}

