using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity Create(LabelModel labelModel, long userid, long noteid);

        public string Delete(long LabelID);

        public LabelEntity Update(LabelModel labelModel, long ID);

        public IEnumerable<LabelEntity> GetLables();
    }
}
