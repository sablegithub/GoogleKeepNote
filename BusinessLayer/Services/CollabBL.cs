using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL: ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL=collabRL;
        }
        public CollabEntity Create(CollabModel collabModel, long userid, long noteid)
        {
            try
            {
                return collabRL.Create(collabModel, userid,noteid);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollabEntity> GetCollabs()
        {
            try
            {
                return collabRL.GetCollabs();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Delete(long NoteID)
        {
            try
            {
                return collabRL.Delete(NoteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
