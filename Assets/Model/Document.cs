using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Document
    {
        public Int64 Id { get; set; }
        public String Title { get; set; }
        public String Type { get; set; }
        public Int32 Path { get; set; }

        public virtual ICollection<Enterprise_docs> Enterprise_docs { get; set; }

        public Document(Int64 id, String title, String type, Int32 path)
        {
            Id = id;
            Title = title;
            Type = type;
            Path = path;
        }
    }
}