using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity Create(CollabModel collabModel, long userid, long noteid);
        public IEnumerable<CollabEntity> GetCollabs();
        public string Delete(long NoteID);
    }
}
