using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QA.DocumentationAndroid
{
    internal interface IDocument
    {
        string PatientID { get; set; }

        string FacAdmissionID { get; set; }

        string PhysicianAdmissionID { get; set; }

        string DocInstanceID { get; set; }

        string DocType { get; set; }

        DateTime DocDate { get; set; }

        DataRow LDocumentationType { get; set; }
    }
}