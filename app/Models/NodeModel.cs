using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

namespace WebAppVue.Models
{
    public class NodeModel : BaseModel
    {
        public NodeModel(WebAppContext context) : base(context) { }

        public Json.Item[] List()
        {
            return this._context.Nodes
                .Where(name => !name.Deleted)
                .Select(name => new Json.Item
                {
                    Id = name.Id,
                    Sort = name.Sort,
                    Name = name.Text,
                    ImageIds = NodeModel.JsonDeserialize<List<Int64>>(name.ImageIds),
                    TimeStamp = NodeModel.ConvertIntToDateTime(name.Date, name.Time)
                })
                .ToArray();
        }
        public Json.Item Get(Int64 id)
        {
            var item = this._context.Nodes.Join(
                this._context.Descriptions,
                name => name.Id,
                descriptions => descriptions.NamesId,
                (name, description) => new
                {
                    Id = name.Id,
                    Delete = name.Deleted || description.Deleted,
                    Sort = name.Sort,
                    Name = name.Text,
                    ImageIds = NodeModel.JsonDeserialize<List<Int64>>(name.ImageIds),
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
                TimeStamp = NodeModel.ConvertIntToDateTime(item.Date, item.Time),
                Description = item.Description
            };
        }

        public long Create(Json.Item item)
        {
            using (var tran = this._context.Database.BeginTransaction())
            {
                var newNode = new Entity.Context.Node();
                newNode.Text = item.Name;
                newNode.ImageIds = NodeModel.JsonSerialize(item.ImageIds);
                newNode.Sort = item.Sort;
                newNode.Date = NodeModel.ConvertDateToInt(item.TimeStamp);
                newNode.Time = NodeModel.ConvertTimeToInt(item.TimeStamp);
                this._context.Nodes.Add(newNode);
                if (this._context.SaveChanges() <= 0)
                {
                    tran.Rollback();
                    return -1;
                }

                var newDescription = new Entity.Context.Description();
                newDescription.NamesId = newNode.Id;
                newDescription.Text = item.Description;
                this._context.Descriptions.Add(newDescription);
                if (this._context.SaveChanges() <= 0)
                {
                    tran.Rollback();
                    return -1;
                }

                var images = this._context.Images
                    .Where(image => !image.Deleted)
                    .Where(image => item.ImageIds.Contains(image.Id));
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        image.NamesId = newNode.Id;
                    }
                    if (this._context.SaveChanges() <= 0)
                    {
                        tran.Rollback();
                        return -1;
                    }
                }

                tran.Commit();
                return newNode.Id;
            }
        }

        public long Update(Int64 id, Json.Item item)
        {
            var name = this._context.Nodes
                .Where(name => !name.Deleted)
                .FirstOrDefault(name => name.Id == id);
            if (name == null) return -1;

            var description = this._context.Descriptions
                .Where(description => !description.Deleted)
                .FirstOrDefault(description => description.NamesId == id);
            if (description == null) return -1;

            name.Text = item.Name;
            if (item.ImageIds != null) name.ImageIds = NodeModel.JsonSerialize(item.ImageIds);
            if (item.Sort == -1) name.Sort = item.Sort;
            if (item.TimeStamp != null) name.Date = NodeModel.ConvertDateToInt(item.TimeStamp);
            if (item.TimeStamp != null) name.Time = NodeModel.ConvertTimeToInt(item.TimeStamp);
            description.Text = item.Description;

            return this._context.SaveChanges() > 0 ? id : -1;
        }

        public bool Delete(Int64 id)
        {
            var node = this._context.Nodes
                .FirstOrDefault(name => name.Id == id && !name.Deleted);
            var description = this._context.Descriptions
                .FirstOrDefault(description => description.NamesId == id && !description.Deleted);
            if (node == null) return false;
            var timestamp = DateTime.Now;
            node.Deleted = true;
            node.Delete_at = timestamp;
            description.Deleted = true;
            description.Delete_at = timestamp;
            return this._context.SaveChanges() > 0;
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
                // ToDo: エラー通知
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
                // ToDo: エラー通知
                return null;
            }
        }

    }
}
