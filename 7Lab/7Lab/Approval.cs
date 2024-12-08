namespace _7Lab
{
    /// <summary>
    /// структура с утверждением
    /// </summary>
    public struct Approval 
    {

        public string Object { get; set; }

        public string Who { get; set; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="obj">объект</param>
        /// <param name="who">кем является</param>
        public Approval(string obj, string who)
        {
            Object = obj;
            Who = who;
        }

        public override string ToString()
        {
            return $"{Object} - {Who}";
        }
    }
}
