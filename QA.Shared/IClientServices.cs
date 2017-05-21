

using DataAccess;

using System.Data;

namespace QA
{
    public interface IClientServices
    {
        string LastError { get; }

        DataSet Execute(RequestCollection requests);

        object this[string key] { get; set; }

        void SetInformation(string key, object value);

        object GetInformation(string key);

        string Localize(string name);

        string Localize(string category, string name);

        string GetSetting(string name);

        string GetSetting(string category, string name);

        void LoadSettings();

        bool HasPermission(string type);

        void LoadControl(string typeName);

        void RunReport(string reportID, object param);

        void RunReport(string reportID, object param, string newGuid);

        // hungnq 2014-10-08
        //void LoadPatientByCondition(string condition);
    }
}