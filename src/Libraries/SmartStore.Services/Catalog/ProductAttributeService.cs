using System;
using System.Collections.Generic;
using System.Linq;
using SmartStore.Core.Caching;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Services.Events;
using SmartStore.Services.Media;
using SmartStore.Core.Infrastructure;
using SmartStore.Data;
using System.Text;
using System.Linq.Expressions;
using SmartStore.Core.Domain.Media;

namespace SmartStore.Services.Catalog
{
    /// <summary>
    /// Product attribute service
    /// </summary>
    public partial class ProductAttributeService : IProductAttributeService
    {
        #region Constants
        private const string PRODUCTATTRIBUTES_ALL_KEY = "SmartStore.productattribute.all";
        private const string PRODUCTVARIANTATTRIBUTES_ALL_KEY = "SmartStore.productvariantattribute.all-{0}";
        private const string PRODUCTVARIANTATTRIBUTEVALUES_ALL_KEY = "SmartStore.productvariantattributevalue.all-{0}";
        private const string PRODUCTATTRIBUTES_PATTERN_KEY = "SmartStore.productattribute.";
        private const string PRODUCTVARIANTATTRIBUTES_PATTERN_KEY = "SmartStore.productvariantattribute.";
        private const string PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY = "SmartStore.productvariantattributevalue.";
        #endregion

        #region Fields

        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<ProductVariantAttribute> _productVariantAttributeRepository;
        private readonly IRepository<ProductVariantAttributeCombination> _productVariantAttributeCombinationRepository;
        private readonly IRepository<ProductVariantAttributeValue> _productVariantAttributeValueRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
		private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="productAttributeRepository">Product attribute repository</param>
        /// <param name="productVariantAttributeRepository">Product variant attribute mapping repository</param>
        /// <param name="productVariantAttributeCombinationRepository">Product variant attribute combination repository</param>
        /// <param name="productVariantAttributeValueRepository">Product variant attribute value repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductAttributeService(ICacheManager cacheManager,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<ProductVariantAttribute> productVariantAttributeRepository,
            IRepository<ProductVariantAttributeCombination> productVariantAttributeCombinationRepository,
            IRepository<ProductVariantAttributeValue> productVariantAttributeValueRepository,
            IEventPublisher eventPublisher,
			IPictureService pictureService)
        {
            _cacheManager = cacheManager;
            _productAttributeRepository = productAttributeRepository;
            _productVariantAttributeRepository = productVariantAttributeRepository;
            _productVariantAttributeCombinationRepository = productVariantAttributeCombinationRepository;
            _productVariantAttributeValueRepository = productVariantAttributeValueRepository;
            _eventPublisher = eventPublisher;
			_pictureService = pictureService;
        }

        #endregion

        // Autowired Dependency (is a proptery dependency to avoid circularity)
        public virtual IProductAttributeParser AttributeParser { get; set; }

        #region Methods

        #region Product attributes

        /// <summary>
        /// Deletes a product attribute
        /// </summary>
        /// <param name="productAttribute">Product attribute</param>
        public virtual void DeleteProductAttribute(ProductAttribute productAttribute)
        {
            if (productAttribute == null)
                throw new ArgumentNullException("productAttribute");

            _productAttributeRepository.Delete(productAttribute);

            //cache
            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productAttribute);
        }

        /// <summary>
        /// Gets all product attributes
        /// </summary>
        /// <returns>Product attribute collection</returns>
        public virtual IList<ProductAttribute> GetAllProductAttributes()
        {
            string key = PRODUCTATTRIBUTES_ALL_KEY;
            return _cacheManager.Get(key, () =>
            {
                var query = from pa in _productAttributeRepository.Table
                            orderby pa.Name
                            select pa;
                var productAttributes = query.ToList();
                return productAttributes;
            });
        }

        /// <summary>
        /// Gets a product attribute 
        /// </summary>
        /// <param name="productAttributeId">Product attribute identifier</param>
        /// <returns>Product attribute </returns>
        public virtual ProductAttribute GetProductAttributeById(int productAttributeId)
        {
            if (productAttributeId == 0)
                return null;

            return _productAttributeRepository.GetById(productAttributeId);
        }

        /// <summary>
        /// Inserts a product attribute
        /// </summary>
        /// <param name="productAttribute">Product attribute</param>
        public virtual void InsertProductAttribute(ProductAttribute productAttribute)
        {
            if (productAttribute == null)
                throw new ArgumentNullException("productAttribute");

            _productAttributeRepository.Insert(productAttribute);
            
            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productAttribute);
        }

        /// <summary>
        /// Updates the product attribute
        /// </summary>
        /// <param name="productAttribute">Product attribute</param>
        public virtual void UpdateProductAttribute(ProductAttribute productAttribute)
        {
            if (productAttribute == null)
                throw new ArgumentNullException("productAttribute");

            _productAttributeRepository.Update(productAttribute);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productAttribute);
        }

        #endregion

        #region Product variant attributes mappings (ProductVariantAttribute)

        /// <summary>
        /// Deletes a product variant attribute mapping
        /// </summary>
        /// <param name="productVariantAttribute">Product variant attribute mapping</param>
        public virtual void DeleteProductVariantAttribute(ProductVariantAttribute productVariantAttribute)
        {
            if (productVariantAttribute == null)
                throw new ArgumentNullException("productVariantAttribute");

            _productVariantAttributeRepository.Delete(productVariantAttribute);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productVariantAttribute);
        }

        /// <summary>
        /// Gets product variant attribute mappings by product identifier
        /// </summary>
        /// <param name="productVariantId">The product variant identifier</param>
        /// <returns>Product variant attribute mapping collection</returns>
        public virtual IList<ProductVariantAttribute> GetProductVariantAttributesByProductVariantId(int productVariantId)
        {
            string key = string.Format(PRODUCTVARIANTATTRIBUTES_ALL_KEY, productVariantId);

            return _cacheManager.Get(key, () =>
            {
                var query = from pva in _productVariantAttributeRepository.Table
                            orderby pva.DisplayOrder
                            where pva.ProductVariantId == productVariantId
                            select pva;
                var productVariantAttributes = query.ToList();
                return productVariantAttributes;
            });
        }

        /// <summary>
        /// Gets a product variant attribute mapping
        /// </summary>
        /// <param name="productVariantAttributeId">Product variant attribute mapping identifier</param>
        /// <returns>Product variant attribute mapping</returns>
        public virtual ProductVariantAttribute GetProductVariantAttributeById(int productVariantAttributeId)
        {
            if (productVariantAttributeId == 0)
                return null;

            return _productVariantAttributeRepository.GetById(productVariantAttributeId);
        }

        // codehint: sm-add
        public virtual IEnumerable<ProductVariantAttribute> GetProductVariantAttributesByIds(params int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return Enumerable.Empty<ProductVariantAttribute>();
            }

            return _productVariantAttributeRepository.GetMany(ids);
        }

        // codehint: sm-add
        public virtual IEnumerable<ProductVariantAttributeValue> GetProductVariantAttributeValuesByIds(params int[] productVariantAttributeValueIds)
        {
            if (productVariantAttributeValueIds == null || productVariantAttributeValueIds.Length == 0)
            {
                return Enumerable.Empty<ProductVariantAttributeValue>();
            }

            return _productVariantAttributeValueRepository.GetMany(productVariantAttributeValueIds);
        }

        /// <summary>
        /// Inserts a product variant attribute mapping
        /// </summary>
        /// <param name="productVariantAttribute">The product variant attribute mapping</param>
        public virtual void InsertProductVariantAttribute(ProductVariantAttribute productVariantAttribute)
        {
            if (productVariantAttribute == null)
                throw new ArgumentNullException("productVariantAttribute");

            _productVariantAttributeRepository.Insert(productVariantAttribute);
            
            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productVariantAttribute);
        }

        /// <summary>
        /// Updates the product variant attribute mapping
        /// </summary>
        /// <param name="productVariantAttribute">The product variant attribute mapping</param>
        public virtual void UpdateProductVariantAttribute(ProductVariantAttribute productVariantAttribute)
        {
            if (productVariantAttribute == null)
                throw new ArgumentNullException("productVariantAttribute");

            _productVariantAttributeRepository.Update(productVariantAttribute);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productVariantAttribute);
        }

        #endregion

        #region Product variant attribute values (ProductVariantAttributeValue)

        /// <summary>
        /// Deletes a product variant attribute value
        /// </summary>
        /// <param name="productVariantAttributeValue">Product variant attribute value</param>
        public virtual void DeleteProductVariantAttributeValue(ProductVariantAttributeValue productVariantAttributeValue)
        {
            if (productVariantAttributeValue == null)
                throw new ArgumentNullException("productVariantAttributeValue");

            _productVariantAttributeValueRepository.Delete(productVariantAttributeValue);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productVariantAttributeValue);
        }

        /// <summary>
        /// Gets product variant attribute values by product identifier
        /// </summary>
        /// <param name="productVariantAttributeId">The product variant attribute mapping identifier</param>
        /// <returns>Product variant attribute mapping collection</returns>
        public virtual IList<ProductVariantAttributeValue> GetProductVariantAttributeValues(int productVariantAttributeId)
        {
            string key = string.Format(PRODUCTVARIANTATTRIBUTEVALUES_ALL_KEY, productVariantAttributeId);
            return _cacheManager.Get(key, () =>
            {
                var query = from pvav in _productVariantAttributeValueRepository.Table
                            orderby pvav.DisplayOrder
                            where pvav.ProductVariantAttributeId == productVariantAttributeId
                            select pvav;
                var productVariantAttributeValues = query.ToList();
                return productVariantAttributeValues;
            });
        }

        /// <summary>
        /// Gets a product variant attribute value
        /// </summary>
        /// <param name="productVariantAttributeValueId">Product variant attribute value identifier</param>
        /// <returns>Product variant attribute value</returns>
        public virtual ProductVariantAttributeValue GetProductVariantAttributeValueById(int productVariantAttributeValueId)
        {
            if (productVariantAttributeValueId == 0)
                return null;

            return _productVariantAttributeValueRepository.GetById(productVariantAttributeValueId);
        }

        /// <summary>
        /// Inserts a product variant attribute value
        /// </summary>
        /// <param name="productVariantAttributeValue">The product variant attribute value</param>
        public virtual void InsertProductVariantAttributeValue(ProductVariantAttributeValue productVariantAttributeValue)
        {
            if (productVariantAttributeValue == null)
                throw new ArgumentNullException("productVariantAttributeValue");

            _productVariantAttributeValueRepository.Insert(productVariantAttributeValue);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productVariantAttributeValue);
        }

        /// <summary>
        /// Updates the product variant attribute value
        /// </summary>
        /// <param name="productVariantAttributeValue">The product variant attribute value</param>
        public virtual void UpdateProductVariantAttributeValue(ProductVariantAttributeValue productVariantAttributeValue)
        {
            if (productVariantAttributeValue == null)
                throw new ArgumentNullException("productVariantAttributeValue");

            _productVariantAttributeValueRepository.Update(productVariantAttributeValue);

            _cacheManager.RemoveByPattern(PRODUCTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTVARIANTATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productVariantAttributeValue);
        }

        #endregion

        #region Product variant attribute combinations (ProductVariantAttributeCombination)

        public virtual bool VariantHasAttributeCombinations(int productVariantId, bool showHidden = false)
        {
            if (productVariantId == 0)
            {
                return false;
            }
            
            var query = from c in _productVariantAttributeCombinationRepository.Table
                        where c.ProductVariantId == productVariantId
                        select c;

            if (!showHidden)
            {
                query = query.Where(x => x.IsActive == true);
            }

            return query.Select(x => x.Id).Any();
        }

		//private void EnsureSingleDefaultVariant(ProductVariantAttributeCombination combination) {
		//	// the current combination should be the default one, so reset the prior default combination.
		//	var query = from c in _productVariantAttributeCombinationRepository.Table
		//				where c.IsDefaultCombination && c.ProductVariantId == combination.ProductVariantId             
		//				select c;

		//	foreach (var comb in query.ToList())
		//	{
		//		comb.IsDefaultCombination = false;
		//		_productVariantAttributeCombinationRepository.Update(comb);
		//	}
		//}
        
		private void CombineAll(List<List<ProductVariantAttributeValue>> toCombine, List<List<ProductVariantAttributeValue>> result, int y, List<ProductVariantAttributeValue> tmp) {
			var combine = toCombine[y];

			for (int i = 0; i < combine.Count; ++i) {
				List<ProductVariantAttributeValue> lst = new List<ProductVariantAttributeValue>(tmp);
				lst.Add(combine[i]);

				if (y == (toCombine.Count - 1))
					result.Add(lst);
				else
					CombineAll(toCombine, result, y + 1, lst);
			}
		}

        /// <summary>
        /// Deletes a product variant attribute combination
        /// </summary>
        /// <param name="combination">Product variant attribute combination</param>
        public virtual void DeleteProductVariantAttributeCombination(ProductVariantAttributeCombination combination)
        {
            if (combination == null)
                throw new ArgumentNullException("combination");

			// codehint: sm-add
            // pictures

            //var wasDefault = combination.IsDefaultCombination;
            //var prevId = combination.ProductVariantId;

            _productVariantAttributeCombinationRepository.Delete(combination);

			//if (wasDefault)
			//{
			//	// we deleted the default combination, set another as default!
			//	var newDefault = _productVariantAttributeCombinationRepository.Table.FirstOrDefault(x => x.ProductVariantId == prevId);
			//	if (newDefault != null)
			//	{
			//		newDefault.IsDefaultCombination = true;
			//		_productVariantAttributeCombinationRepository.Update(newDefault);
			//	}
			//}

            //event notification
            _eventPublisher.EntityDeleted(combination);
        }

        /// <summary>
        /// Gets all product variant attribute combinations
        /// </summary>
        /// <param name="productVariantId">Product variant identifier</param>
        /// <returns>Product variant attribute combination collection</returns>
        public virtual IList<ProductVariantAttributeCombination> GetAllProductVariantAttributeCombinations(int productVariantId, bool showHidden = false)
        {
            if (productVariantId == 0)
                return new List<ProductVariantAttributeCombination>();

            var query = from pvac in _productVariantAttributeCombinationRepository.Table
                        orderby pvac.Id
                        where pvac.ProductVariantId == productVariantId
                        select pvac;

            if (!showHidden)
            {
                query = query.Where(x => x.IsActive == true);
            }

            var combinations = query.ToList();
            return combinations;
        }

        /// <summary>
        /// Gets a product variant attribute combination
        /// </summary>
        /// <param name="productVariantAttributeCombinationId">Product variant attribute combination identifier</param>
        /// <returns>Product variant attribute combination</returns>
        public virtual ProductVariantAttributeCombination GetProductVariantAttributeCombinationById(int productVariantAttributeCombinationId)
        {
            if (productVariantAttributeCombinationId == 0)
                return null;
            
            var combination = _productVariantAttributeCombinationRepository.GetById(productVariantAttributeCombinationId);
            return combination;
        }

        /// <summary>
        /// Inserts a product variant attribute combination
        /// </summary>
        /// <param name="combination">Product variant attribute combination</param>
        public virtual void InsertProductVariantAttributeCombination(ProductVariantAttributeCombination combination)
        {
            if (combination == null)
                throw new ArgumentNullException("combination");

			// codehint: sm-add
			//if (combination.IsDefaultCombination)
			//{
			//	EnsureSingleDefaultVariant(combination);
			//}

            _productVariantAttributeCombinationRepository.Insert(combination);

            //event notification
            _eventPublisher.EntityInserted(combination);
        }

        /// <summary>
        /// Updates a product variant attribute combination
        /// </summary>
        /// <param name="combination">Product variant attribute combination</param>
        public virtual void UpdateProductVariantAttributeCombination(ProductVariantAttributeCombination combination)
        {
            if (combination == null)
                throw new ArgumentNullException("combination");

            // codehint: sm-add
			//if (combination.IsDefaultCombination)
			//{
			//	EnsureSingleDefaultVariant(combination);
			//}
			//else
			//{
			//	// check if it was default before modification...
			//	// but make it Type-Safe (resistant to code refactoring ;-))
			//	Expression<Func<ProductVariantAttributeCombination, bool>> expr = x => x.IsDefaultCombination;
			//	string propertyToCheck = expr.ExtractPropertyInfo().Name;

			//	object originalValue = null;
			//	if (_productVariantAttributeCombinationRepository.GetModifiedProperties(combination).TryGetValue(propertyToCheck, out originalValue))
			//	{
			//		bool wasDefault = (bool)originalValue;
			//		if (wasDefault) 
			//		{
			//			// we can't uncheck the default variant within a combination list,
			//			// we would't have a default combination anymore.
			//			combination.IsDefaultCombination = true;
			//		}
			//	}
			//}

            _productVariantAttributeCombinationRepository.Update(combination);

            //event notification
            _eventPublisher.EntityUpdated(combination);
        }

		/// <summary>
		/// Creates all variant attribute combinations
		/// </summary>
		/// <param name="productVariantId">Product variant identifier</param>
		public virtual void CreateAllProductVariantAttributeCombinations(ProductVariant variant) {
			// delete all existing combinations
			foreach(var itm in GetAllProductVariantAttributeCombinations(variant.Id, true)) {
				DeleteProductVariantAttributeCombination(itm);
			}

			var attributes = GetProductVariantAttributesByProductVariantId(variant.Id);
			if (attributes == null || attributes.Count <= 0)
				return;

			var toCombine = new List<List<ProductVariantAttributeValue>>();
			var resultMatrix = new List<List<ProductVariantAttributeValue>>();
			var tmp = new List<ProductVariantAttributeValue>();

			foreach (var attr in attributes) {
				var attributeValues = attr.ProductVariantAttributeValues.ToList();
				if (attributeValues.Count > 0)
					toCombine.Add(attributeValues);
			}

			if (toCombine.Count > 0) {
				CombineAll(toCombine, resultMatrix, 0, tmp);

				foreach (var values in resultMatrix) {
					string attrXml = "";
					foreach (var x in values) 
                    {
						attrXml = this.AttributeParser.AddProductAttribute(attrXml, attributes[values.IndexOf(x)], x.Id.ToString());
					}

					var combination = new ProductVariantAttributeCombination {
						ProductVariantId = variant.Id,
						AttributesXml = attrXml,
						StockQuantity = 10000,
						AllowOutOfStockOrders = true,
						IsActive = true
					};

					_productVariantAttributeCombinationRepository.Insert(combination);
					_eventPublisher.EntityInserted(combination);
				}
			}


			//foreach (var y in resultMatrix) {
			//	StringBuilder sb = new StringBuilder();
			//	foreach (var x in y) {
			//		sb.AppendFormat("{0} ", x.Name);
			//	}
			//	sb.ToString().Dump();
			//}


			//Sample ResultMatrix:
			//Size Color Material	(var attributes)
			//1X Blau Cotton (var AttrXml)
			//1X Blau Leather 
			//1X Gr�n Cotton 
			//1X Gr�n Leather 
			//1X Rot Cotton 
			//1X Rot Leather 
			//2X Blau Cotton 
			//2X Blau Leather 
			//2X Gr�n Cotton 
			//2X Gr�n Leather 
			//2X Rot Cotton 
			//2X Rot Leather 
			//3X Blau Cotton 
			//3X Blau Leather 
			//3X Gr�n Cotton 
			//3X Gr�n Leather 
			//3X Rot Cotton 
			//3X Rot Leather 
			//4X Blau Cotton 
			//4X Blau Leather 
			//4X Gr�n Cotton 
			//4X Gr�n Leather 
			//4X Rot Cotton 
			//4X Rot Leather 
			//5X Blau Cotton 
			//5X Blau Leather 
			//5X Gr�n Cotton 
			//5X Gr�n Leather 
			//5X Rot Cotton 
			//5X Rot Leather

		}

        #endregion

        #endregion
    }
}
