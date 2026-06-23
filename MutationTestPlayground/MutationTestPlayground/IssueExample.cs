using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MutationTestPlayground
{
    public class IssueExample : DeeDevBase

    {
        public override bool DeeTestCondition(Dictionary<string, object> Input)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, object> DeeActionCode(Dictionary<string, object> Input)
        {
            string actionGroupName = Input["ActionGroupName"] as string;
            string sourceAPI = GetSourceAPIFromActionGroup(actionGroupName);


            Input["sourceAPI"] = sourceAPI;
            return Input;

            string GetSourceAPIFromActionGroup(string actionGroupName)
            {
                string sourceAPI = actionGroupName.Split('.')
                    .Reverse()
                    .Skip(1)
                    .FirstOrDefault();

                return sourceAPI;
            }

        }
    }
}
