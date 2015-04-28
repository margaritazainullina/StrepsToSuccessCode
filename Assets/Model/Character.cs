using System;

namespace Model
{
    /// <summary>
    /// Singletone class use instance property which is Character to work with it
    /// </summary>
    [Serializable]
    public class Character  //Singletone
    {
        public Int64 Id { get; set; }
        public String Title { get; set; }
        public String Gender { get; set; }
        public Int32 Level { get; set; }

        public DateTime GameTime { get; set; }
        public String GameDay { get; set; }
        public String GameScene { get; set; }

        public Double LocationX { get; set; }
        public Double LocationY { get; set; }
        public Double LocationZ { get; set; }

        private static Character instance;

        public static Character Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Character();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public virtual Enterprise Enterprise { get; set; }

        private Character()
        {
        }
    }
}