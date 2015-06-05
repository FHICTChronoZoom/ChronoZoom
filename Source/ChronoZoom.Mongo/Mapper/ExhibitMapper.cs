using Chronozoom.Business.Repositories;
using ChronoZoom.Mongo.Models;
using ChronoZoom.Mongo.PersistencyEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Mapper
{
    class ExhibitMapper : IExhibitRepository
    {
        private ExhibitFactory exhibitFactory;
        private UserMapper userMapper;

        public ExhibitMapper(ExhibitFactory exhibitFactory, UserMapper userFactory)
        {
            this.exhibitFactory = exhibitFactory;
            this.userMapper = userFactory;
        }

        public async Task<IEnumerable<Chronozoom.Business.Models.Exhibit>> GetByTimelineAsync(Guid timelineId)
        {
            List<Mongo.Models.Exhibit> exhibitList = await exhibitFactory.FindByTimelineIdAsync(timelineId);
            List<Chronozoom.Business.Models.Exhibit> listMappedExhibit = new List<Chronozoom.Business.Models.Exhibit>();

            foreach (Mongo.Models.Exhibit exhibit in exhibitList)
            {
                Chronozoom.Business.Models.User UpdatedBy = await userMapper.FindByIdAsync(exhibit.UpdatedByUser);
                Chronozoom.Business.Models.Exhibit mappedExhibit = mapExhibit(exhibit, UpdatedBy);
                
                listMappedExhibit.Add(mappedExhibit);
            }
            return listMappedExhibit;
        }

        public Task<Chronozoom.Business.Models.Exhibit> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Chronozoom.Business.Models.Exhibit item)
        {
            Exhibit mappedItem = mapExhibit(item);
            return await exhibitFactory.InsertAsync(mappedItem);
        }

        public async Task<bool> UpdateAsync(Chronozoom.Business.Models.Exhibit item)
        {
            Exhibit mappedItem = mapExhibit(item);
            return await exhibitFactory.UpdateAsync(mappedItem);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        //maps a Mongo.Models.ContentItem to a Chronozoom.Business.Models.ContentItem
        private Chronozoom.Business.Models.ContentItem mapContentItem(ContentItem contentItem)
        {
            Chronozoom.Business.Models.ContentItem mappedContentItem = new Chronozoom.Business.Models.ContentItem
            {
                Id = contentItem.Id,
                Depth = contentItem.Depth,
                Title = contentItem.Title,
                Caption = contentItem.Caption,
                Year = contentItem.Year,
                Uri = contentItem.Uri,
                Attribution = contentItem.Attribution,
                Order = short.Parse(contentItem.Order.ToString()),
                MediaType = (string)contentItem.GetType().GetProperty("type").GetValue(contentItem, null),
                MediaSource = (string)contentItem.GetType().GetProperty("source").GetValue(contentItem, null)
            };
            return mappedContentItem;
        }

        //maps a Mongo.Models.Exhibit to a Chronozoom.Business.Models.Exhibit
        private Chronozoom.Business.Models.Exhibit mapExhibit(Mongo.Models.Exhibit exhibit, Chronozoom.Business.Models.User UpdatedBy)
        {
            List<Chronozoom.Business.Models.ContentItem> listMappedContentItem = new List<Chronozoom.Business.Models.ContentItem>();
            foreach (ContentItem contentItem in exhibit.ContentItems)
            {
                Chronozoom.Business.Models.ContentItem mappedContentItem = mapContentItem(contentItem);
                listMappedContentItem.Add(mappedContentItem);
            }

            Chronozoom.Business.Models.Exhibit mappedExhibit = new Chronozoom.Business.Models.Exhibit
            {
                Id = exhibit.Id,
                Depth = exhibit.Depth,
                Title = exhibit.Title,
                Year = exhibit.Year,
                IsCirca = exhibit.IsCirca,
                UpdatedBy = UpdatedBy,
                UpdatedTime = exhibit.UpdatedAt,
                ContentItems = listMappedContentItem
            };
            return mappedExhibit;
        }

        //maps a Chronozoom.Business.Models.ContentItem to a Mongo.Models.ContentItem
        private ContentItem mapContentItem(Chronozoom.Business.Models.ContentItem contentItem)
        {
            ContentItem mappedContentItem = new ContentItem
            {
                Id = contentItem.Id,
                Depth = contentItem.Depth,
                Title = contentItem.Title,
                Caption = contentItem.Caption,
                Year = (int)contentItem.Year, //doesn't this need to be decimal?
                Uri = contentItem.Uri,
                Attribution = contentItem.Attribution,
                Order = short.Parse(contentItem.Order.ToString())
                //why is there an internal class for media type and source?
                //they still need to be mapped here
            };
            return mappedContentItem;
        }

        //maps a Chronozoom.Business.Models.Exhibit to a Mongo.Models.Exhibit
        private Exhibit mapExhibit(Chronozoom.Business.Models.Exhibit exhibit)
        {
            List<ContentItem> listMappedContentItem = new List<ContentItem>();
            foreach (Chronozoom.Business.Models.ContentItem contentItem in exhibit.ContentItems)
            {
                ContentItem mappedContentItem = mapContentItem(contentItem);
                listMappedContentItem.Add(mappedContentItem);
            }

            Exhibit mappedExhibit = new Exhibit
            {
                Id = exhibit.Id,
                Depth = exhibit.Depth,
                Title = exhibit.Title,
                Year = exhibit.Year,
                IsCirca = exhibit.IsCirca,
                UpdatedByUser = exhibit.UpdatedBy.Id,
                UpdatedAt = exhibit.UpdatedTime,
                ContentItems = listMappedContentItem
            };
            return mappedExhibit;
        }
    }
}
