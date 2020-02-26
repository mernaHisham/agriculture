using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agriculure.WebUi.Custom_Classes
{
    public class StackHolderTypeModel
    {
        public int StackHolderTypeId { get; set; }
        public string StackHolderTypeName { get; set; }

        public List<string> StackHolders = new List<string>
        {
            "Farmer",
            "Supplier",
            "Producer",
            "Logistics"
        };
    }
    public static class StackHolderType
    {
        public static List<StackHolderTypeModel> GetStackHolderTypes()
        {
            StackHolderTypeModel typeModel = new StackHolderTypeModel();

            var StackHolderList = new List<StackHolderTypeModel>();
            for (int i = 0; i < typeModel.StackHolders.Count; i++)
            {
                StackHolderList.Add(new StackHolderTypeModel
                {
                    StackHolderTypeId = i,
                    StackHolderTypeName = typeModel.StackHolders[i]
                });
            }

            return StackHolderList;
        }

    }
}