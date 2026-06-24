using Cmf.Foundation.Common.Abstractions;
using Cmf.Navigo.BusinessObjects.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MutationTestPlayground
{
    public class CustomGetLotDetailsIntegrationHandler : DeeDevBase
    {
        public override bool DeeTestCondition(Dictionary<string, object> Input)
        {
            //---Start DEE Condition Code---

            /// <summary>
            /// Summary text:
            ///     Call the action CustomGetLotDetailsFromMAM, take the name of the material to trigger the Request of Lot Details.
            /// Action Groups:
            ///     N/A
            /// </summary>

            #region Constants

            const string InputMaterialName = "MaterialName";
            const string CustomLotMaterialForm = "Lot";

            #endregion Constants

            IServiceProvider _serviceProvider = (IServiceProvider)Input["ServiceProvider"];

            bool canExecute = false;
            string materialName = Input[InputMaterialName] as string;

            IMaterial material = _serviceProvider.GetService<IMaterial>();
            material.Name = materialName;
            material.Load();

            if (material.Form.Equals(CustomLotMaterialForm, StringComparison.InvariantCultureIgnoreCase))
            {
                canExecute = true;
            }

            return canExecute;

            //---End DEE Condition Code---
        }

        public override Dictionary<string, object> DeeActionCode(Dictionary<string, object> Input)
        {
            //---Start DEE Code---

            // System
            UseReference("", "System.Threading");
            UseReference("", "System.Text");

            // Navigo
            UseReference("", "Cmf.Navigo.Common");

            // Foundation
            UseReference("Cmf.Foundation.BusinessOrchestration.dll", "Cmf.Foundation.BusinessOrchestration.Abstractions");
            UseReference("Cmf.Foundation.BusinessOrchestration.dll", "Cmf.Foundation.BusinessOrchestration.ErpManagement.InputObjects");

            #region Global variables

            IServiceProvider _serviceProvider = (IServiceProvider)Input["ServiceProvider"];
            IAction _customGetLotDetailsFromMAMActionName = _serviceProvider.GetService<IAction>();

            #endregion Global variables

            #region Constants

            // Input Variable Name
            const string InputMaterialName = "MaterialName";
            const string CustomGetLotDetailsFromMAMActionName = "CustomGetLotDetailsFromMAM";

            #endregion Constants

            string materialName = Input[InputMaterialName] as string;

            IMaterial material = _serviceProvider.GetService<IMaterial>();
            material.Name = materialName;

            if (material.ObjectExists())
            {
                #region Trigger 'Get Lot Details' logic

                _customGetLotDetailsFromMAMActionName.Load(CustomGetLotDetailsFromMAMActionName);
                Dictionary<string, object> actionDataResult = _customGetLotDetailsFromMAMActionName.ExecuteAction
                (
                    new Dictionary<string, object>()
                    {
                        { "Material", material },
                    }
                );

                #endregion Trigger 'Get Lot Details' logic
            }

            //---End DEE Code---

            return Input;
        }
    }
}