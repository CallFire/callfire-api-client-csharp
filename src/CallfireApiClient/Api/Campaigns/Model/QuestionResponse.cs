using CallfireApiClient.Api.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class QuestionResponse : CallfireModel
    {
        public string Question { get; private set; }

        public string Response { get; private set; }

        public override string ToString()
        {
            return string.Format("[QuestionResponse: Question={0}, Response={1}", Question, Response);
        }
    }
}
