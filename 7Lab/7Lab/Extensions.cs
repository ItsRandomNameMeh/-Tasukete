namespace _7Lab
{
    public static class Extensions
    {
        // возвращает строку в КНФ

        public static string KNF(this string a, string b)
        {
            return $"{a} & {b}";
        }

        // возвращает строку в ДНФ
        public static string DNF(this string a, string b)
        {
            return $"!{a} | {b}";
        }

        /// <summary>
        /// Разбиаваем строку КНФ на массив подстрок
        /// </summary>
        /// <param name="str">строка КНФ</param>
        /// <param name="sym">символ по которому разбиваем</param>
        /// <returns></returns>
        public static string[] MySplit(this string str, char sym)
        {
            string[] knfSplit = str.Split(sym);

            for (int i = 0, n = knfSplit.Length; i < n; i++)
            {
                knfSplit[i] = knfSplit[i].Trim();
            }

            return knfSplit;
        }

        public static string GetResolvent(this string str, string predicate)
        {
            int index = str.IndexOf(predicate[0]);
            string result = string.Empty;
            if (str[index - 1] == '!')
            {
                result += "!";
            }
            result += str.Substring(index, predicate.Length);
            return result;
        }
    }
}
