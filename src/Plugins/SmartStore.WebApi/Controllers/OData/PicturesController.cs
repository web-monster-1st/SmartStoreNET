﻿using System.Linq;
using System.Net.Http;
using System.Web.Http;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Domain.Media;
using SmartStore.Core.Security;
using SmartStore.Services.Media;
using SmartStore.Web.Framework.WebApi;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.OData
{
    [WebApiAuthenticate(Permission = Permissions.Catalog.Product.EditPicture)]
	public class PicturesController : WebApiEntityController<MediaFile, IMediaService>
	{
        protected override IQueryable<MediaFile> GetEntitySet()
        {
            var query =
                from x in Repository.Table
                where !x.Deleted && !x.Hidden
                select x;

            return query;
        }

        protected override void Insert(MediaFile entity)
		{
			throw this.ExceptionNotImplemented();
		}

        [WebApiAuthenticate(Permission = Permissions.Media.Update)]
        protected override void Update(MediaFile entity)
		{
			throw this.ExceptionNotImplemented();
		}

        [WebApiAuthenticate(Permission = Permissions.Media.Delete)]
        protected override void Delete(MediaFile entity)
		{
            var permanent = false;
            var queries = Request?.RequestUri?.ParseQueryString();

            if (queries?.AllKeys?.Contains("permanent") ?? false)
            {
                permanent = queries["permanent"].ToBool(permanent);
            }

            Service.DeleteFile(entity, permanent);
        }

		[WebApiQueryable]
        public SingleResult<MediaFile> GetPicture(int key)
		{
			return GetSingleResult(key);
		}

		// Navigation properties.

		[WebApiQueryable]
        public IQueryable<ProductMediaFile> GetProductPictures(int key)
		{
			return GetRelatedCollection(key, x => x.ProductMediaFiles);
		}
	}
}
