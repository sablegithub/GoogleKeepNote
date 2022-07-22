using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity Create(LabelModel labelModel, long userid, long noteid);
        public string Delete(long NoteID);
        public LabelEntity Update(LabelModel labelModel, long ID);

        public IEnumerable<LabelEntity> GetLables();
    }
}
