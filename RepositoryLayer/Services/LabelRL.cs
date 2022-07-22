using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity Create(LabelModel labelModel, long userid, long noteid)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();

                labelEntity.LabelName = labelModel.LabelName;
                labelEntity.NoteID = noteid;
                labelEntity.UserID = userid;

                fundooContext.LabelTable.Add(labelEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Delete(long LabelID)
        {

            var result = fundooContext.LabelTable.FirstOrDefault(e => e.NoteID == LabelID);
            if (result != null)
            {
                fundooContext.LabelTable.Remove(result);
                fundooContext.SaveChanges();
                return "Notes Delete Successfull";
            }
            else
            {
                return "Notes Does not Delete";
            }
        }
        public LabelEntity Update(LabelModel labelModel, long ID)
        {
            var data = fundooContext.LabelTable.SingleOrDefault(x => x.LabelID == ID);
            if (data != null)
            {
                data.LabelName = labelModel.LabelName;
                fundooContext.LabelTable.Update(data);
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<LabelEntity> GetLables()
        {
            return fundooContext.LabelTable.ToList();
        }
    }
}
