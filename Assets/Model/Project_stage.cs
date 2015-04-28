using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Project_stage
    {

        public Int64 Project_id { get; set; }

        public Int32? Conception_hours { get; set; }
        public Int32? Programming_hours { get; set; }
        public Int32? Testing_hours { get; set; }
        public Int32? Design_hours { get; set; }


        public Double? Conception_done { get; set; }
        public Double? Programming_done { get; set; }
        public Double? Testing_done { get; set; }
        public Double? Design_done { get; set; }

        public virtual Project Project { get; set; }

        public Project_stage(Int64 project_id, Int32? conception_hours, Int32? programming_hours, Int32? testing_hours, Int32? design_hours,
                             Double? conception_done, Double? programming_done, Double? testing_done, Double? design_done)
        {
            Conception_hours = conception_hours;
            Programming_hours = programming_hours;
            Testing_hours = testing_hours;
            Design_hours = design_hours;
            Conception_done = conception_done;
            Programming_done = programming_done;
            Testing_done = testing_done;
            Design_done = design_done;
            Project_id = project_id;

        }
        public Project_stage() { }

        public Project_stage(Int64 project_id, Int32? conception_hours, Int32? programming_hours, Int32? testing_hours,
                             Int32? design_hours)
            : this(project_id, conception_hours, programming_hours, testing_hours, design_hours, null, null, null, null)
        {

        }
    }
}