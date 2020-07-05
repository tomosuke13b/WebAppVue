using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

namespace WebAppVue.Models
{
    public class ImageModel
    {
        public ImageModel() {
        }

        public Json.Image Get(Int64 id)
        {
            using (var db = new TestContext())
            {
                return db.Images
                    .Where(image => !image.Deleted && image.Id == id)
                    .Select(image => new Json.Image
                    {
                        Id = image.Id,
                        Data = image.Data,
                        ContentType = image.ContentType,
                    })
                    .FirstOrDefault();
            }
        }

        public Json.Image Create(Json.Image item)
        {
            using (var db = new TestContext())
            using (var tran = db.Database.BeginTransaction())
            {
                var newimage = new Entity.TestContext.Image();
                newimage.Data = item.Data;
                newimage.ContentType = item.ContentType;
                db.Images.Add(newimage);
                if (db.SaveChanges() <= 0)
                {
                    tran.Rollback();
                    return null;
                }

                tran.Commit();
                item.Id = newimage.Id;
                return item;
            }
        }

        public long Update(Int64 id, Json.Image item)
        {
            using (var db = new TestContext())
            {
                var image = db.Images
                    .Where(image => !image.Deleted)
                    .FirstOrDefault(image => image.Id == id);
                if (image == null) return -1;

                image.Data = item.Data;
                image.ContentType = item.ContentType;

                return db.SaveChanges() > 0 ? id : -1;
            }
        }

        public bool Delete(Int64 id)
        {
            using (var db = new TestContext())
            {
                var image = db.Images
                    .FirstOrDefault(image => image.Id == id && !image.Deleted);
                if (image == null) return false;
                var timestamp = DateTime.Now;
                image.Deleted = true;
                image.Delete_at = timestamp;
                return db.SaveChanges() > 0;
            }
        }


    }
}
