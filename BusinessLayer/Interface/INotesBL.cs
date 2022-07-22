using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity Create(NotesModel notesModel,long userid);

        public string Delete(long NoteID);

        public IEnumerable<NotesEntity> Retrieve(long NoteID);

        public IEnumerable<NotesEntity> GetNotess();

        public NotesEntity Update(UpdateModel updateModel, long NoteID);

        public NotesEntity Pin(long NoteID);

        public NotesEntity Trash (long NoteID);

        public NotesEntity Archive (long NoteID);

        public NotesEntity Image(long NoteID, IFormFile img);

    }
}
