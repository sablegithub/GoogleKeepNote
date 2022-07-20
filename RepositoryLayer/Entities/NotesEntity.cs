using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entities
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteID { get; set; }


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

        [ForeignKey("User")]
        public long UserID { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
