using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Helper.Common
{
   public class Tool
    {
        //public enum Categoria
        //{
        //    Tecnologia = 1,
        //    [Description("Pendiente Aprobacion")]
        //    Mercadotecnia = 2
        //}

        //public static List<SelectListItem> ToListSelectListItemGeneric<T>(List<Models.Categoria> entList)
        //{
        //    var t = typeof(T);
        //    if (!t.IsClass) { throw new ApplicationException("Tipo debe ser Clase"); }

        //    return entList.Select(item => new SelectListItem() { Text = item.Categoria1, Value = item.IdCategoria.ToString() }).ToList();
        //}


        public static List<SelectListItem> ConvertToSelectListItemCollection<T>(List<T> source, Func<T, string> text,
        Func<T, string> value, bool createEmpty = true)
        {
            var SelectListItems = new List<SelectListItem>();
            if (createEmpty)
                SelectListItems.Add(new SelectListItem { Text = "Please Select", Value = "", Selected = true });

            foreach (var item in source)
            {
                SelectListItems.Add(new SelectListItem { Text = text(item), Value = value(item) });
            }

            return SelectListItems;
        }

        public static List<SelectListItem> ConvertToSelectListItemCollection<T>
             (List<T> source, Func<T, string> textAndValue, bool createEmpty = true)
             where T : class
        {
            return ConvertToSelectListItemCollection(source, textAndValue, textAndValue, createEmpty);
        }


        public static List<SelectListItem> ToListSelectListItemEnum<T>()
        {
            var t = typeof(T);
            if (!t.IsEnum) { throw new ApplicationException("Tipo debe ser enum"); }

            var members = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            var result = new List<SelectListItem>();

            foreach (var member in members)
            {
                var attributeDescription = member.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = member.Name;

                if (attributeDescription.Any())
                {
                    description = ((DescriptionAttribute)attributeDescription[0]).Description;
                }
                var valor = ((int)Enum.Parse(t, member.Name));
                result.Add(new SelectListItem()
                {
                    Text = description,
                    Value = valor.ToString()
                });
            }

            return result;
        }
    }
}
