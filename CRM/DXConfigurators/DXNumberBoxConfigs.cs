using DevExtreme.AspNet.Mvc.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DXConfigurators
{
    public static class DXNumberBoxConfigs
    {
        public static NumberBoxBuilder CurrencyConfig(this NumberBoxBuilder numberBox, double step = 5)
        {
            return numberBox.Step(step).ShowSpinButtons(true).Format("$ #,##0.##");
        }

        public static NumberBoxBuilder PercentageConfig(this NumberBoxBuilder numberBox, double step = 1)
        {
            return numberBox.Step(Math.Round(step / 100, 2)).ShowSpinButtons(true).Format("#0 %");
        }
    }
}
