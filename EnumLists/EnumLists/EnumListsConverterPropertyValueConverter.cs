using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace EnumLists
{
    public class EnumListsConverterPropertyValueConverter : PropertyValueConverterBase
    {
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
            => typeof(IEnumerable<string>);

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
            => PropertyCacheLevel.Element;

        public override bool IsConverter(IPublishedPropertyType propertyType)
        {
            return propertyType.EditorAlias.Equals(
                Constants.EnumListsConverterPropertyEditorAlias);
        }

        public override object ConvertSourceToIntermediate(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            object source,
            bool preview)
        {
            return source;
        }

        public override object ConvertIntermediateToObject(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel,
            object inter,
            bool preview)
        {
            if (inter == null)
            {
                return null;
            }

            return
                JsonConvert.DeserializeObject<IEnumerable<string>>(
                    inter.ToString());
        }
    }
}