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
    public class CollabRL: ICollabRL
    {
        private readonly FundooContext fundooContext;
      
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollabEntity Create(CollabModel collabModel,long userid,long noteid)
        {
            try
            {
                CollabEntity collabentity = new CollabEntity();
                collabentity.EmailID= collabModel.EmailID;
                collabentity.NoteID= noteid;
                collabentity.UserID = userid;

                fundooContext.CollabTable.Add(collabentity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabentity;
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
        public IEnumerable<CollabEntity> GetCollabs()
        {
            return fundooContext.CollabTable.ToList();
        }

        public String Delete(long NoteID)
        {

            var result = fundooContext.CollabTable.FirstOrDefault(e => e.NoteID == NoteID);
            if (result != null)
            {
                fundooContext.CollabTable.Remove(result);
                fundooContext.SaveChanges();
                return "Notes Delete Successfull";
            }
            else
            {
                return "Notes Does not Delete";
            }
        }
    }
}
