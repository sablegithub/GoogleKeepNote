using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public const string CLOUD_NAME = "dgb6b1laf";
        public const string API_KEY= "195912455371571";
        public const string   API_Secret= "_RRp6M_qVXzJmLKi5GFrABto1AM";
        public static Cloudinary cloud;
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

        public string Delete(long NoteID)
        {
           
            var result = fundooContext.NotesTable.FirstOrDefault(e => e.NoteID == NoteID);
            if (result != null)
            {
                fundooContext.NotesTable.Remove(result);
                fundooContext.SaveChanges();
                return "Notes Delete Successfull";
            }
            else
            {
                return "Notes Does not Delete";
            }
        }

        public IEnumerable<NotesEntity> Retrieve(long NoteID)
        {
                var result = fundooContext.NotesTable.SingleOrDefault(e => e.NoteID == NoteID);
                List<NotesEntity> list = fundooContext.NotesTable.ToList();
                if (result !=null )
                {
                return list;
                }
                else
                {
                    return null;
                }
        }

        public NotesEntity Update(UpdateModel updateModel, long NoteID)
        {
            var data= fundooContext.NotesTable.SingleOrDefault(x=>x.NoteID == NoteID);
            if(data != null)
            {
                data.Title = updateModel.Title;
                data.Discription = updateModel.Discription;
                data.image = updateModel.image;
                data.color=updateModel.color;
                data.remainder=updateModel.remainder;
                data.createdate=updateModel.createdate;
                data.modifidedate=updateModel.modifidedate;
                data.archieve=updateModel.archieve;
                data.pin = updateModel.pin;
                data.trash=updateModel.trash;
                fundooContext.NotesTable.Update(data);
                fundooContext.SaveChanges();
                return data ;
            }
            else
            {
                return null;
            }
        }

        public NotesEntity Pin(long NoteID)
        {
            NotesEntity data = fundooContext.NotesTable.FirstOrDefault (x=>x.NoteID==NoteID);
            if(data.pin == true)
            {
                data.pin=false;
                
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }

        public NotesEntity Trash(long NoteID)
        {
            NotesEntity data = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
            if (data.trash == true)
            {
                data.trash = false;
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
        public NotesEntity Archive(long NoteID)
        {
            NotesEntity data = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
            if (data.archieve == true)
            {
                data.archieve = false;
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }

        public NotesEntity Image(long NoteID, IFormFile img)
        {
            try
            {
                var data = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
                if(data !=null)
                {
                    Account acc = new Account(CLOUD_NAME, API_KEY, API_Secret);
                    cloud = new Cloudinary(acc);
                    var imagepath = img.OpenReadStream();
                    var uploadparams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, imagepath)
                    };
                    var uploadresult = cloud.Upload(uploadparams);
                    data.image = img.FileName;
                    fundooContext.NotesTable.Update(data);
                    int upload = fundooContext.SaveChanges();
                    if(upload>0)
                    {
                        return data;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                 throw;
            }
        }
    }
}
