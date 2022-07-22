using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity Create(LabelModel labelModel, long userid, long noteid)
        {
            try
            {
                return labelRL.Create(labelModel, userid, noteid);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Delete(long LabelID)
        {
            try
            {
                return labelRL.Delete(LabelID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LabelEntity Update(LabelModel labelModel, long ID)
        {
            try
            {
                return labelRL.Update(labelModel,ID);
            }
            catch (Exception)
            {

                return null ;
            }
        }
        public IEnumerable<LabelEntity> GetLables()
        {
            try
            {
                return labelRL.GetLables();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
