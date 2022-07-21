using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UpdateModel
    {
         
       // public long NoteID { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string image { get; set; }
        public string color { get; set; }

        public DateTime remainder { get; set; }
        public DateTime createdate { get; set; }
        public DateTime modifidedate { get; set; }
        public bool archieve { get; set; }
        public bool pin { get; set; }
        public bool trash { get; set; }

    }
}

