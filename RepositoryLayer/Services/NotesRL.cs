using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesEntity Create(NotesModel notesModel, long userid)
        {

            try
            {
                NotesEntity noteentity = new NotesEntity();
                noteentity.Title = notesModel.Title;
                noteentity.Discription = notesModel.Discription;
                noteentity.image = notesModel.image;
                noteentity.color = notesModel.color;
                noteentity.remainder = notesModel.remainder;
                noteentity.createdate = notesModel.createdate;
                noteentity.modifidedate = notesModel.modifidedate;
                noteentity.archieve = notesModel.archieve;
                noteentity.pin = notesModel.pin;
                noteentity.trash = notesModel.trash;
                noteentity.UserID = userid;

                fundooContext.NotesTable.Add(noteentity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteentity;
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


    }
}
