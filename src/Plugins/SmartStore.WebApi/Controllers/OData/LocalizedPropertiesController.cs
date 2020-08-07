﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SmartStore.Core.Domain.Localization;
using SmartStore.Services.Localization;
using SmartStore.Web.Framework.WebApi;
using SmartStore.Web.Framework.WebApi.OData;
using SmartStore.Web.Framework.WebApi.Security;

namespace SmartStore.WebApi.Controllers.OData
{
    [WebApiAuthenticate]
	public class LocalizedPropertiesController : WebApiEntityController<LocalizedProperty, ILocalizedEntityService>
	{
		protected override void Insert(LocalizedProperty entity)
		{
			Service.InsertLocalizedProperty(entity);
		}
		protected override void Update(LocalizedProperty entity)
		{
			Service.UpdateLocalizedProperty(entity);
		}
		protected override void Delete(LocalizedProperty entity)
		{
			Service.DeleteLocalizedProperty(entity);
		}

		[WebApiQueryable]
		public IQueryable<LocalizedProperty> Get()
		{
			return GetEntitySet();
		}

		[WebApiQueryable]
		public SingleResult<LocalizedProperty> Get(int key)
		{
			return GetSingleResult(key);
		}

		public IHttpActionResult Post(LocalizedProperty entity)
		{
			var result = Insert(entity, () => Service.InsertLocalizedProperty(entity));
			return result;
		}

		public async Task<IHttpActionResult> Put(int key, LocalizedProperty entity)
		{
			var result = await UpdateAsync(entity, key, () => Service.UpdateLocalizedProperty(entity));
			return result;
		}

		public async Task<IHttpActionResult> Patch(int key, Delta<LocalizedProperty> model)
		{
			var result = await PartiallyUpdateAsync(key, model, entity => Service.UpdateLocalizedProperty(entity));
			return result;
		}

		public async Task<IHttpActionResult> Delete(int key)
		{
			var result = await DeleteAsync(key, entity => Service.DeleteLocalizedProperty(entity));
			return result;
		}

		#region Navigation properties

		[WebApiQueryable]
		public SingleResult<Language> GetLanguage(int key)
		{
			return GetRelatedEntity(key, x => x.Language);
		}

		#endregion
	}
}
