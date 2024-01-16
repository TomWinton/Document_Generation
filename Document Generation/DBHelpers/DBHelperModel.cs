using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DocumentGeneration.DBHelpers
    {
        public class DocumentParamaterRequestResponse
        {
          public DocumentParamaterRequestResponse(string documentId , IEnumerable<DocumentParamaterRequest> documentParamaterRequestList)
            {
            DocumentId = documentId;
            DocumentParamaterRequestList = documentParamaterRequestList;
            }

        public string DocumentId {  get; set; }
           public  IEnumerable<DocumentParamaterRequest> DocumentParamaterRequestList { get; set; }
        }
    public class DocumentParamaterRequest
        {
        public string Type { get; set; }
        public string Paramater { get; set; }
        }
    }