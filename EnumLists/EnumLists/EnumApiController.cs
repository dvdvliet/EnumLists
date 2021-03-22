using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Hosting;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace EnumLists
{
    [PluginController("EnumLists")]
    public class EnumApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<string> GetAll(string assemblyName, string typeName)
        {
            var assembly = GetAssembly(assemblyName);
            if (assembly == null)
                throw new ArgumentException($"DataType Error: Assembly {assemblyName} does not exists.",
                    nameof(assemblyName));

            var type = assembly.GetType(typeName);
            if (type == null)
                throw new ArgumentException(
                    $"DataType Error: Type {typeName} does not exists in assembly {assemblyName}.", nameof(typeName));

            List<string> items = new List<string>();

            foreach (string name in Enum.GetNames(type))
            {
                items.Add(name);
            }

            return items;
        }

        /// <summary>
        /// Gets the <see cref="Assembly"/> with the specified name.
        /// </summary>
        /// <param name="assemblyName">The <see cref="Assembly"/> name.</param>
        /// <returns>The <see cref="Assembly"/>.</returns>
        private static Assembly GetAssembly(string assemblyName)
        {
            if (string.Equals(assemblyName, "App_Code", StringComparison.InvariantCultureIgnoreCase))
            {
                return Assembly.Load(assemblyName);
            }

            var path = HostingEnvironment.MapPath(string.Concat("~/bin/", assemblyName));
            return Assembly.ReflectionOnlyLoadFrom(path);
        }
    }
}