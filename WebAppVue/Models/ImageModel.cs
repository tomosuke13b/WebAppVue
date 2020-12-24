using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

namespace WebAppVue.Models
{
    public class ImageModel : BaseModel
    {
        public ImageModel(WebAppContext context) : base(context) { }

        public Json.Image Get(Int64 id)
        {
            return this._context.Images
                .Where(image => !image.Deleted && image.Id == id)
                .Select(image => new Json.Image
                {
                    Id = image.Id,
                    Data = image.Data,
                    ContentType = image.ContentType,
                })
                .FirstOrDefault();
        }

        public Json.Image Create(Json.Image item)
        {
            using (var tran = this._context.Database.BeginTransaction())
            {
                var newimage = new Entity.Context.Image();
                newimage.Data = item.Data;
                newimage.ContentType = item.ContentType;
                this._context.Images.Add(newimage);
                if (this._context.SaveChanges() <= 0)
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
            var image = this._context.Images
                .Where(image => !image.Deleted)
                .FirstOrDefault(image => image.Id == id);
            if (image == null) return -1;

            image.Data = item.Data;
            image.ContentType = item.ContentType;

            return this._context.SaveChanges() > 0 ? id : -1;
        }

        public bool Delete(Int64 id)
        {
            var image = this._context.Images
                .FirstOrDefault(image => image.Id == id && !image.Deleted);
            if (image == null) return false;
            var timestamp = DateTime.Now;
            image.Deleted = true;
            image.Delete_at = timestamp;
            return this._context.SaveChanges() > 0;
        }
    }
}
