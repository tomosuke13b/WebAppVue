using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

namespace WebAppVue.Models
{
    public class NameModel
    {
        public NameModel() {
        }

        public Json.Item[] List()
        {
            using (var db = new TestContext())
            {
                return db.Names
                    .Where(name => !name.Deleted)
                    .Select(name => new Json.Item
                    {
                        Id = name.Id,
                        Sort = name.Sort,
                        Name = name.Text,
                        ImageIds = NameModel.JsonDeserialize<List<Int64>>(name.ImageIds),
                        TimeStamp = NameModel.ConvertIntToDateTime(name.Date, name.Time)
                    })
                    .ToArray();
            }
        }
        public Json.Item Get(Int64 id)
        {
            using (var db = new TestContext())
            {
                var item = db.Names.Join(
                    db.Descriptions,
                    name => name.Id,
                    descriptions => descriptions.NamesId,
                    (name, description) => new
                    {
                        Id = name.Id,
                        Delete = name.Deleted || description.Deleted,
                        Sort = name.Sort,
                        Name = name.Text,
                        ImageIds = NameModel.JsonDeserialize<List<Int64>>(name.ImageIds),
                        Date = name.Date,
                        Time = name.Time,
                        Description = description.Text
                    })
                    .FirstOrDefault(item => item.Id == id && !item.Delete);

                return item == null ? null : new Json.Item
                {
                    Id = item.Id,
                    Sort = item.Sort,
                    Name = item.Name,
                    ImageIds = item.ImageIds,
                    TimeStamp = NameModel.ConvertIntToDateTime(item.Date, item.Time),
                    Description = item.Description
                };
            }
        }

        public long Create(Json.Item item)
        {
            using (var db = new TestContext())
            using (var tran = db.Database.BeginTransaction())
            {
                var newName = new Entity.TestContext.Name();
                newName.Text = item.Name;
                newName.ImageIds = NameModel.JsonSerialize(item.ImageIds);
                newName.Sort = item.Sort;
                newName.Date = NameModel.ConvertDateToInt(item.TimeStamp);
                newName.Time = NameModel.ConvertTimeToInt(item.TimeStamp);
                db.Names.Add(newName);
                if (db.SaveChanges() <= 0)
                {
                    tran.Rollback();
                    return -1;
                }

                var newDescription = new Entity.TestContext.Description();
                newDescription.NamesId = newName.Id;
                newDescription.Text = item.Description;
                db.Descriptions.Add(newDescription);
                if (db.SaveChanges() <= 0)
                {
                    tran.Rollback();
                    return -1;
                }

                var images = db.Images
                    .Where(image => !image.Deleted)
                    .Where(image => item.ImageIds.Contains(image.Id));
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        image.NamesId = newName.Id;
                    }
                    if (db.SaveChanges() <= 0)
                    {
                        tran.Rollback();
                        return -1;
                    }
                }

                tran.Commit();
                return newName.Id;
            }
        }

        public long Update(Int64 id, Json.Item item)
        {
            using (var db = new TestContext())
            {
                var name = db.Names
                    .Where(name => !name.Deleted)
                    .FirstOrDefault(name => name.Id == id);
                if (name == null) return -1;

                var description = db.Descriptions
                    .Where(description => !description.Deleted)
                    .FirstOrDefault(description => description.NamesId == id);
                if (description == null) return -1;

                name.Text = item.Name;
                name.ImageIds = NameModel.JsonSerialize(item.ImageIds);
                name.Sort = item.Sort;
                name.Date = NameModel.ConvertDateToInt(item.TimeStamp);
                name.Time = NameModel.ConvertTimeToInt(item.TimeStamp);
                description.Text = item.Description;

                return db.SaveChanges() > 0 ? id : -1;
            }
        }

        public bool Delete(Int64 id)
        {
            using (var db = new TestContext())
            {
                var name = db.Names
                    .FirstOrDefault(name => name.Id == id && !name.Deleted);
                var description = db.Descriptions
                    .FirstOrDefault(description => description.NamesId == id && !description.Deleted);
                if (name == null) return false;
                var timestamp = DateTime.Now;
                name.Deleted = true;
                name.Delete_at = timestamp;
                description.Deleted = true;
                description.Delete_at = timestamp;
                return db.SaveChanges() > 0;
            }
        }


        private static DateTime ConvertIntToDateTime(int date, int time)
        {
            var dateTime = (date.ToString() + time.ToString());
            var format = "yyyyMMddHHmmss";
            try
            {
                // TODO: データ不正時の処理は未実装
                return DateTime.ParseExact(dateTime, format, null);
            }
            catch { }
            return DateTime.Now;
        }
        private static int ConvertDateToInt(DateTime timeStamp)
        {
            try
            {
                // TODO: データ不正時の処理は未実装
                return Convert.ToInt32(timeStamp.ToString("yyyyMMdd"));
            }
            catch { }
            return Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
        }
        private static int ConvertTimeToInt(DateTime timeStamp)
        {
            try
            {
                // TODO: データ不正時の処理は未実装
                return Convert.ToInt32(timeStamp.ToString("HHmmss"));
            }
            catch { }
            return Convert.ToInt32(timeStamp.ToString("HHmmss"));
        }

        private static T JsonDeserialize<T>(string images)
        {
            if (images == null) return default(T);
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                return JsonSerializer.Deserialize<T>(images, options);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private static string JsonSerialize<T>(T images)
        {
            if (images == null) return null;
            try
            {
                return JsonSerializer.Serialize(images);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
