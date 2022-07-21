using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }


        public NotesEntity Create(NotesModel notesModel,long userid)

        {
            try
            {
                return notesRL.Create(notesModel,userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Delete(long NoteID)
        {
            try
            {
                return notesRL.Delete(NoteID);
            }
            catch (Exception )
            {

                return null;
            }
        }

        public IEnumerable<NotesEntity> Retrieve(long NoteID)
        {
            try
            {
                return notesRL.Retrieve(NoteID);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public NotesEntity Update(UpdateModel updateModel, long NoteID)
        {
            try
            {
                return notesRL.Update(updateModel,NoteID);
            }
            catch (Exception)
            {

                return null ;
            }
        }
    }
}
