using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7Lab
{
    public class Resolution
    {
        // лист всех утверждений
        protected List<Approval> approvals;
        // лист предикатов
        protected List<string> predicates;

        /// <summary>
        /// Конструктор с одним параметром
        /// Инициализация поля класса
        /// </summary>
        /// <param name="approvals">утверждения</param>
        public Resolution(List<Approval> approvals)
        {
            this.approvals = new List<Approval>();
            this.approvals.AddRange(approvals);
            predicates = CreatePredicates();
        }

        /// <summary>
        /// создание предикатов
        /// </summary>
        /// <returns></returns>
        private List<string> CreatePredicates()
        {
            List<string> predicates = new List<string>();

            // добавление предикатов в лист
            foreach (var approval in approvals)
            {
                if (!predicates.Contains(approval.Who))
                {
                    predicates.Add($"{approval.Who}");
                }
                if (!predicates.Contains(approval.Object))
                {
                    predicates.Add($"{approval.Object}");
                }
            }
            return predicates;
        }

        /// <summary>
        /// метод вывода истинности утверждения
        /// </summary>
        /// <param name="approval">утверждение</param>
        /// <returns></returns>
        public bool Method(Approval approval)
        {
            List<string> dnfs = DNFForm();

            var noApprovalRes = $"!{approval.Who}";
            var knf = KNFForm(dnfs, noApprovalRes);

            return SearchResolvent(knf) == "";
        }

        /// <summary>
        /// Поиск резольвенты
        /// </summary>
        /// <param name="knf">строка - КНФ</param>
        /// <returns></returns>
        private string SearchResolvent(string knf)
        {
            // Разбиваем КНФ на массив подстрок-дизъюнктов
            string[] knfSplit = knf.MySplit('&');
            // Берем первую подстроку в качестве резольвенты
            string resolvent = knfSplit[0];

            // В цикле проведим поиск резольвенты с каждой парой дизъюнктов
            for (int n = knfSplit.Length - 1, i = 0; i < n; i++)
            {
                resolvent = Resolvent(resolvent, knfSplit[i + 1]);
            }
            return resolvent;
        }

        /// <summary>
        /// Поиск контрарной пары резольвент
        /// </summary>
        /// <param name="dnf1"></param>
        /// <param name="dnf2"></param>
        /// <returns></returns>
        public string Resolvent(string dnf1, string dnf2)
        {
            string resolvent = " ";
            string left, right;
            // для каждого предиката
            foreach (var predicate in predicates)
            {
                if (dnf1.Contains(predicate) && dnf2.Contains("!" + predicate))
                {
                    left = dnf1.Replace(predicate, "");
                    right = dnf2.Replace(predicate, "");
                    resolvent = GetResolvent(left, right);
                }
                if (dnf1.Contains("!" + predicate) && dnf2.Contains(predicate))
                {
                    left = dnf1.Replace(predicate, "");
                    right = dnf2.Replace(predicate, "");
                    resolvent = GetResolvent(left, right);
                }
            }
            return resolvent;
        }

        /// <summary>
        /// Получение контрарной пары
        /// </summary>
        /// <param name="left">левый дизъюнкт</param>
        /// <param name="right">правй дизъюнкт</param>
        /// <returns>резольвента</returns>
        private string GetResolvent(string left, string right)
        {
            string resolvent = string.Empty;
            foreach (var predicate in predicates)
            {
                // если предикат содержится в левом дизъюнкте
                if (left.Contains(predicate))
                {
                    if (resolvent.Length > 0)
                    {
                        resolvent += " | ";
                    }
                    resolvent += left.GetResolvent(predicate);
                }
                // если предикат содержится в левом дизъюнкте
                if (right.Contains(predicate))
                {
                    if (resolvent.Length > 0)
                    {
                        resolvent += " | ";
                    }
                    resolvent += right.GetResolvent(predicate);
                }
            }
            return resolvent;
        }

        /// <summary>
        /// Приведение к КНФ
        /// </summary>
        /// <param name="dnfs">лист ДНФ</param>
        /// <param name="noApprovalRes">дизъюнкт искомого выражения</param>
        /// <returns></returns>
        private string KNFForm(List<string> dnfs, string noApprovalRes)
        {
            string knf = dnfs[0];

            for (int n = dnfs.Count, i = 1; i < n; i++)
            {
                knf = knf.KNF(dnfs[i]);
            }

            return knf.KNF(noApprovalRes);
        }

        /// <summary>
        /// Приведение к ДНФ
        /// </summary>
        /// <returns></returns>
        private List<string> DNFForm()
        {
            List<string> dnfs = new List<string>();

            for (int n = predicates.Count - 1, i = 0; i < n; i++)
            {
                dnfs.Add($"{predicates[i]}".DNF($"{predicates[i + 1]}"));
            }
            dnfs.Add($"{approvals[0].Who}");

            return dnfs;
        }

    }
}
