﻿using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity Create(NotesModel notesModel,long userid);

        public string Delete(long NoteID);

        public IEnumerable<NotesEntity> Retrieve(long NoteID);

        public NotesEntity Update(UpdateModel updateModel, long NoteID);
    }
}