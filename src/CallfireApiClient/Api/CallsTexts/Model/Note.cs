using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class Note : CallfireModel
    {
        public string Text { get; set; }

        public DateTime? Created { get; set; }

        public override string ToString()
        {
            return string.Format("[Note: Text={0}, Created={1}]", Text, Created);
        }
    }
}

