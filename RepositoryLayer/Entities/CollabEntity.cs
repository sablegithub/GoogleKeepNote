using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entities
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID { get; set; }
        public string EmailID { get; set; }


        [ForeignKey("UserTable")]
        public long UserID { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
        [ForeignKey("NoteTable")]
        public long NoteID { get; set; }
        [JsonIgnore]
        public virtual NotesEntity Note { get; set; }
    }
}
