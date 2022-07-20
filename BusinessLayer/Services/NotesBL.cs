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

    }
}
